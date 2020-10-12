using SDCode.Web.Classes;

namespace SDCode.Web.Models.Data
{
    public class PSQIDbModel
    {
        public string ParticipantID { get; set; }
        public string MonthBed { get; set; }
        public string MonthLatency { get; set; }
        public string MonthWake { get; set; }
        public string TotalHours{ get; set; }
        public string TotalMinutes{ get; set; } 
        public FrequenciesWeekly? No30Min{ get; set; }
        public FrequenciesWeekly? WASO{ get; set; }    
        public FrequenciesWeekly? Bathroom{ get; set; }  
        public FrequenciesWeekly? Breathing{ get; set; }  
        public FrequenciesWeekly? Snoring{ get; set; }
        public FrequenciesWeekly? Hot{ get; set; }
        public FrequenciesWeekly? Cold{ get; set; }   
        public FrequenciesWeekly? Dreams{ get; set; }  
        public FrequenciesWeekly? Pain{ get; set; }    
        public FrequenciesWeekly? OtherFrequency{ get; set; }  
        public string OtherDescribe{ get; set; }       
        public Qualities? SleepQuality{ get; set; } 
        public FrequenciesWeekly? Medication{ get; set; } 
        public FrequenciesWeekly? Sleepiness{ get; set; } 
        public Problems? Enthusiasm{ get; set; } 
        public BedPartners? BedPartner{ get; set; } 
        public FrequenciesWeekly? PartSnore{ get; set; } 
        public FrequenciesWeekly? BreathPause{ get; set; } 
        public FrequenciesWeekly? Legs{ get; set; } 
        public FrequenciesWeekly? Disorientation{ get; set; } 
        public FrequenciesWeekly? OtherRestless{ get; set; } 
        public string OtherRestDescribe{ get; set; } 
    }
}
