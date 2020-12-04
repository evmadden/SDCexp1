namespace SDCode.Web.Classes
{
    public interface IConfig
    {
        int ImageDisplayDurationInMilliseconds { get; set; }
        int AttentionResetDisplayDurationInMilliseconds { get; set; }
        int NumberDisplayProbabilityPercentage { get; set; }
        int NumberCheckIntervalInMilliseconds { get; set; }
        int NumberDisplayThresholdInMilliseconds { get; set; }
        bool AutomateTests { get; set; }
        int TestAutomationDelayInMilliseconds { get; set; }
        int TestWaitDelayedDays { get; set; }
        int TestWaitFollowupDays { get; set; }
        int TestStartTimePlusMinusMinutes { get; set; }
        int EncodingImageCountPerSubset { get; set; }
        int TestImageCountPerOldSubset { get; set; }
        int TestImageCountPerNewSubset { get; set; }
        string ImageTypesUrlTemplate { get; set; }
        bool NotificationsEnabled { get; set; }
        string NotificationsFromAddress { get; set; }
        string NotificationsFromName { get; set; }
        string NotificationsToAddress { get; set; }
        string NotificationsToName { get; set; }
    }

    public class Config : IConfig
    {
        public int ImageDisplayDurationInMilliseconds { get; set; }
        public int AttentionResetDisplayDurationInMilliseconds { get; set; }
        public int NumberDisplayProbabilityPercentage { get; set; }
        public int NumberCheckIntervalInMilliseconds { get; set; }
        public int NumberDisplayThresholdInMilliseconds { get; set; }
        public bool AutomateTests { get; set; }
        public int TestAutomationDelayInMilliseconds { get; set; }
        public int TestWaitDelayedDays { get; set; }
        public int TestWaitFollowupDays { get; set; }
        public int TestStartTimePlusMinusMinutes { get; set; }
        public int EncodingImageCountPerSubset { get; set; }
        public int TestImageCountPerOldSubset { get; set; }
        public int TestImageCountPerNewSubset { get; set; }
        public string ImageTypesUrlTemplate { get; set; }
        public bool NotificationsEnabled { get; set; }
        public string NotificationsFromAddress { get; set; }
        public string NotificationsFromName { get; set; }
        public string NotificationsToAddress { get; set; }
        public string NotificationsToName { get; set; }
    }
}
