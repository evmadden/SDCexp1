using System;
using System.Collections.Generic;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IImageIndexesGetter
    {
        IEnumerable<string> Get(IEnumerable<string> imageSetTypes);
    }

    public class ImageIndexesGetter : IImageIndexesGetter
    {
        public IEnumerable<string> Get(IEnumerable<string> imageSetTypes)
        {
            var result = new List<string>();
            foreach (var imageSetType in imageSetTypes)
            {
                var randomizedSet = Randomize(Enumerable.Range(1, 36)); // randomized 1 through 36
                var randomizedTypeIndexes = randomizedSet.Select(index => $"{imageSetType}{index}"); // randomized indexes, but with type letter prepended
                result.AddRange(randomizedTypeIndexes); // add type-prepended indexes to result
            }
            result = Randomize(result).ToList(); // randomize final result
            return result;
        }

        private IEnumerable<T> Randomize<T>(IEnumerable<T> elements)
        {
            var random = new Random();
            var result = elements
                .Select(i => new { key = random.Next(), i })
                .OrderBy(tmp => tmp.key)
                .Select(tmp => tmp.i);
            return result;
        }
    }
}
