using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IImageContextGetter
    {
        Contexts Get(string imageName);
    }

    public class ImageContextGetter : IImageContextGetter
    {
        
        private static readonly IDictionary<string, Contexts> ContextsMap = new Dictionary<string, Contexts>{{"A", Contexts.StillInContext}, {"B", Contexts.Decontextualized}, {"C", Contexts.Decontextualized}, {"D", Contexts.Decontextualized}, {"E", Contexts.StillInContext}, {"F", Contexts.NoChange}, {"N", Contexts.Foil}};
        public Contexts Get(string imageName)
        {
            imageName = Path.GetFileNameWithoutExtension(imageName);
            var result = ContextsMap.First(x=>imageName.Contains(x.Key)).Value;
            return result;
        }
    }
}
