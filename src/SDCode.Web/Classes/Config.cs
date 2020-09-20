namespace SDCode.Web.Classes
{
        public class Config {
            public int ImageDisplayDurationInMilliseconds { get; set; }
            public int AttentionResetDisplayDurationInMilliseconds { get; set; }
            public int NumberDisplayProbabilityPercentage { get; set; }
            public int NumberCheckIntervalInMilliseconds { get; set; }
            public int NumberDisplayThresholdInMilliseconds { get; set; }
            public bool AutomateTests { get; set; }
            public int TestAutomationDelayInMilliseconds { get; }
            public int TestWaitDaysDelayed { get; }
            public int TestWaitDaysFollowup { get; }
        }
}
