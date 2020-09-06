using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class EpworthModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(Reading))]
        public string Reading { get; set; }
        [Name(nameof(TV))]
        public string TV { get; set; }
        [Name(nameof(PublicPlace))]
        public string PublicPlace { get; set; }
        [Name(nameof(PassengerCar))]
        public string PassengerCar{ get; set; }
        [Name(nameof(Afternoon))]
        public string Afternoon{ get; set; } 
        [Name(nameof(Talking))]
        public string Talking{ get; set; }
        [Name(nameof(Lunch))]
        public string Lunch{ get; set; }    
        [Name(nameof(Traffic))]
        public string Traffic{ get; set; }    
    }

    public sealed class EpworthMap : ClassMap<EpworthModel>
    {
        public EpworthMap()
        {
            Map(m => m.ParticipantID).Name(nameof(EpworthModel.ParticipantID));
            Map(m => m.Reading).Name(nameof(EpworthModel.Reading));
            Map(m => m.TV).Name(nameof(EpworthModel.TV));
            Map(m => m.PublicPlace).Name(nameof(EpworthModel.PublicPlace));
            Map(m => m.PassengerCar).Name(nameof(EpworthModel.PassengerCar));
            Map(m => m.Afternoon).Name(nameof(EpworthModel.Afternoon));
            Map(m => m.Talking).Name(nameof(EpworthModel.Talking));
            Map(m => m.Lunch).Name(nameof(EpworthModel.Lunch));
            Map(m => m.Traffic).Name(nameof(EpworthModel.Traffic));
        }
    }
}
