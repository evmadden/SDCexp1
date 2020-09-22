using System;
using System.Collections.Generic;
using System.Linq;
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

        public PhaseSetsGetter(ICsvFile<PhaseSetsModel, PhaseSetsModel.Map> phaseSetsCsvFile, IImageIndexesGetter imageIndexesGetter, ICollectionRandomizer collectionRandomizer)
        {
            _phaseSetsCsvFile = phaseSetsCsvFile;
            _imageIndexesGetter = imageIndexesGetter;
            _collectionRandomizer = collectionRandomizer;
        }

        public PhaseSetsModel Get(string participantID)
        {
            var phaseSets = _phaseSetsCsvFile.Read().ToList();
            var result = phaseSets.SingleOrDefault(x => string.Equals(x.ParticipantID, participantID));
            if (result == default)
            {
                var testImageCountPerOldType = 12;
                var testImageCountPerNewType = 36;
                var oldImageIndexes = GetTestImageIndexes(testImageCountPerOldType, new List<string>{"A", "AI", "D", "DI", "F", "FI"});
                var newImageIndexes = GetTestImageIndexes(testImageCountPerNewType, new List<string>{"N", "NI"});
                var imagesAllocatedByType = oldImageIndexes.Keys.Union(newImageIndexes.Keys).ToDictionary(x=>x, x=>0);;
                var imageTypeCounts = new Dictionary<int, Dictionary<string, IEnumerable<string>>>{
                    {testImageCountPerOldType, oldImageIndexes}
                    , {testImageCountPerNewType, newImageIndexes}
                };
                Func<IEnumerable<string>> createTestSet = () => {
                    var result = new List<string>();
                    foreach (var imageCountsKvp in imageTypeCounts)
                    {
                        var imageCount = imageCountsKvp.Key;
                        var imageIndexes = imageCountsKvp.Value;
                        foreach (var imageIndexesKvp in imageIndexes)
                        {
                            var imageType = imageIndexesKvp.Key;
                            var imageTypeIndexes = imageIndexesKvp.Value;
                            var unallocatedImageTypeIndexes = imageTypeIndexes.Skip(imagesAllocatedByType[imageType]);
                            imagesAllocatedByType[imageType] = imagesAllocatedByType[imageType] + imageCount;
                            result.AddRange(unallocatedImageTypeIndexes.Take(imageCount));
                        }
                    }
                    result = _collectionRandomizer.Randomize(result).ToList();
                    return result;
                };
                var encoding = CreateEncodingSet();
                var immediate = createTestSet();
                var delayed = createTestSet();
                var followup = createTestSet();
                result = new PhaseSetsModel { ParticipantID = participantID, Encoding = encoding, Immediate = immediate, Delayed = delayed, Followup = followup };
                phaseSets.Insert(0, result);
                _phaseSetsCsvFile.Write(phaseSets);
            }
            return result;
            Dictionary<string, IEnumerable<string>> GetTestImageIndexes(int imageCountPerType, IEnumerable<string> imageTypes) {
                var result = imageTypes.ToDictionary(x=>x, x=>_imageIndexesGetter.Get(new List<ImageIndexesRequest>{new ImageIndexesRequest(x, imageCountPerType * 3)}));
                return result;
            }
            IEnumerable<string> CreateEncodingSet() {
                var encodingImageTypes = new List<string> { "A", "A", "AI", "AI", "B", "BI", "C", "CI", "F", "F", "FI", "FI" };
                var encodingImageIndexesRequests = encodingImageTypes.Select(x=>new ImageIndexesRequest(x, 36));
                var result = _imageIndexesGetter.Get(encodingImageIndexesRequests);
                return result;
            }
        }
    }
}
