using System.Collections.Generic;
using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class NeglectedModel
    {
        [Name(nameof(ParticipantID))]
        [Description("The ID by which the participant is enrolled.")] // todo mlh check all ParticipantID descriptions for consistency
        public string ParticipantID { get; set; }
        [Name(nameof(Indexes))]
        [Description("The image indexes neglected during Encoding. (comma-delimited)")]
        public IEnumerable<int> Indexes { get; set; }
        [Name(nameof(Reason))]
        [Description("The reason provided by the participant.")]
        public string Reason { get; set; }

        public sealed class Map : ClassMap<NeglectedModel> {
            public Map() {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.Indexes).Name(nameof(Indexes)).TypeConverter<CsvIntegersConverter>();
                Map(m => m.Reason).Name(nameof(Reason));
            }
        }
    }
}
