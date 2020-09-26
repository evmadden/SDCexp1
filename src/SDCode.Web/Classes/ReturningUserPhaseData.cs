using System;

namespace SDCode.Web.Classes
{
    public interface IReturningUserPhaseData
    {
        ReturningUserAction Action { get; }
        int Progress { get; }
        string TestName { get; }
        DateTime? NextTestWhenUtc { get; }
    }

    public class ReturningUserPhaseData : IReturningUserPhaseData
    {
        public ReturningUserPhaseData(ReturningUserAction action, int progress, string testName, DateTime? nextTestWhenUtc)
        {
            Action = action;
            Progress = progress;
            TestName = testName;
            NextTestWhenUtc = nextTestWhenUtc;
        }

        public ReturningUserAction Action { get; private set; }
        public int Progress { get; private set; }
        public string TestName { get; private set; }
        public DateTime? NextTestWhenUtc { get; private set; }
    }
}
