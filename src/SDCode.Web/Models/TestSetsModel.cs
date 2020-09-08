using System.Collections.Generic;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class TestSetsModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(Immediate))]
        public IEnumerable<string> Immediate { get; set; }
        [Name(nameof(Delayed))]
        public IEnumerable<string> Delayed { get; set; }
        [Name(nameof(Followup))]
        public IEnumerable<string> Followup { get; set; }

        public sealed class Map : ClassMap<TestSetsModel> {
            public Map() {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.Immediate).Name(nameof(Immediate)).TypeConverter<CsvStringsConverter>();
                Map(m => m.Delayed).Name(nameof(Delayed)).TypeConverter<CsvStringsConverter>();
                Map(m => m.Followup).Name(nameof(Followup)).TypeConverter<CsvStringsConverter>();
            }
        }
    }
}
