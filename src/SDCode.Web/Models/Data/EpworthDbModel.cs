using SDCode.Web.Classes;

namespace SDCode.Web.Models.Data
{
    public class EpworthDbModel
    {
        public string ParticipantID { get; set; }
        public ChancesDozing? Reading { get; set; }
        public ChancesDozing? TV { get; set; }
        public ChancesDozing? PublicPlace { get; set; }
        public ChancesDozing? PassengerCar{ get; set; }
        public ChancesDozing? Afternoon{ get; set; } 
        public ChancesDozing? Talking{ get; set; }
        public ChancesDozing? Lunch{ get; set; }    
        public ChancesDozing? Traffic{ get; set; }    
    }
}
