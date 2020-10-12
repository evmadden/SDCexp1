using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models.CSV
{
    public class EpworthCsvModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(Reading))]
        [Description("Chance of falling asleep while reading.")]
        public ChancesDozing? Reading { get; set; }
        [Name(nameof(TV))]
        [Description("Chance of falling asleep while watching TV.")]
        public ChancesDozing? TV { get; set; }
        [Name(nameof(PublicPlace))]
        [Description("Chance of falling asleep while sititng still in a public place.")]
        public ChancesDozing? PublicPlace { get; set; }
        [Name(nameof(PassengerCar))]
        [Description("Chance of falling asleep as a passenger in a car for an hour without a break.")]
        public ChancesDozing? PassengerCar{ get; set; }
        [Name(nameof(Afternoon))]
        [Description("Chance of falling asleep when laying down to rest in the afternoon when circumstances allow.")]
        public ChancesDozing? Afternoon{ get; set; } 
        [Name(nameof(Talking))]
        [Description("Chance of falling asleep while sitting and talking with someone.")]
        public ChancesDozing? Talking{ get; set; }
        [Name(nameof(Lunch))]
        [Description("Chance of falling asleep while sitting quietly after lunch without having drunk alcohol.")]
        public ChancesDozing? Lunch{ get; set; }    
        [Name(nameof(Traffic))]
        [Description("Chance of falling asleep when in a car or bus while stopped for a few minutes in traffic.")]
        public ChancesDozing? Traffic{ get; set; }    

        public sealed class Map : ClassMap<EpworthCsvModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.Reading).Name(nameof(Reading)).TypeConverter<CsvChancesDozingConverter>();
                Map(m => m.TV).Name(nameof(TV)).TypeConverter<CsvChancesDozingConverter>();
                Map(m => m.PublicPlace).Name(nameof(PublicPlace)).TypeConverter<CsvChancesDozingConverter>();
                Map(m => m.PassengerCar).Name(nameof(PassengerCar)).TypeConverter<CsvChancesDozingConverter>();
                Map(m => m.Afternoon).Name(nameof(Afternoon)).TypeConverter<CsvChancesDozingConverter>();
                Map(m => m.Talking).Name(nameof(Talking)).TypeConverter<CsvChancesDozingConverter>();
                Map(m => m.Lunch).Name(nameof(Lunch)).TypeConverter<CsvChancesDozingConverter>();
                Map(m => m.Traffic).Name(nameof(Traffic)).TypeConverter<CsvChancesDozingConverter>();
            }
        }
    }
}
