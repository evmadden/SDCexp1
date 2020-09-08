using System.Collections.Generic;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IStimuliImageUrlGetter
    {
        IEnumerable<string> Get(IEnumerable<string> indexes);
        string Get(string index);
    }

    public class StimuliImageUrlGetter : IStimuliImageUrlGetter
    {
        public IEnumerable<string> Get(IEnumerable<string> indexes)
        {
            var result = indexes.Select(GetUrl);
            return result;
        }

        public string Get(string index)
        {
            var result = GetUrl(index);
            return result;
        }

        private string GetUrl(string index) {
            var result = $"/img/Stimuli/{index}.jpg";
            return result;
        }
    }
}
