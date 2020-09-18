using System.IO;

namespace SDCode.Web.Classes
{
    public interface IImageCongruencyGetter
    {
        Congruencies Get(string imageName);
    }

    public class ImageCongruencyGetter : IImageCongruencyGetter
    {
        public Congruencies Get(string imageName)
        {
            imageName = Path.GetFileNameWithoutExtension(imageName);
            var result = imageName.Contains('I') ? Congruencies.Incongruent : Congruencies.Congruent;
            return result;
        }
    }
}
