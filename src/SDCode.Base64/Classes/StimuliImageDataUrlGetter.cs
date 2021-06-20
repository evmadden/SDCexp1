using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SDCode.Base64.Classes
{
    public interface IStimuliImageDataUrlGetter
    {
        IEnumerable<string> Get(IEnumerable<string> indexes);
        string Get(string index);
    }

    public class StimuliImageDataUrlGetter : IStimuliImageDataUrlGetter
    {
        private readonly string _fileExtension;

        public StimuliImageDataUrlGetter(string fileExtension) {
            _fileExtension = fileExtension;
        }
        public IEnumerable<string> Get(IEnumerable<string> indexes)
        {
            var result = indexes.Select(Get);
            return result;
        }

        public string Get(string index)
        {
            var fullPath = Path.Join(Program.ImagesPath,$"{index}.{_fileExtension}");
            var bytes = File.ReadAllBytes(fullPath);
            var base64 = Convert.ToBase64String(bytes);
            var result = $"data:image/jpg;base64,{base64}";
            return result;
        }
    }
}
