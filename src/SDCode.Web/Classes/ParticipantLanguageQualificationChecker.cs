using Microsoft.Extensions.Options;

namespace SDCode.Web.Classes
{
    public interface IParticipantLanguageQualificationChecker
    {
        bool IsQualified(string nativeLanguageName);
    }

    public class ParticipantLanguageQualificationChecker : IParticipantLanguageQualificationChecker
    {
        private readonly IConfig _config;

        public ParticipantLanguageQualificationChecker(IOptions<Config> config)
        {
            _config = config.Value;
        }
        public bool IsQualified(string nativeLanguageName)
        {
            var result = true;
            if (_config.LanguageIsRelevant) {
                result = nativeLanguageName?.Trim().ToLower().StartsWith("en") ?? false;
            }
            return result;
        }
    }
}