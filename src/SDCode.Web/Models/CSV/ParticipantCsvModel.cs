using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models.CSV
{
    [Description("Participant IDs enrolled.")]
    public class ParticipantCsvModel
    {
        [Name(nameof(ID))]
        [Description("Participant enrollment identifier.")]
        public string ID { get; set; }

        [Name(nameof(Active))]
        [Description("Participant's activity status.")]
        public bool Active { get; set; }

        public sealed class Map : ClassMap<ParticipantCsvModel>
        {
            public Map()
            {
                Map(m => m.ID).Name(nameof(ID));
                Map(m => m.Active).Name(nameof(Active));
            }
        }
    }
}
