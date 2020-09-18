using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    [Description("Participant's consent to participate.")]
    public class ConsentModel
    {
        [Name(nameof(ID))]
        [Description("ID of the participant.")]
        public string ID { get; set; } // todo mlh ask EM if we should rename this to ParticipantID (to match every other model)
        [Name(nameof(InfoSheet))]
        [Description("Participant has read study information sheet.")]
        public bool InfoSheet { get; set; }
        [Name(nameof(Withdraw))]
        [Description("Participant understands withdraw freedom.")]
        public bool Withdraw { get; set; }
        [Name(nameof(NPSDisorder))]
        [Description(@"Participant understands ""NPS history"" participation stipulation.")]
        public bool NPSDisorder{ get; set; }
        [Name(nameof(ADHD))]
        [Description(@"Participant understands ""additional difficulties history"" participation stipulation.")]
        public bool ADHD{ get; set; } 
        [Name(nameof(HeadInjury))]
        [Description(@"Participant understands ""serious head injury"" participation stipulation.")]
        public bool HeadInjury{ get; set; }
        [Name(nameof(NormalVision))]
        [Description(@"Participant understands ""normal vision"" participation stipulation.")]
        public bool NormalVision{ get; set; }    
        [Name(nameof(VisionProblems))]
        [Description(@"Participant understands ""special visual"" participation stipulation.")]
        public bool VisionProblems{ get; set; }   
        [Name(nameof(AltShifts))]
        [Description(@"Participant understands ""alternate shift patterns"" participation stipulation.")]
        public bool AltShifts{ get; set; }
        [Name(nameof(Smoker))]
        [Description(@"Participant understands ""smoker"" participation stipulation.")]
        public bool Smoker{ get; set; }
        [Name(nameof(DataProtection))]
        [Description(@"Participant understands ""anonymised data access"" participation stipulation.")]
        public bool DataProtection{ get; set; }   
        [Name(nameof(AgreeParticipate))]
        [Description("Participant agrees to take part in the study.")]
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
