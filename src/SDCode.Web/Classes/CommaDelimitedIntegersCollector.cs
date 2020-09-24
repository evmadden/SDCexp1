using System.Collections.Generic;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface ICommaDelimitedIntegersCollector
    {
        IEnumerable<int> Collect(string commaDelimitedIntegers);
    }

    public class CommaDelimitedIntegersCollector : ICommaDelimitedIntegersCollector
    {
        public IEnumerable<int> Collect(string commaDelimitedIntegers)
        {
            var result = commaDelimitedIntegers?.Split(",").Select(int.Parse) ?? new List<int>();
            return result;
        }

    }
}
