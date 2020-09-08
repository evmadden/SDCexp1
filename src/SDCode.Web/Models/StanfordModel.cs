using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class StanfordModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }

        [Name(nameof (Immediate))] 
        public string Immediate { get; set; }
        [Name(nameof(Delayed))]
        public string Delayed { get; set; }
        [Name(nameof(FollowUp))]
        public string FollowUp { get; set; }
    }   
    public sealed class StanfordMap : ClassMap<StanfordModel>
    {
        public StanfordMap()
        {
            Map(m => m.ParticipantID).Name(nameof(StanfordModel.ParticipantID));
            Map(m => m.Immediate).Name(nameof(StanfordModel.Immediate));
            Map(m => m.Delayed).Name(nameof(StanfordModel.Delayed));
            Map(m => m.FollowUp).Name(nameof(StanfordModel.FollowUp));
        }
    }
}
