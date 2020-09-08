using System.IO;

namespace SDCode.Web.Classes
{
    public interface IImageCongruencyGetter
    {
        int Get(string imageName);
    }

    public class ImageCongruencyGetter : IImageCongruencyGetter
    {
        public int Get(string imageName)
        {
            imageName = Path.GetFileNameWithoutExtension(imageName);
            var result = imageName.Contains('I') ? 2 : 1;
            return result;
        }
    }
}
