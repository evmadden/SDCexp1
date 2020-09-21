using System.Collections.Generic;
using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    // todo mlh talk to EM about maybe renaming this to EncodingMetaModel, instead (it regards only Encoding)
    public class SessionMetaModel : IParticipantModel
    {
        [Name(nameof(ParticipantID))]
        [Description("The ID by which the participant is enrolled.")] // todo mlh check all ParticipantID descriptions for consistency
        public string ParticipantID { get; set; }
        [Name(nameof(NeglectedIndexes))]
        [Description("The image indexes neglected during Encoding. (comma-delimited)")]
        public IEnumerable<int> NeglectedIndexes { get; set; }
        [Name(nameof(NeglectedReason))]
        [Description("The participant-provided reason for neglected image(s).")]
        public string NeglectedReason { get; set; }
        [Name(nameof(ObscuredIndexes))]
        [Description("The image indexes obscured during Encoding. (comma-delimited)")]
        public IEnumerable<int> ObscuredIndexes { get; set; }
        [Name(nameof(ObscuredReason))]
        [Description("The participant-provided reason for obscured image(s).")]
        public string ObscuredReason { get; set; }

        public sealed class Map : ClassMap<SessionMetaModel> {
            public Map() {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.NeglectedIndexes).Name(nameof(NeglectedIndexes)).TypeConverter<CsvIntegersConverter>();
                Map(m => m.NeglectedReason).Name(nameof(NeglectedReason));
                Map(m => m.ObscuredIndexes).Name(nameof(ObscuredIndexes)).TypeConverter<CsvIntegersConverter>();
                Map(m => m.ObscuredReason).Name(nameof(ObscuredReason));
            }
        }
    }
}
