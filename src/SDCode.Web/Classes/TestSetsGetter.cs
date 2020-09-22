using System;
using System.Collections.Generic;
using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ITestSetsGetter
    {
        TestSetsModel Get(string participantID);
    }

    public class TestSetsGetter : ITestSetsGetter
    {
        private readonly ICsvFile<TestSetsModel, TestSetsModel.Map> _testSetsCsvFile;
        private readonly IImageIndexesGetter _imageIndexesGetter;
        private readonly ICollectionRandomizer _collectionRandomizer;

        public TestSetsGetter(ICsvFile<TestSetsModel, TestSetsModel.Map> testSetsCsvFile, IImageIndexesGetter imageIndexesGetter, ICollectionRandomizer collectionRandomizer)
        {
            _testSetsCsvFile = testSetsCsvFile;
            _imageIndexesGetter = imageIndexesGetter;
            _collectionRandomizer = collectionRandomizer;
        }

        public TestSetsModel Get(string participantID)
        {
            var testSets = _testSetsCsvFile.Read().ToList();
            var result = testSets.SingleOrDefault(x => string.Equals(x.ParticipantID, participantID));
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
                result = new TestSetsModel { ParticipantID = participantID, Encoding = encoding, Immediate = immediate, Delayed = delayed, Followup = followup };
                testSets.Insert(0, result);
                _testSetsCsvFile.Write(testSets);
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
