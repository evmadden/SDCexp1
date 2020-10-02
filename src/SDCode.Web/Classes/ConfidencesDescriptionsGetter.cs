using System;
using System.Collections.Generic;

namespace SDCode.Web.Classes
{
    public interface IConfidencesDescriptionsGetter
    {
        IDictionary<int, string> Get();
    }

    public class ConfidencesDescriptionsGetter : IConfidencesDescriptionsGetter
    {
        private readonly IConfidencesDescriptionGetter _confidencesDescriptionGetter;

        public ConfidencesDescriptionsGetter(IConfidencesDescriptionGetter confidencesDescriptionGetter)
        {
            _confidencesDescriptionGetter = confidencesDescriptionGetter;
        }

        public IDictionary<int, string> Get()
        {
            var result = new Dictionary<int, string>();
            foreach (int confidence in Enum.GetValues(typeof(Confidences)))
            {
                String description = _confidencesDescriptionGetter.Get((Confidences)confidence);
                result.Add(confidence, description);
            }
            return result;
        }

    }
}


