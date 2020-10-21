using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface IPhaseSetsGetter
    {
        PhaseSets Get(string participantID);
    }

    public class PhaseSetsGetter : IPhaseSetsGetter
    {
        private readonly SQLiteDBContext _dbContext;
        private readonly IImageIndexesGetter _imageIndexesGetter;
        private readonly ICollectionRandomizer _collectionRandomizer;
        private readonly IConfig _config;

        public static readonly IEnumerable<string> TestOldImageTypes = new List<string>{"A", "AI", "D", "DI", "F", "FI"};
        public static readonly IEnumerable<string> TestNewImageTypes = new List<string>{"N", "NI"};
        public static readonly IEnumerable<string> EncodingImageTypes = new List<string> { "A", "A", "AI", "AI", "B", "BI", "C", "CI", "F", "F", "FI", "FI" };

        public PhaseSetsGetter(SQLiteDBContext dbContext, IImageIndexesGetter imageIndexesGetter, ICollectionRandomizer collectionRandomizer, IOptions<Config> config)
        {
            _dbContext = dbContext;
            _imageIndexesGetter = imageIndexesGetter;
            _collectionRandomizer = collectionRandomizer;
            _config = config.Value;
        }

        public PhaseSets Get(string participantID)
        {
            // fetch the four phases of images as previously defined and stored for given participantID (line 34)
            var encoding = _dbContext.PhaseImages.Where(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.PhaseName, "Encoding")).ToList();
            var immediate = _dbContext.PhaseImages.Where(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.PhaseName, "Immediate")).ToList();
            var delayed = _dbContext.PhaseImages.Where(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.PhaseName, "Delayed")).ToList();
            var followup = _dbContext.PhaseImages.Where(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.PhaseName, "Followup")).ToList();
            if ((!encoding.Any()) || (!immediate.Any()) || (!delayed.Any()) || (!followup.Any())) // if any of the four phases lacks images, then
            { // define and store images for all four phases
                var testImageCountPerOldType = _config.TestImageCountPerOldSubset; // production: 12; short: 2: development: 12
                var testImageCountPerNewType = _config.TestImageCountPerNewSubset; // production: 36; short: 6: development: 36
                var oldImageIndexes = GetTestImageIndexes(testImageCountPerOldType, TestOldImageTypes); // see comment near line 89
                var newImageIndexes = GetTestImageIndexes(testImageCountPerNewType, TestNewImageTypes); // see comment near line 89
                var imagesAllocatedByType = oldImageIndexes.Keys.Union(newImageIndexes.Keys).ToDictionary(x=>x, x=>0); // for each test image type (lines 22 and 23), create as 0 a "how many we've used" count number we'll use later (near line 75)
                var imageTypeCounts = new Dictionary<int, Dictionary<string, IEnumerable<string>>>{
                    {testImageCountPerOldType, oldImageIndexes} // during each test, X images should be shown per image type stored in Y (X = {testImageCountPerOldType} - see line 43) (Y = {oldImageIndexes} - see line 45)
                    , {testImageCountPerNewType, newImageIndexes} // during each test, X images should be shown per image type stored in Y (X = {testImageCountPerNewType} - see line 44) (Y = {newImageIndexes} = see line 46)
                };

                var encodingIndexes = CreateEncodingSet().ToList(); // see comment near line 102
                var immediateIndexes = CreateTestSet().ToList(); // see comment near line 82
                var delayedIndexes = CreateTestSet().ToList(); // see comment near line 82
                var followupIndexes = CreateTestSet().ToList(); // see comment near line 82

                encoding = encodingIndexes.Select(x=>new PhaseImageModel{ParticipantID=participantID,PhaseName="Encoding",Index=x}).ToList(); // convert image names into storeable data
                immediate = immediateIndexes.Select(x=>new PhaseImageModel{ParticipantID=participantID,PhaseName="Immediate",Index=x}).ToList(); // convert image names into storeable data
                delayed = delayedIndexes.Select(x=>new PhaseImageModel{ParticipantID=participantID,PhaseName="Delayed",Index=x}).ToList(); // convert image names into storeable data
                followup = followupIndexes.Select(x=>new PhaseImageModel{ParticipantID=participantID,PhaseName="Followup",Index=x}).ToList(); // convert image names into storeable data
                _dbContext.PhaseImages.RemoveRange(_dbContext.PhaseImages.Where(x=>string.Equals(participantID, x.ParticipantID))); // remove any such data for given participantID (should never exist, but just in case...)
                _dbContext.PhaseImages.AddRange(encoding); // prepare to store Encoding image names for given participantID
                _dbContext.PhaseImages.AddRange(immediate); // prepare to store Immediate image names for given participantID
                _dbContext.PhaseImages.AddRange(delayed); // prepare to store Delayed image names for given participantID
                _dbContext.PhaseImages.AddRange(followup); // prepare to store Followup image names for given participantID
                _dbContext.SaveChanges(); // store image names for given participantID

                IEnumerable<string> CreateTestSet() { // this method is used near lines 54-56
                    var testSet = new List<string>(); // create an empty list of image names, which we'll gradually fill throughout this method and eventually return to the caller
                    foreach (var (imageCount, imageIndexes) in imageTypeCounts) // for each of "old" and "new", each loop iteration will know the count of images to show per test phase and the list of image names
                    {
                        foreach (var (imageType, imageTypeIndexes) in imageIndexes) // for each image type, each loop iteration will know the image type and the randomized list of image names (from line 95)
                        {
                            var unallocatedImageTypeIndexes = imageTypeIndexes.Skip(imagesAllocatedByType[imageType]); // in the list of randomized image names, skip how many we've already used, and store the rest in {unallocatedImageTypeIndexes}
                            imagesAllocatedByType[imageType] = imagesAllocatedByType[imageType] + imageCount; // note that we're allocating {imageCount} more of the randomized image names belonging to the given {imageType} (so we know to skip them later)
                            var images = unallocatedImageTypeIndexes.Take(imageCount); // take {imageCount} image names from {unallocatedImageTypeIndexes}
                            testSet.AddRange(images); // and associate them with the set of test phase image names we're building
                        }
                    }
                    testSet = _collectionRandomizer.Randomize(testSet).ToList(); // again randomize the list of image names we've come up with
                    return testSet; // return the "test set" - a list of randomized, distinct image names ({imageCount} for each of every of both the "old" and "new" image types)
                }
            }
            var result = new PhaseSets(participantID, encoding.Select(x=>x.Index), immediate.Select(x=>x.Index), delayed.Select(x=>x.Index), followup.Select(x=>x.Index));
            return result; // return all four phases of image names to the caller
            Dictionary<string, IEnumerable<string>> GetTestImageIndexes(int imageCountPerType, IEnumerable<string> imageTypes) { // this method is used twice near line 45
                var totalNumberOfTestPhases = 3;
                // for each imageType, get a randomized set of distinct image names large enough to accomodate all test phases
                var testImageIndexes = imageTypes.ToDictionary(x=>x, x=>{
                    var imageIndexes = _imageIndexesGetter.Get(new List<ImageIndexesRequest>{new ImageIndexesRequest(x, imageCountPerType * totalNumberOfTestPhases)});
                    return imageIndexes;
                });
                return testImageIndexes; // return all image name sets, one per imagetype
            }
            IEnumerable<string> CreateEncodingSet() { // this method is used near line 53
                var encodingImageTypes = EncodingImageTypes; // defined near line 24
                var encodingImageIndexesRequests = encodingImageTypes.Select(x=>new ImageIndexesRequest(x, _config.EncodingImageCountPerSubset));
                var encodingSet = _imageIndexesGetter.Get(encodingImageIndexesRequests);
                return encodingSet; // return a single set of image names that contains X of each EncodingImageType (where x is defined in the config as EncodingImageCountPerSubset)
            }
        }
    }
}
