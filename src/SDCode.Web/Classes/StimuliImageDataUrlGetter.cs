using System;
using System.Collections.Generic;
using System.IO;
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
            var result = new List<string>();
            foreach (var index in indexes)
            {
                var fullPath = Path.Join(new List<string>{_webHostEnvironment.WebRootPath,"img","Stimuli",$"{index}.jpg"}.ToArray());
                var bytes = File.ReadAllBytes(fullPath);
                var base64 = Convert.ToBase64String(bytes);
                var dataUrl = $"data:image/jpg;base64,{base64}";
                result.Add(dataUrl);
            }
            return result;
        }

        public string Get(string index)
        {
            var result = GetUrl(index);
            return result;
        }

        private string GetUrl(string index) {
            var result = $"/img/Stimuli/{index}.jpg";
            return result;
        }
    }
}
