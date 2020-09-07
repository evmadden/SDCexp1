using System.Collections.Generic;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IStimuliImageUrlGetter
    {
        IEnumerable<string> Get(IEnumerable<string> indexes);
    }

    public class StimuliImageUrlGetter : IStimuliImageUrlGetter
    {
        public IEnumerable<string> Get(IEnumerable<string> indexes)
        {
            var result = indexes.Select(x => $"/img/Stimuli/{x}.jpg");
            return result;
        }
    }
}
