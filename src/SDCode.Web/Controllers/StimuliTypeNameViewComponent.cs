using Microsoft.AspNetCore.Mvc;
using SDCode.Web.Classes;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace SDCode.Web.Controllers {
    // https://stackoverflow.com/a/52013931
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components
    public class StimuliTypeNameViewComponent : ViewComponent {
        private readonly IConfig _config;

        public StimuliTypeNameViewComponent(IOptions<Config> config) {
            _config = config.Value;
        }

        static readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        static readonly IDictionary<WordForm, Func<string[], string>> Formatters = new Dictionary<WordForm, Func<string[], string>> {
            {WordForm.LowerSingular, strings=>$"{strings[0].ToLower()}"},
            {WordForm.LowerPlural, strings=>$"{strings[1].ToLower()}"},
            {WordForm.TitlePlural, strings=>$"{textInfo.ToTitleCase(strings[1])}"}
        };

        public IViewComponentResult Invoke(WordForm wordForm) {
            var stimuliTypeNames = _config.StimuliTypeName.Split("|");
            var stimuliTypeName = Formatters[wordForm](stimuliTypeNames);
            return View("Default", stimuliTypeName); // Views/Shared/Components/StimuliTypeName/Default.cshtml
        }
    }
}