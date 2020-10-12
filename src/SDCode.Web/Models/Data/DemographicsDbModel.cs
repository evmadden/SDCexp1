using SDCode.Web.Classes;

namespace SDCode.Web.Models.Data
{
    public class DemographicsDbModel
    {
        public string ParticipantID { get; set; }
        public Sexes? Sex { get; set; }
        public string Age { get; set; }
        public string YearStudy { get; set; }
        public Hands? Handed{ get; set; } 
        public bool? Impairments{ get; set; } 
        public bool? Glasses{ get; set; }
        public string Language{ get; set; }     
        public string Bilingual{ get; set; }    
        public string CurrentCountry{ get; set; }
    }
}
