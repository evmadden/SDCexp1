using System.Collections.Generic;
using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class SessionMetaModel : IParticipantModel
    {
        [Name(nameof(ParticipantID))]
        [Description("The ID by which the participant is enrolled.")] // todo mlh check all "ParticipantID" property/column descriptions for consistency
        public string ParticipantID { get; set; }
        [Name(nameof(SessionName))]
        public string SessionName { get; set; }
        [Name(nameof(NeglectedImages))]
        [Description("The images neglected during the session. (comma-delimited)")]
        public IEnumerable<string> NeglectedImages { get; set; }
        [Name(nameof(NeglectedReason))]
        [Description("The participant-provided reason for neglected image(s).")]
        public string NeglectedReason { get; set; }
        [Name(nameof(ObscuredImages))]
        [Description("The images obscured during the session. (comma-delimited)")]
        public IEnumerable<string> ObscuredImages { get; set; }
        [Name(nameof(ObscuredReason))]
        [Description("The participant-provided reason for obscured image(s).")]
        public string ObscuredReason { get; set; }

        public sealed class Map : ClassMap<SessionMetaModel> {
            public Map() {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.SessionName).Name(nameof(SessionName));
                Map(m => m.NeglectedImages).Name(nameof(NeglectedImages)).TypeConverter<CsvStringsConverter>();
                Map(m => m.NeglectedReason).Name(nameof(NeglectedReason));
                Map(m => m.ObscuredImages).Name(nameof(ObscuredImages)).TypeConverter<CsvStringsConverter>();
                Map(m => m.ObscuredReason).Name(nameof(ObscuredReason));
            }
        }
    }
}
