using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace SDCode.Web.Classes
{
    public interface IStimuliImageDataUrlGetter
    {
        IEnumerable<string> Get(IEnumerable<string> indexes);
        string Get(string index);
    }

    public class StimuliImageDataUrlGetter : IStimuliImageDataUrlGetter
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StimuliImageDataUrlGetter(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<string> Get(IEnumerable<string> indexes)
        {
            var result = indexes.Select(Get);
            return result;
        }

        public string Get(string index)
        {
            var fullPath = Path.Join(new List<string>{_webHostEnvironment.WebRootPath,"img","Stimuli",$"{index}.jpg"}.ToArray());
            var bytes = File.ReadAllBytes(fullPath);
            var base64 = Convert.ToBase64String(bytes);
            var result = $"data:image/jpg;base64,{base64}";
            return result;
        }
    }
}
