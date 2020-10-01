using System.Collections.Generic;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class EncodingIndexViewModel
    {
        public EncodingIndexViewModel(string participantID, Sleepinesses? stanford, int imageDisplayDurationInMilliseconds, int plusSignDisplayDurationInMilliseconds, int numberDisplayProbabilityPercentage, int numberCheckIntervalInMilliseconds, int numberDisplayThresholdInMilliseconds, IEnumerable<string> imageTypesToPreload) {
            ParticipantID = participantID;
            Stanford = stanford;
            ImageDisplayDurationInMilliseconds = imageDisplayDurationInMilliseconds;
            PlusSignDisplayDurationInMilliseconds = plusSignDisplayDurationInMilliseconds;
            NumberDisplayProbabilityPercentage = numberDisplayProbabilityPercentage;
            NumberCheckIntervalInMilliseconds = numberCheckIntervalInMilliseconds;
            NumberDisplayThresholdInMilliseconds = numberDisplayThresholdInMilliseconds;
            ImageTypesToPreload = imageTypesToPreload;
        }

        public string ParticipantID { get; }
        public Sleepinesses? Stanford { get; }
        public int ImageDisplayDurationInMilliseconds { get; }
        public int PlusSignDisplayDurationInMilliseconds { get; }
        public int NumberDisplayProbabilityPercentage { get; }
        public int NumberCheckIntervalInMilliseconds { get; }
        public int NumberDisplayThresholdInMilliseconds { get; }
        public IEnumerable<string> ImageTypesToPreload { get; }
    }
}