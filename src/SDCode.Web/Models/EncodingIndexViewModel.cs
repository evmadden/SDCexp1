using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class EncodingIndexViewModel
    {
        public EncodingIndexViewModel(IEnumerable<string> imageUrls) {
            ImageUrls = imageUrls;
        }
        public IEnumerable<string> ImageUrls {get;}
    }
}