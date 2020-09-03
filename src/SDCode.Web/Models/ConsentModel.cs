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
    }

    public sealed class ConsentMap : ClassMap<ConsentModel>
    {
        public ConsentMap()
        {
            Map(m => m.ParticipantID).Name(nameof(ConsentModel.ParticipantID));
            Map(m => m.InfoSheet).Name(nameof(ConsentModel.InfoSheet));
            Map(m => m.Withdraw).Name(nameof(ConsentModel.Withdraw));
            Map(m => m.NPSDisorder).Name(nameof(ConsentModel.NPSDisorder));
        }
    }
}
