using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class StanfordModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(Stanford))]
        public string Stanford { get; set; }

        //Not sure if we need these or not, so they are going in here just in case
        [Name(nameof(StanfordImmediate))]
        public string StanfordImmediate { get; set; }
        [Name(nameof(StanfordDelayed))]
        public string StanfordDelayed { get; set; }
        [Name(nameof(StanfordFollowUp))]
        public string StanfordFollowUp { get; set; }
    }   
    public sealed class StanfordMap : ClassMap<StanfordModel>
    {
        public StanfordMap()
        {
            Map(m => m.ParticipantID).Name(nameof(StanfordModel.ParticipantID));
            Map(m => m.Stanford).Name(nameof(StanfordModel.Stanford));

            //Not sure if we need these or not, so they are going in here just in case
            Map(m => m.StanfordImmediate).Name(nameof(StanfordModel.StanfordImmediate));
            Map(m => m.StanfordDelayed).Name(nameof(StanfordModel.StanfordDelayed));
            Map(m => m.StanfordFollowUp).Name(nameof(StanfordModel.StanfordFollowUp));
        }
    }
}
