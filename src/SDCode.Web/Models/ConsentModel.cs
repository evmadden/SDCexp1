using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    [Description("Participant's consent to participate.")]
    public class ConsentModel : IParticipantModel
    {
        [Name(nameof(ParticipantID))]
        [Description("ID of the participant.")]
        public string ParticipantID { get; set; }
        [Name(nameof(InfoSheet))]
        [Description("Participant has read study information sheet.")]
        public bool InfoSheet { get; set; }
        [Name(nameof(Withdraw))]
        [Description("Participant understands withdraw freedom.")]
        public bool Withdraw { get; set; }
        [Name(nameof(NPSDisorder))]
        [Description("Participant confirms no neurological, psychiatric or sleep problems.")]
        public bool NPSDisorder{ get; set; }
        [Name(nameof(ADHD))]
        [Description("Participant confirms no attentional difficulties.")]
        public bool ADHD{ get; set; } 
        [Name(nameof(HeadInjury))]
        [Description("Participant confirms no serious head injuries.")]
        public bool HeadInjury{ get; set; }
        [Name(nameof(NormalVision))]
        [Description("Participant confirms normal vision.")]
        public bool NormalVision{ get; set; }    
        [Name(nameof(VisionProblems))]
        [Description("Participant confirms no speical visual characteristics.")]
        public bool VisionProblems{ get; set; }   
        [Name(nameof(AltShifts))]
        [Description("Participant confirms they do not work alternating shift patterns.")]
        public bool AltShifts{ get; set; }
        [Name(nameof(Smoker))]
        [Description("Participant confirms they are not a smoker.")]
        public bool Smoker{ get; set; }
        [Name(nameof(DataProtection))]
        [Description("Participant understands that data will be anonymised and accessed by researchers.")]
        public bool DataProtection{ get; set; }   
        [Name(nameof(AgreeParticipate))]
        [Description("Participant agrees to take part in the study.")]
        public bool AgreeParticipate{ get; set; }            

        public sealed class Map : ClassMap<ConsentModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(ConsentModel.ParticipantID));
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
}
