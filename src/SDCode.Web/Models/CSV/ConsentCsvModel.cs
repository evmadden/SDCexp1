using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models.CSV
{
    [Description("Participant's consent to participate.")]
    public class ConsentCsvModel
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
        [Name(nameof(DataProtection))]
        [Description("Participant understands that data will be anonymised and accessed by researchers.")]
        public bool DataProtection{ get; set; }   
        [Name(nameof(AgreeLanguage))]
        [Description("Participant confirms native English speaker.")]
        public bool? AgreeLanguage{ get; set; }      
        [Name(nameof(AgreeParticipate))]
        [Description("Participant agrees to take part in the study.")]
        public bool AgreeParticipate{ get; set; }            

        public sealed class Map : ClassMap<ConsentCsvModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.InfoSheet).Name(nameof(InfoSheet));
                Map(m => m.Withdraw).Name(nameof(Withdraw));
                Map(m => m.NPSDisorder).Name(nameof(NPSDisorder));
                Map(m => m.ADHD).Name(nameof(ADHD));
                Map(m => m.HeadInjury).Name(nameof(HeadInjury));
                Map(m => m.NormalVision).Name(nameof(NormalVision));
                Map(m => m.VisionProblems).Name(nameof(VisionProblems));
                Map(m => m.AltShifts).Name(nameof(AltShifts));
                Map(m => m.DataProtection).Name(nameof(DataProtection));
                Map(m => m.AgreeLanguage).Name(nameof(AgreeLanguage)).TypeConverter<CsvBooleanConverter>();
                Map(m => m.AgreeParticipate).Name(nameof(AgreeParticipate));
            }
        }
    }
}
