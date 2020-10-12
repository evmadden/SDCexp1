using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class NeglectedImageModel
    {
        public int ID { get; private set; }
        public string ParticipantID { get; set; }
        public string PhaseName { get; set; }
        public string Index { get; set; }
    }
}
