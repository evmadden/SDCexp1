using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class PSQIModel : IParticipantModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
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
        public FrequenciesWeekly? No30Min{ get; set; }
        [Name(nameof(WASO))]
        public FrequenciesWeekly? WASO{ get; set; }    
        [Name(nameof(Bathroom))]
        public FrequenciesWeekly? Bathroom{ get; set; }  
        [Name(nameof(Breathing))]
        public FrequenciesWeekly? Breathing{ get; set; }  
        [Name(nameof(Snoring))]
        public FrequenciesWeekly? Snoring{ get; set; }
        [Name(nameof(Hot))]
        public FrequenciesWeekly? Hot{ get; set; }
        [Name(nameof(Cold))]
        public FrequenciesWeekly? Cold{ get; set; }   
        [Name(nameof(Dreams))]
        public FrequenciesWeekly? Dreams{ get; set; }  
        [Name(nameof(Pain))]
        public FrequenciesWeekly? Pain{ get; set; }    
        [Name(nameof(OtherFrequency))]
        public FrequenciesWeekly? OtherFrequency{ get; set; }  
        [Name(nameof(OtherDescribe))]
        public string OtherDescribe{ get; set; }       
        [Name(nameof(SleepQuality))]
        public Qualities? SleepQuality{ get; set; } 
        [Name(nameof(Medication))]
        public FrequenciesWeekly? Medication{ get; set; } 
        [Name(nameof(Sleepiness))]
        public FrequenciesWeekly? Sleepiness{ get; set; } 
        [Name(nameof(Enthusiasm))]
        public Problems? Enthusiasm{ get; set; } 
        [Name(nameof(BedPartner))]
        public BedPartners? BedPartner{ get; set; } 
        [Name(nameof(PartSnore))]
        public FrequenciesWeekly? PartSnore{ get; set; } 
        [Name(nameof(BreathPause))]
        public FrequenciesWeekly? BreathPause{ get; set; } 
        [Name(nameof(Legs))]
        public FrequenciesWeekly? Legs{ get; set; } 
        [Name(nameof(Disorientation))]
        public FrequenciesWeekly? Disorientation{ get; set; } 
        [Name(nameof(OtherRestless))]
        public FrequenciesWeekly? OtherRestless{ get; set; } 
        [Name(nameof(OtherRestDescribe))]
        public string OtherRestDescribe{ get; set; } 
    }

    public sealed class PSQIMap : ClassMap<PSQIModel>
    {
        public PSQIMap()
        {
            Map(m => m.ParticipantID).Name(nameof(PSQIModel.ParticipantID));
            Map(m => m.MonthBed).Name(nameof(PSQIModel.MonthBed));
            Map(m => m.MonthLatency).Name(nameof(PSQIModel.MonthLatency));
            Map(m => m.MonthWake).Name(nameof(PSQIModel.MonthWake));
            Map(m => m.TotalHours).Name(nameof(PSQIModel.TotalHours));
            Map(m => m.TotalMinutes).Name(nameof(PSQIModel.TotalMinutes));
            Map(m => m.No30Min).Name(nameof(PSQIModel.No30Min)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.WASO).Name(nameof(PSQIModel.WASO)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Bathroom).Name(nameof(PSQIModel.Bathroom)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Breathing).Name(nameof(PSQIModel.Breathing)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Snoring).Name(nameof(PSQIModel.Snoring)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Hot).Name(nameof(PSQIModel.Hot)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Cold).Name(nameof(PSQIModel.Cold)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Dreams).Name(nameof(PSQIModel.Dreams)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Pain).Name(nameof(PSQIModel.Pain)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.OtherFrequency).Name(nameof(PSQIModel.OtherFrequency)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.OtherDescribe).Name(nameof(PSQIModel.OtherDescribe));
            Map(m => m.SleepQuality).Name(nameof(PSQIModel.SleepQuality)).TypeConverter<CsvQualitiesConverter>();
            Map(m => m.Medication).Name(nameof(PSQIModel.Medication)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Sleepiness).Name(nameof(PSQIModel.Sleepiness)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Enthusiasm).Name(nameof(PSQIModel.Enthusiasm)).TypeConverter<CsvProblemsConverter>();
            Map(m => m.BedPartner).Name(nameof(PSQIModel.BedPartner)).TypeConverter<CsvBedPartnersConverter>();
            Map(m => m.PartSnore).Name(nameof(PSQIModel.PartSnore)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.BreathPause).Name(nameof(PSQIModel.BreathPause)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Legs).Name(nameof(PSQIModel.Legs)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.Disorientation).Name(nameof(PSQIModel.Disorientation)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.OtherRestless).Name(nameof(PSQIModel.OtherRestless)).TypeConverter<CsvFrequenciesWeeklyConverter>();
            Map(m => m.OtherRestDescribe).Name(nameof(PSQIModel.OtherRestDescribe));
        }
    }
}
