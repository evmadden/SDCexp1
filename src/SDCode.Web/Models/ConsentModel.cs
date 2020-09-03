using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class ConsentModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(InfoSheet))]
        public bool InfoSheet { get; set; }
        [Name(nameof(Withdraw))]
        public bool Withdraw { get; set; }
        [Name(nameof(NPSDisorder))]
        public bool NPSDisorder{ get; set; }
        [Name(nameof(ADHD))]
        public bool ADHD{ get; set; } 
        [Name(nameof(HeadInjury))]
        public bool HeadInjury{ get; set; }
        [Name(nameof(NormalVision))]
        public bool NormalVision{ get; set; }        
    }

    public sealed class ConsentMap : ClassMap<ConsentModel>
    {
        public ConsentMap()
        {
            Map(m => m.ParticipantID).Name(nameof(ConsentModel.ParticipantID));
            Map(m => m.InfoSheet).Name(nameof(ConsentModel.InfoSheet));
            Map(m => m.Withdraw).Name(nameof(ConsentModel.Withdraw));
            Map(m => m.NPSDisorder).Name(nameof(ConsentModel.NPSDisorder));
            Map(m => m.ADHD).Name(nameof(ConsentModel.ADHD));
            Map(m => m.HeadInjury).Name(nameof(ConsentModel.HeadInjury));
            Map(m => m.NormalVision).Name(nameof(ConsentModel.NormalVision));
        }
    }
}
