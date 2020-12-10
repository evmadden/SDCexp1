namespace SDCode.Web.Models.Data
{
    public class ConsentDbModel
    {
        public string ParticipantID { get; set; }
        public bool InfoSheet { get; set; }
        public bool Withdraw { get; set; }
        public bool NPSDisorder{ get; set; }
        public bool ADHD{ get; set; } 
        public bool HeadInjury{ get; set; }
        public bool NormalVision{ get; set; }    
        public bool VisionProblems{ get; set; }   
        public bool AltShifts{ get; set; }
        public bool? AgreeLanguage{ get; set; }
        public bool DataProtection{ get; set; }   
        public bool AgreeParticipate{ get; set; }            
    }
}
