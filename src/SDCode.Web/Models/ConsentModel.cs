using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class ConsentModel
    {
        [Name(nameof(ID))]
        public string ID { get; set; }
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
        [Name(nameof(VisionProblems))]
        public bool VisionProblems{ get; set; }   
        [Name(nameof(AltShifts))]
        public bool AltShifts{ get; set; }
        [Name(nameof(Smoker))]
        public bool Smoker{ get; set; }
        [Name(nameof(DataProtection))]
        public bool DataProtection{ get; set; }   
        [Name(nameof(AgreeParticipate))]
        public bool AgreeParticipate{ get; set; }            
    }

    public sealed class ConsentMap : ClassMap<ConsentModel>
    {
        public ConsentMap()
        {
            Map(m => m.ID).Name(nameof(ConsentModel.ID));
            Map(m => m.InfoSheet).Name(nameof(ConsentModel.InfoSheet));
            Map(m => m.Withdraw).Name(nameof(ConsentModel.Withdraw));
            Map(m => m.NPSDisorder).Name(nameof(ConsentModel.NPSDisorder));
            Map(m => m.ADHD).Name(nameof(ConsentModel.ADHD));
            Map(m => m.HeadInjury).Name(nameof(ConsentModel.HeadInjury));
            Map(m => m.NormalVision).Name(nameof(ConsentModel.NormalVision));
            Map(m => m.VisionProblems).Name(nameof(ConsentModel.VisionProblems));
            Map(m => m.AltShifts).Name(nameof(ConsentModel.AltShifts));
            Map(m => m.Smoker).Name(nameof(ConsentModel.Smoker));
            Map(m => m.DataProtection).Name(nameof(ConsentModel.DataProtection));
            Map(m => m.AgreeParticipate).Name(nameof(ConsentModel.AgreeParticipate));
        }
    }
}
