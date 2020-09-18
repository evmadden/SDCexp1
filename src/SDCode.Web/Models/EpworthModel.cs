using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class EpworthModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(Reading))]
        public ChancesDozing Reading { get; set; }
        [Name(nameof(TV))]
        public ChancesDozing TV { get; set; }
        [Name(nameof(PublicPlace))]
        public ChancesDozing PublicPlace { get; set; }
        [Name(nameof(PassengerCar))]
        public ChancesDozing PassengerCar{ get; set; }
        [Name(nameof(Afternoon))]
        public ChancesDozing Afternoon{ get; set; } 
        [Name(nameof(Talking))]
        public ChancesDozing Talking{ get; set; }
        [Name(nameof(Lunch))]
        public ChancesDozing Lunch{ get; set; }    
        [Name(nameof(Traffic))]
        public ChancesDozing Traffic{ get; set; }    
    }

    public sealed class EpworthMap : ClassMap<EpworthModel>
    {
        public EpworthMap()
        {
            Map(m => m.ParticipantID).Name(nameof(EpworthModel.ParticipantID));
            Map(m => m.Reading).Name(nameof(EpworthModel.Reading)).TypeConverter<CsvChancesDozingConverter>();
            Map(m => m.TV).Name(nameof(EpworthModel.TV)).TypeConverter<CsvChancesDozingConverter>();
            Map(m => m.PublicPlace).Name(nameof(EpworthModel.PublicPlace)).TypeConverter<CsvChancesDozingConverter>();
            Map(m => m.PassengerCar).Name(nameof(EpworthModel.PassengerCar)).TypeConverter<CsvChancesDozingConverter>();
            Map(m => m.Afternoon).Name(nameof(EpworthModel.Afternoon)).TypeConverter<CsvChancesDozingConverter>();
            Map(m => m.Talking).Name(nameof(EpworthModel.Talking)).TypeConverter<CsvChancesDozingConverter>();
            Map(m => m.Lunch).Name(nameof(EpworthModel.Lunch)).TypeConverter<CsvChancesDozingConverter>();
            Map(m => m.Traffic).Name(nameof(EpworthModel.Traffic)).TypeConverter<CsvChancesDozingConverter>();
        }
    }
}
