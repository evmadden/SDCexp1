using System;
using System.Collections.Generic;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IImageIndexesGetter
    {
        IEnumerable<string> Get(IEnumerable<ImageIndexesRequest> imageSetTypes);
    }

    public class ImageIndexesGetter : IImageIndexesGetter
    {
        private ICollectionRandomizer _collectionRandomizer;

        public ImageIndexesGetter(ICollectionRandomizer collectionRandomizer)
        {
            _collectionRandomizer = collectionRandomizer;
        }

        public IEnumerable<string> Get(IEnumerable<ImageIndexesRequest> requests)
        {
            var result = new List<string>();
            foreach (var request in requests)
            {
                var numerals = Enumerable.Range(1, request.Count); // 1 through request.Count
                var typeIndexes = numerals.Select(index => $"{request.Type}{index}"); // numerals, but with type letter prepended
                result.AddRange(typeIndexes); // add type-prepended indexes to result
            }
            result = _collectionRandomizer.Randomize(result).ToList(); // randomize final result
            return result;
        }
    }

    public class ImageIndexesRequest
    {
        public ImageIndexesRequest(string type, int count)
        {
            Type = type;
            Count = count;
        }

        public string Type { get; }
        public int Count { get; }
    }
}
