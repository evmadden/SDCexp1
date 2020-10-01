using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SDCode.Web.Models;
using SDCode.Web.Classes;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace SDCode.Web.Controllers
{
    public class UtilityController : Controller
    {
        private readonly IStimuliImageDataUrlGetter _stimuliImageDataUrlGetter;
        private readonly IPhaseSetsGetter _phaseSetsGetter;

        public UtilityController(IStimuliImageDataUrlGetter stimuliImageDataUrlGetter, IPhaseSetsGetter phaseSetsGetter)
        {
            _stimuliImageDataUrlGetter = stimuliImageDataUrlGetter;
            _phaseSetsGetter = phaseSetsGetter;
        }

        public IActionResult EncodeAll()
        {
            var indexes = System.IO.Directory.GetFiles(System.IO.Path.Join("wwwroot","img","Stimuli")).Select(System.IO.Path.GetFileNameWithoutExtension);
            var indexTypes = indexes.Select(x=>Regex.Replace(x, "[0-9]", string.Empty));
            var distinctIndexTypes = indexTypes.Distinct();
            var indexesByLetter = distinctIndexTypes.ToDictionary(x=>x, x=>indexes.Where(y=>y.StartsWith(x)));
            var indexesToThird = new List<string>{"N", "NI"};
            foreach (var index in indexesToThird)
            {
                indexesByLetter[$"{index}1"] = indexesByLetter[index].Take(Decimal.ToInt32(indexesByLetter[index].Count()/3));
                var secondThird = indexesByLetter[index].Except(indexesByLetter[$"{index}1"]);
                indexesByLetter[$"{index}2"] = secondThird.Take(Decimal.ToInt32(secondThird.Count()/2));
                indexesByLetter[$"{index}3"] = secondThird.Except(indexesByLetter[$"{index}2"]);
                indexesByLetter.Remove(index);                
            }
            foreach ((var indexType, var typeIndexes) in indexesByLetter)
            {
                var data = typeIndexes.ToDictionary(x=>x, _stimuliImageDataUrlGetter.Get);
                var value = System.Text.Json.JsonSerializer.Serialize(data);
                var directory = "_dataUrls";
                var path = System.IO.Path.Join(directory, $"{indexType}.json");
                System.IO.Directory.CreateDirectory(directory);
                System.IO.File.WriteAllText(path, value);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
