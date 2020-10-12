using System.Linq;
using SDCode.Web.Models.CSV;

namespace SDCode.Web.Classes
{
    public interface IParticipantEnrollmentVerifier
    {
        bool Verify(string participantID);
    }

    public class ParticipantEnrollmentVerifier : IParticipantEnrollmentVerifier
    {
        public bool Verify(string participantID)
        {
            var participantCsvFile = new CsvFile<ParticipantCsvModel, ParticipantCsvModel.Map>(new ModelTypeCsvFilenameGetter());
            var result = participantCsvFile.Read().Any(x=>string.Equals(x.ID, participantID));
            return result;
        }
    }
}
