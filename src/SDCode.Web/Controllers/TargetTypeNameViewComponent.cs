using Microsoft.AspNetCore.Mvc;
using SDCode.Web.Classes;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System;

namespace SDCode.Web.Controllers {
    // https://stackoverflow.com/a/52013931
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components
    public class TargetTypeNameViewComponent : ViewComponent {
        private readonly IConfig _config;

        public TargetTypeNameViewComponent(IOptions<Config> config) {
            _config = config.Value;
        }

        static readonly IDictionary<WordForm, Func<string[], string>> Formatters = new Dictionary<WordForm, Func<string[], string>> {
            {WordForm.LowerSingular, strings=>$"{strings[0].ToLower()}"},
            {WordForm.LowerPlural, strings=>$"{strings[1].ToLower()}"}
        };

        public IViewComponentResult Invoke(WordForm wordForm, bool describeDecoration) {
            var targetTypeNames = _config.TargetTypeName.Split("|");
            var targetTypeName = Formatters[wordForm](targetTypeNames);
            if (describeDecoration) {
                var targetDecorationFormats = _config.TargetDecorationFormat.Split("|");
                var targetDecorationFormat = Formatters[wordForm](targetDecorationFormats);
                targetTypeName = string.Format(targetDecorationFormat, targetTypeName);
            }
            return View("Default", targetTypeName); // Views/Shared/Components/TargetTypeName/Default.cshtml
        }
    }
}