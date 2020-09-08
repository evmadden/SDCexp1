using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IImageContextGetter
    {
        int Get(string imageName);
    }

    public class ImageContextGetter : IImageContextGetter
    {
        private static readonly IDictionary<string, int> Contexts = new Dictionary<string, int>{{"A", 2}, {"B", 3}, {"C", 3}, {"D", 3}, {"E", 2}, {"F", 1}, {"N", 4}};
        public int Get(string imageName)
        {
            imageName = Path.GetFileNameWithoutExtension(imageName);
            var result = Contexts.First(x=>imageName.Contains(x.Key)).Value;
            return result;
        }
    }
}
