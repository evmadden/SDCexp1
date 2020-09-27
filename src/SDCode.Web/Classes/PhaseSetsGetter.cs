using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface IPhaseSetsGetter
    {
        PhaseSetsModel Get(string participantID);
    }

    public class PhaseSetsGetter : IPhaseSetsGetter
    {
        private readonly ICsvFile<PhaseSetsModel, PhaseSetsModel.Map> _phaseSetsCsvFile;
        private readonly IImageIndexesGetter _imageIndexesGetter;
        private readonly ICollectionRandomizer _collectionRandomizer;
        private readonly IConfig _config;

        public PhaseSetsGetter(ICsvFile<PhaseSetsModel, PhaseSetsModel.Map> phaseSetsCsvFile, IImageIndexesGetter imageIndexesGetter, ICollectionRandomizer collectionRandomizer, IOptions<Config> config)
        {
            _phaseSetsCsvFile = phaseSetsCsvFile;
            _imageIndexesGetter = imageIndexesGetter;
            _collectionRandomizer = collectionRandomizer;
            _config = config.Value;
        }

        public PhaseSetsModel Get(string participantID)
        {
            var phaseSets = _phaseSetsCsvFile.Read().ToList();
            var result = phaseSets.SingleOrDefault(x => string.Equals(x.ParticipantID, participantID));
            if (result == default)
            {
                var testImageCountPerOldType = _config.TestImageCountPerOldSubset;
                var testImageCountPerNewType = _config.TestImageCountPerNewSubset;
                var oldImageIndexes = GetTestImageIndexes(testImageCountPerOldType, new List<string>{"A", "AI", "D", "DI", "F", "FI"});
                var newImageIndexes = GetTestImageIndexes(testImageCountPerNewType, new List<string>{"N", "NI"});
                var imagesAllocatedByType = oldImageIndexes.Keys.Union(newImageIndexes.Keys).ToDictionary(x=>x, x=>0);
                var imageTypeCounts = new Dictionary<int, Dictionary<string, IEnumerable<string>>>{
                    {testImageCountPerOldType, oldImageIndexes}
                    , {testImageCountPerNewType, newImageIndexes}
                };

                var encoding = CreateEncodingSet();
                var immediate = CreateTestSet();
                var delayed = CreateTestSet();
                var followup = CreateTestSet();
                result = new PhaseSetsModel { ParticipantID = participantID, Encoding = encoding, Immediate = immediate, Delayed = delayed, Followup = followup };
                phaseSets.Insert(0, result);
                _phaseSetsCsvFile.Write(phaseSets);
                IEnumerable<string> CreateTestSet() {
                    var testSet = new List<string>();
                    foreach (var (imageCount, imageIndexes) in imageTypeCounts)
                    {
                        foreach (var (imageType, imageTypeIndexes) in imageIndexes)
                        {
                            var unallocatedImageTypeIndexes = imageTypeIndexes.Skip(imagesAllocatedByType[imageType]);
                            imagesAllocatedByType[imageType] = imagesAllocatedByType[imageType] + imageCount;
                            testSet.AddRange(unallocatedImageTypeIndexes.Take(imageCount));
                        }
                    }

                    testSet = _collectionRandomizer.Randomize(testSet).ToList();
                    return testSet;
                }
            }
            return result;
            Dictionary<string, IEnumerable<string>> GetTestImageIndexes(int imageCountPerType, IEnumerable<string> imageTypes) {
                var testImageIndexes = imageTypes.ToDictionary(x=>x, x=>_imageIndexesGetter.Get(new List<ImageIndexesRequest>{new ImageIndexesRequest(x, imageCountPerType * 3)}));
                return testImageIndexes;
            }
            IEnumerable<string> CreateEncodingSet() {
                var encodingImageTypes = new List<string> { "A", "A", "AI", "AI", "B", "BI", "C", "CI", "F", "F", "FI", "FI" };
                var encodingImageIndexesRequests = encodingImageTypes.Select(x=>new ImageIndexesRequest(x, _config.EncodingImageCountPerSubset));
                var encodingSet = _imageIndexesGetter.Get(encodingImageIndexesRequests);
                return encodingSet;
            }
        }
    }
}
