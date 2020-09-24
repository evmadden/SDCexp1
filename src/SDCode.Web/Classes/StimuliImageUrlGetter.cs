namespace SDCode.Web.Classes
{
    public interface IStimuliImageUrlGetter
    {
        string Get(string index);
    }

    public class StimuliImageUrlGetter : IStimuliImageUrlGetter
    {
        public string Get(string index)
        {
            var result = $"/img/Stimuli/{index}.jpg";
            return result;
        }
    }
}
