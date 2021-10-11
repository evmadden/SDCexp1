using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SDCode.Base64.Classes
{
    public interface IStimuliDataUrlGetter
    {
        IEnumerable<string> Get(IEnumerable<string> indexes);
        string Get(string index);
    }

    public class StimuliDataUrlGetter : IStimuliDataUrlGetter
    {
        private readonly string _fileExtension;
        private readonly string _mimeType;

        public StimuliDataUrlGetter(string fileExtension, string mimeType) {
            _fileExtension = fileExtension;
            _mimeType = mimeType;
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
            var result = $"data:{_mimeType};base64,{base64}";
            return result;
        }
    }
}
