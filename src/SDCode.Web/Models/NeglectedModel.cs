using System.Collections.Generic;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class NeglectedModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(Indexes))]
        public IEnumerable<int> Indexes { get; set; }
        [Name(nameof(Reason))]
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
