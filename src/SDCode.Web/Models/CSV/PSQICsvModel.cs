using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models.CSV
{
    public class PSQICsvModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(MonthBed))]
        [Description("Q1.  Average bed time during the past month.")]
        public string MonthBed { get; set; }
        [Name(nameof(MonthLatency))]
        [Description("Q2.  Average time taken to fall asleep during the past month.")]
        public string MonthLatency { get; set; }
        [Name(nameof(MonthWake))]
        [Description("Q3.  Average wake time during the past month.")]
        public string MonthWake { get; set; }
        [Name(nameof(TotalHours))]
        [Description("Q4.  Average total hours of sleep during the past month.")]
        public string TotalHours{ get; set; }
        [Name(nameof(TotalMinutes))]
        [Description("Q4.  Combines with TotalHours to provide minutes if TST is not a whole number.")]
        public string TotalMinutes{ get; set; } 
        [Name(nameof(No30Min))]
        [Description("Q5. Could not fall asleep within 30 minutes.")]
        public FrequenciesWeekly? No30Min{ get; set; }
        [Name(nameof(WASO))]
        [Description("Q6. Wake in the middle of night or early morning.")]
        public FrequenciesWeekly? WASO{ get; set; }    
        [Name(nameof(Bathroom))]
        [Description("Q7. Have to get up to use the bathroom.")]
        public FrequenciesWeekly? Bathroom{ get; set; }  
        [Name(nameof(Breathing))]
        [Description("Q8. Cannot breathe comfortably.")]
        public FrequenciesWeekly? Breathing{ get; set; }  
        [Name(nameof(Snoring))]
        [Description("Q9. Cough or snore loudly.")]
        public FrequenciesWeekly? Snoring{ get; set; }
        [Name(nameof(Hot))]
        [Description("Q10. Feel too hot.")]
        public FrequenciesWeekly? Hot{ get; set; }
        [Name(nameof(Cold))]
        [Description("Q11. Feel too cold.")]
        public FrequenciesWeekly? Cold{ get; set; }   
        [Name(nameof(Dreams))]
        [Description("Q12.  Had bad dreams.")]
        public FrequenciesWeekly? Dreams{ get; set; }  
        [Name(nameof(Pain))]
        [Description("Q13. Have pain.")]
        public FrequenciesWeekly? Pain{ get; set; }    
        [Name(nameof(OtherFrequency))]
        [Description("Q14. Other reasons.")]
        public FrequenciesWeekly? OtherFrequency{ get; set; }  
        [Name(nameof(OtherDescribe))]
        [Description("Q14. Other reasons description.")]
        public string OtherDescribe{ get; set; }       
        [Name(nameof(SleepQuality))]
        [Description("Q15. Rating of overall sleep quality.")]
        public Qualities? SleepQuality{ get; set; } 
        [Name(nameof(Medication))]
        [Description("Q16. Use of medication to help sleep.")]
        public FrequenciesWeekly? Medication{ get; set; } 
        [Name(nameof(Sleepiness))]
        [Description("Q17. Difficulty staying awake while driving, eating meals, engaging in social activity.")]
        public FrequenciesWeekly? Sleepiness{ get; set; } 
        [Name(nameof(Enthusiasm))]
        [Description("Q18. Difficulty keeping enough enthusiasm to get things done.")]
        public Problems? Enthusiasm{ get; set; } 
        [Name(nameof(BedPartner))]
        [Description("Q19. Does the participant have a bed partner or roommate?")]
        public BedPartners? BedPartner{ get; set; } 
        [Name(nameof(PartSnore))]
        [Description("Q20a. How often bed partner would report the participant snores loudly.")]
        public FrequenciesWeekly? PartSnore{ get; set; } 
        [Name(nameof(BreathPause))]
        [Description("Q20b. How often bed partner would report the participant takes long pauses between breaths while asleep.")]
        public FrequenciesWeekly? BreathPause{ get; set; } 
        [Name(nameof(Legs))]
        [Description("Q20c. How often bed partner would report the participant's legs twitch or jerk while asleep.")]
        public FrequenciesWeekly? Legs{ get; set; } 
        [Name(nameof(Disorientation))]
        [Description("Q20d. How often bed partner would report the participant experiences episodes of disorientation or confusion during sleep.")]
        public FrequenciesWeekly? Disorientation{ get; set; } 
        [Name(nameof(OtherRestless))]
        [Description("Q20e. How often bed partner would report the participant experiences other restlessness while asleep.")]
        public FrequenciesWeekly? OtherRestless{ get; set; } 
        [Name(nameof(OtherRestDescribe))]
        [Description("Q20e. Description of the other restlessness during sleep.")]
        public string OtherRestDescribe{ get; set; } 

        public sealed class Map : ClassMap<PSQICsvModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.MonthBed).Name(nameof(MonthBed));
                Map(m => m.MonthLatency).Name(nameof(MonthLatency));
                Map(m => m.MonthWake).Name(nameof(MonthWake));
                Map(m => m.TotalHours).Name(nameof(TotalHours));
                Map(m => m.TotalMinutes).Name(nameof(TotalMinutes));
                Map(m => m.No30Min).Name(nameof(No30Min)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.WASO).Name(nameof(WASO)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Bathroom).Name(nameof(Bathroom)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Breathing).Name(nameof(Breathing)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Snoring).Name(nameof(Snoring)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Hot).Name(nameof(Hot)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Cold).Name(nameof(Cold)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Dreams).Name(nameof(Dreams)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Pain).Name(nameof(Pain)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.OtherFrequency).Name(nameof(OtherFrequency)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.OtherDescribe).Name(nameof(OtherDescribe));
                Map(m => m.SleepQuality).Name(nameof(SleepQuality)).TypeConverter<CsvQualitiesConverter>();
                Map(m => m.Medication).Name(nameof(Medication)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Sleepiness).Name(nameof(Sleepiness)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Enthusiasm).Name(nameof(Enthusiasm)).TypeConverter<CsvProblemsConverter>();
                Map(m => m.BedPartner).Name(nameof(BedPartner)).TypeConverter<CsvBedPartnersConverter>();
                Map(m => m.PartSnore).Name(nameof(PartSnore)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.BreathPause).Name(nameof(BreathPause)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Legs).Name(nameof(Legs)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.Disorientation).Name(nameof(Disorientation)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.OtherRestless).Name(nameof(OtherRestless)).TypeConverter<CsvFrequenciesWeeklyConverter>();
                Map(m => m.OtherRestDescribe).Name(nameof(OtherRestDescribe));
            }
        }
    }
}
