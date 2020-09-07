using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.Text;
using System.IO;
using System.Globalization;
using CsvHelper.TypeConversion;
using CsvHelper.Configuration;

namespace SDCode.Web.Classes
{
    public class StimuliImageUrlGetter
    {
        public IEnumerable<string> Get(IEnumerable<string> indexes)
        {
            //                          /img/Logo.png
            var result = indexes.Select(x=>$"/img/Stimuli/{x}.jpg");
            return result;
        }
    }
}
