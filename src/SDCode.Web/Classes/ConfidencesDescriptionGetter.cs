using System;
using System.Text.RegularExpressions;

namespace SDCode.Web.Classes
{
    public interface IConfidencesDescriptionGetter
    {
        string Get(Confidences confidence);
    }

    public class ConfidencesDescriptionGetter : IConfidencesDescriptionGetter
    {
        public string Get(Confidences confidence)
        {
            var result = Regex.Replace(Enum.GetName(typeof(Confidences), confidence), "(\\B[A-Z])", " $1"); // https://stackoverflow.com/a/5796427/116895
            return result;
        }

    }
}


