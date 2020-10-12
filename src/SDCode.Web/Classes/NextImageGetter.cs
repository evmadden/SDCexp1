using System.Linq;
using SDCode.Web.Classes;

namespace SDCode.Web.Classes
{
    public interface INextImageGetter
    {
        string Get(PhaseSets phaseSets, int progress);
    }

    public class NextImageGetter : INextImageGetter
    {
        public string Get(PhaseSets phaseSets, int progress)
        {
            string result;
            if (progress > phaseSets.Immediate.Count() - 1)
            {
                if (progress > (phaseSets.Immediate.Count() + phaseSets.Delayed.Count()) - 1)
                {
                    result = phaseSets.Followup.ElementAt(progress - phaseSets.Immediate.Count() - phaseSets.Delayed.Count());
                }
                else
                {
                    result = phaseSets.Delayed.ElementAt(progress - phaseSets.Immediate.Count());
                }
            }
            else
            {
                result = phaseSets.Immediate.ElementAt(progress);
            }
            return result;
        }
    }
}
