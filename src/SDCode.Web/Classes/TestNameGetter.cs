using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ITestNameGetter
    {
        string Get(PhaseSetsModel phaseSets, int progress);
    }

    public class TestNameGetter : ITestNameGetter
    {
        public string Get(PhaseSetsModel phaseSets, int progress)
        {
            string result = default;
            if (progress >= phaseSets.Immediate.Count())
            {
                if (progress >= phaseSets.Immediate.Count() + phaseSets.Delayed.Count())
                {
                    if (progress < phaseSets.Immediate.Count() + phaseSets.Delayed.Count() + phaseSets.Followup.Count()) {
                        result = progress >= phaseSets.Immediate.Count() + phaseSets.Delayed.Count() + phaseSets.Followup.Count() ? null : nameof(phaseSets.Followup);
                    }
                }
                else
                {
                    result = nameof(phaseSets.Delayed);
                }
            }
            else
            {
                result = nameof(phaseSets.Immediate);
            }
            return result;
        }
    }
}
