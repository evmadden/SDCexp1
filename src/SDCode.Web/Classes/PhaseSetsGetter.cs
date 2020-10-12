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
            var encoding = _dbContext.PhaseImages.Where(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.PhaseName, "Encoding")).ToList();
            var immediate = _dbContext.PhaseImages.Where(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.PhaseName, "Immediate")).ToList();
            var delayed = _dbContext.PhaseImages.Where(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.PhaseName, "Delayed")).ToList();
            var followup = _dbContext.PhaseImages.Where(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.PhaseName, "Followup")).ToList();
            if ((!encoding.Any()) || (!immediate.Any()) || (!delayed.Any()) || (!followup.Any()))
            {
                var testImageCountPerOldType = _config.TestImageCountPerOldSubset;
                var testImageCountPerNewType = _config.TestImageCountPerNewSubset;
                var oldImageIndexes = GetTestImageIndexes(testImageCountPerOldType, TestOldImageTypes);
                var newImageIndexes = GetTestImageIndexes(testImageCountPerNewType, TestNewImageTypes);
                var imagesAllocatedByType = oldImageIndexes.Keys.Union(newImageIndexes.Keys).ToDictionary(x=>x, x=>0);
                var imageTypeCounts = new Dictionary<int, Dictionary<string, IEnumerable<string>>>{
                    {testImageCountPerOldType, oldImageIndexes}
                    , {testImageCountPerNewType, newImageIndexes}
                };

                var encodingIndexes = CreateEncodingSet().ToList();
                var immediateIndexes = CreateTestSet().ToList();
                var delayedIndexes = CreateTestSet().ToList();
                var followupIndexes = CreateTestSet().ToList();

                encoding = encodingIndexes.Select(x=>new PhaseImageModel{ParticipantID=participantID,PhaseName="Encoding",Index=x}).ToList();
                immediate = immediateIndexes.Select(x=>new PhaseImageModel{ParticipantID=participantID,PhaseName="Immediate",Index=x}).ToList();
                delayed = delayedIndexes.Select(x=>new PhaseImageModel{ParticipantID=participantID,PhaseName="Delayed",Index=x}).ToList();
                followup = followupIndexes.Select(x=>new PhaseImageModel{ParticipantID=participantID,PhaseName="Followup",Index=x}).ToList();
                _dbContext.PhaseImages.RemoveRange(_dbContext.PhaseImages.Where(x=>string.Equals(participantID, x.ParticipantID)));
                _dbContext.PhaseImages.AddRange(encoding);
                _dbContext.PhaseImages.AddRange(immediate);
                _dbContext.PhaseImages.AddRange(delayed);
                _dbContext.PhaseImages.AddRange(followup);
                _dbContext.SaveChanges();

                IEnumerable<string> CreateTestSet() {
                    var testSet = new List<string>();
                    foreach (var (imageCount, imageIndexes) in imageTypeCounts)
                    {
                        foreach (var (imageType, imageTypeIndexes) in imageIndexes)
                        {
                            var unallocatedImageTypeIndexes = imageTypeIndexes.Skip(imagesAllocatedByType[imageType]);
                            imagesAllocatedByType[imageType] = imagesAllocatedByType[imageType] + imageCount;
                            var images = unallocatedImageTypeIndexes.Take(imageCount);
                            testSet.AddRange(images);
                        }
                    }

                    testSet = _collectionRandomizer.Randomize(testSet).ToList();
                    return testSet;
                }
            }
            var result = new PhaseSets(participantID, encoding.Select(x=>x.Index), immediate.Select(x=>x.Index), delayed.Select(x=>x.Index), followup.Select(x=>x.Index));
            return result;
            Dictionary<string, IEnumerable<string>> GetTestImageIndexes(int imageCountPerType, IEnumerable<string> imageTypes) {
                var testImageIndexes = imageTypes.ToDictionary(x=>x, x=>{
                    var imageIndexes = _imageIndexesGetter.Get(new List<ImageIndexesRequest>{new ImageIndexesRequest(x, imageCountPerType * 3)});
                    return imageIndexes;
                });
                return testImageIndexes;
            }
            IEnumerable<string> CreateEncodingSet() {
                var encodingImageTypes = EncodingImageTypes;
                var encodingImageIndexesRequests = encodingImageTypes.Select(x=>new ImageIndexesRequest(x, _config.EncodingImageCountPerSubset));
                var encodingSet = _imageIndexesGetter.Get(encodingImageIndexesRequests);
                return encodingSet;
            }
        }
    }
}
