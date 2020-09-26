namespace SDCode.Web.Classes
{
        public class Config {
            public int ImageDisplayDurationInMilliseconds { get; set; }
            public int AttentionResetDisplayDurationInMilliseconds { get; set; }
            public int NumberDisplayProbabilityPercentage { get; set; }
            public int NumberCheckIntervalInMilliseconds { get; set; }
            public int NumberDisplayThresholdInMilliseconds { get; set; }
            public bool AutomateTests { get; set; }
            public int TestAutomationDelayInMilliseconds { get; set; }
            public int TestWaitDelayedDays { get; set; }
            public int TestWaitFollowupDays { get; set; }
            public int TestTooLateDays { get; set; }
        }
}
