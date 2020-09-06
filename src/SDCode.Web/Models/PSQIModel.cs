using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class PSQIModel
    {
        [Name(nameof(MonthBed))]
        public string MonthBed { get; set; }
        [Name(nameof(MonthLatency))]
        public string MonthLatency { get; set; }
        [Name(nameof(MonthWake))]
        public string MonthWake { get; set; }
        [Name(nameof(TotalHours))]
        public string TotalHours{ get; set; }
        [Name(nameof(TotalMinutes))]
        public string TotalMinutes{ get; set; } 
        [Name(nameof(No30Min))]
        public string No30Min{ get; set; }
        [Name(nameof(WASO))]
        public string WASO{ get; set; }    
        [Name(nameof(Bathroom))]
        public string Bathroom{ get; set; }  
        [Name(nameof(Breathing))]
        public string Breathing{ get; set; }  
        [Name(nameof(Snoring))]
        public string Snoring{ get; set; }
        [Name(nameof(Hot))]
        public string Hot{ get; set; }
        [Name(nameof(Cold))]
        public string Cold{ get; set; }   
        [Name(nameof(Dreams))]
        public string Dreams{ get; set; }  
        [Name(nameof(Pain))]
        public string Pain{ get; set; }    
        [Name(nameof(OtherFrequency))]
        public string OtherFrequency{ get; set; }  
        [Name(nameof(OtherDescribe))]
        public string OtherDescribe{ get; set; }       
        [Name(nameof(SleepQuality))]
        public string SleepQuality{ get; set; } 
        [Name(nameof(Medication))]
        public string Medication{ get; set; } 
        [Name(nameof(Sleepiness))]
        public string Sleepiness{ get; set; } 
        [Name(nameof(Enthusiasm))]
        public string Enthusiasm{ get; set; } 
        [Name(nameof(BedPartner))]
        public string BedPartner{ get; set; } 
        [Name(nameof(PartSnore))]
        public string PartSnore{ get; set; } 
        [Name(nameof(BreathPause))]
        public string BreathPause{ get; set; } 
        [Name(nameof(Legs))]
        public string Legs{ get; set; } 
        [Name(nameof(Disorientation))]
        public string Disorientation{ get; set; } 
        [Name(nameof(OtherRestless))]
        public string OtherRestless{ get; set; } 
        [Name(nameof(OtherRestDescribe))]
        public string OtherRestDescribe{ get; set; } 
    }

    public sealed class PSQIMap : ClassMap<PSQIModel>
    {
        public PSQIMap()
        {
            Map(m => m.MonthBed).Name(nameof(PSQIModel.MonthBed));
            Map(m => m.MonthLatency).Name(nameof(PSQIModel.MonthLatency));
            Map(m => m.MonthWake).Name(nameof(PSQIModel.MonthWake));
            Map(m => m.TotalHours).Name(nameof(PSQIModel.TotalHours));
            Map(m => m.TotalMinutes).Name(nameof(PSQIModel.TotalMinutes));
            Map(m => m.No30Min).Name(nameof(PSQIModel.No30Min));
            Map(m => m.WASO).Name(nameof(PSQIModel.WASO));
            Map(m => m.Bathroom).Name(nameof(PSQIModel.Bathroom));
            Map(m => m.Breathing).Name(nameof(PSQIModel.Breathing));
            Map(m => m.Snoring).Name(nameof(PSQIModel.Snoring));
            Map(m => m.Hot).Name(nameof(PSQIModel.Hot));
            Map(m => m.Cold).Name(nameof(PSQIModel.Cold));
            Map(m => m.Dreams).Name(nameof(PSQIModel.Dreams));
            Map(m => m.Pain).Name(nameof(PSQIModel.Pain));
            Map(m => m.OtherFrequency).Name(nameof(PSQIModel.OtherFrequency));
            Map(m => m.OtherDescribe).Name(nameof(PSQIModel.OtherDescribe));
            Map(m => m.SleepQuality).Name(nameof(PSQIModel.SleepQuality));
            Map(m => m.Medication).Name(nameof(PSQIModel.Medication));
            Map(m => m.Sleepiness).Name(nameof(PSQIModel.Sleepiness));
            Map(m => m.Enthusiasm).Name(nameof(PSQIModel.Enthusiasm));
            Map(m => m.BedPartner).Name(nameof(PSQIModel.BedPartner));
            Map(m => m.PartSnore).Name(nameof(PSQIModel.PartSnore));
            Map(m => m.BreathPause).Name(nameof(PSQIModel.BreathPause));
            Map(m => m.Legs).Name(nameof(PSQIModel.Legs));
            Map(m => m.Disorientation).Name(nameof(PSQIModel.Disorientation));
            Map(m => m.OtherRestless).Name(nameof(PSQIModel.OtherRestless));
            Map(m => m.OtherRestDescribe).Name(nameof(PSQIModel.OtherRestDescribe));
        }
    }
}
