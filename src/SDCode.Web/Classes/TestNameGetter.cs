using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ITestNameGetter
    {
        string Get(TestSetsModel testSets, int progress);
    }

    public class TestNameGetter : ITestNameGetter
    {
        public string Get(TestSetsModel testSets, int progress)
        {
            string result = default;
            if (progress >= testSets.Immediate.Count())
            {
                if (progress >= testSets.Immediate.Count() + testSets.Delayed.Count())
                {
                    if (progress < testSets.Immediate.Count() + testSets.Delayed.Count() + testSets.Followup.Count()) {
                        result = progress >= testSets.Immediate.Count() + testSets.Delayed.Count() + testSets.Followup.Count() ? null : nameof(testSets.Followup);
                    }
                }
                else
                {
                    result = nameof(testSets.Delayed);
                }
            }
            else
            {
                result = nameof(testSets.Immediate);
            }
            return result;
        }
    }
}
