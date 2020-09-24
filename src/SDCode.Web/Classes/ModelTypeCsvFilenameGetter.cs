using System;

namespace SDCode.Web.Classes
{
    public interface IModelTypeCsvFilenameGetter
    {
        string Get(Type modelType);
    }

    public class ModelTypeCsvFilenameGetter : IModelTypeCsvFilenameGetter
    {
        public string Get(Type modelType)
        {
            var result = modelType.Name.Replace("Model", "Records.csv");
            return result;
        }
    }
}
