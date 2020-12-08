namespace SDCode.Web.Classes
{
    public interface IParticipantLanguageQualificationChecker
    {
        bool IsQualified(string nativeLanguageName);
    }

    public class ParticipantLanguageQualificationChecker : IParticipantLanguageQualificationChecker
    {
        public bool IsQualified(string nativeLanguageName)
        {
            var result = nativeLanguageName?.Trim().ToLower().StartsWith("en") ?? false;
            return result;
        }
    }
}