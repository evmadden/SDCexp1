namespace SDCode.Web.Classes
{
    public interface IEncodingFinishedChecker
    {
        bool IsFinished(string participantID);
    }

    public class EncodingFinishedChecker : IEncodingFinishedChecker
    {
        private readonly ISessionMetaRepository _sessionMetaRepository;

        public EncodingFinishedChecker(ISessionMetaRepository sessionMetaRepository)
        {
            _sessionMetaRepository = sessionMetaRepository;
        }

        public bool IsFinished(string participantID)
        {
            var encodingMeta = _sessionMetaRepository.Get(participantID, "Encoding");
            var result = encodingMeta?.FinishedWhenUtc.HasValue ?? false;
            return result;
        }
    }
}