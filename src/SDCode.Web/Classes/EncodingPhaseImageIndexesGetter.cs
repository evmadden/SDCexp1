using System;
using System.Collections.Generic;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IEncodingPhaseImageIndexesGetter
    {
        IEnumerable<string> Get();
    }

    public class EncodingPhaseImageIndexesGetter : IEncodingPhaseImageIndexesGetter
    {
        public IEnumerable<string> Get()
        {
            var result = new List<string>();
            var targetContextSets = new Dictionary<string, IEnumerable<IEnumerable<int>>>();
            List<IEnumerable<int>> single = new List<IEnumerable<int>> { Enumerable.Range(1, 36) };
            List<IEnumerable<int>> @double = new List<IEnumerable<int>> { Enumerable.Range(1, 36), Enumerable.Range(1, 36) };
            targetContextSets["A"] = @double;
            targetContextSets["AI"] = @double;
            targetContextSets["B"] = single;
            targetContextSets["BI"] = single;
            targetContextSets["C"] = single;
            targetContextSets["CI"] = single;
            targetContextSets["F"] = @double;
            targetContextSets["FI"] = @double;
            foreach (var kvp in targetContextSets)
            {
                var imageType = kvp.Key;
                var indexSets = kvp.Value;
                foreach (var indexSet in indexSets)
                {
                    var randomizedSet = Randomize(indexSet); // randomized 1 through 36
                    var randomizedTypeIndexes = randomizedSet.Select(index => $"{imageType}{index}"); // randomized indexes, but with type letter prepended
                    result.AddRange(randomizedTypeIndexes); // add type-prepended indexes to result
                }
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
