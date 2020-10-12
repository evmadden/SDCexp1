using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models.CSV
{
    [Description("Participant IDs enrolled.")]
    public class ParticipantCsvModel
    {
        [Name(nameof(ID))]
        [Description("Set of Participant ID's for ALL participants in the study.")]
        public string ID { get; set; }

        public sealed class Map : ClassMap<ParticipantCsvModel>
        {
            public Map()
            {
                Map(m => m.ID).Name(nameof(ID));
            }
        }
    }
}
