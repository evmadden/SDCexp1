using System.Linq;
using SDCode.Web.Models.CSV;

namespace SDCode.Web.Classes
{
    public interface IParticipantEnrollmentVerifier
    {
        (bool Enrolled, bool Active) Verify(string participantID);
    }

    public class ParticipantEnrollmentVerifier : IParticipantEnrollmentVerifier
    {
        public (bool Enrolled, bool Active) Verify(string participantID)
        {
            var participantCsvFile = new CsvFile<ParticipantCsvModel, ParticipantCsvModel.Map>(new ModelTypeCsvFilenameGetter());
            var record = participantCsvFile.Read().FirstOrDefault(x=>string.Equals(x.ID, participantID));
            var enrolled = record != default;
            var active = record?.Active ?? false;
            return (enrolled, active);
        }
    }
}
