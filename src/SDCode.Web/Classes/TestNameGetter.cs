using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ITestNameGetter
    {
        string Get(string participantID);
        string Get(PhaseSetsModel phaseSets, int progress);
    }

    public class TestNameGetter : ITestNameGetter
    {
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly IProgressGetter _progressGetter;

        public TestNameGetter(IPhaseSetsGetter phaseSetsGetter, IProgressGetter progressGetter)
        {
            _phaseSetsGetter = phaseSetsGetter;
            _progressGetter = progressGetter;
        }

        public string Get(string participantID) {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var progress = _progressGetter.Get(participantID);
            var result = Get(phaseSets, progress);
            return result;
        }
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
