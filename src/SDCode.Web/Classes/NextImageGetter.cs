using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface INextImageGetter
    {
        string Get(TestSetsModel testSets, int progress);
    }

    public class NextImageGetter : INextImageGetter
    {
        public string Get(TestSetsModel testSets, int progress)
        {
            string result;
            if (progress >= testSets.Immediate.Count())
            {
                if (progress >= testSets.Immediate.Count() + testSets.Delayed.Count())
                {
                    result = testSets.Followup.ElementAt(progress - testSets.Immediate.Count() - testSets.Delayed.Count());
                }
                else
                {
                    result = testSets.Delayed.ElementAt(progress - testSets.Immediate.Count());
                }
            }
            else
            {
                result = testSets.Immediate.ElementAt(progress);
            }
            return result;
        }
    }
}
