using System;
using System.Collections.Generic;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface ICollectionRandomizer
    {
        IEnumerable<T> Randomize<T>(IEnumerable<T> collection);
    }

    public class CollectionRandomizer : ICollectionRandomizer
    {
        public IEnumerable<T> Randomize<T>(IEnumerable<T> collection)
        {
            var random = new Random();
            var result = collection
                .Select(i => new { key = random.Next(), i })
                .OrderBy(tmp => tmp.key)
                .Select(tmp => tmp.i);
            return result;
        }

    }
}
