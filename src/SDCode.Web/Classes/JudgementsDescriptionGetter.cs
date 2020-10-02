using System;

namespace SDCode.Web.Classes
{
    public interface IJudgementsDescriptionGetter
    {
        string Get(Judgements judgements);
    }

    public class JudgementsDescriptionGetter : IJudgementsDescriptionGetter
    {
        public string Get(Judgements judgements)
        {
            var result = Enum.GetName(typeof(Judgements), judgements);
            return result;
        }

    }
}


