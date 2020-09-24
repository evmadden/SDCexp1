using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    [Description("Participant IDs enrolled.")]
    public class ParticipantModel
    {
        [Name(nameof(ID))]
        [Description("Set of Participant ID's for ALL participants in the study.")]
        public string ID { get; set; }

        public sealed class Map : ClassMap<ConsentModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(ID));
            }
        }
    }
}
