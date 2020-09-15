using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class ParticipantModel
    {
        [Name(nameof(ID))]
        public string ID { get; set; }

        public sealed class Map : ClassMap<ConsentModel>
        {
            public Map()
            {
                Map(m => m.ID).Name(nameof(ID));
            }
        }
    }
}
