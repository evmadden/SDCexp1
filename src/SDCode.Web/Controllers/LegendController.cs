using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class LegendController : Controller
    {
        private readonly ILogger<LegendController> _logger;

        public LegendController(ILogger<LegendController> logger)
        {
            _logger = logger;
        }

        public IActionResult CSV()
        {
            var files = new List<LegendCsvViewModel.File>();
            var types = from type in Assembly.GetExecutingAssembly().GetTypes() where type.IsClass && type.Namespace == "SDCode.Web.Models" && type.Name.EndsWith("Model") && !type.Name.EndsWith("ViewModel") select type;
            foreach (var type in types)
            {
                var fileDescription = default(string);
                object[] fileAttrs = type.GetCustomAttributes(true);
                foreach (object fileAttr in fileAttrs)
                {
                    System.ComponentModel.DescriptionAttribute fileDescAttr = fileAttr as System.ComponentModel.DescriptionAttribute;
                    if (fileDescAttr != null)
                    {
                        fileDescription = fileDescAttr.Description;
                        break;
                    }
                }
                var columns = new List<LegendCsvViewModel.Column>();
                var properties = type.GetProperties();
                foreach (System.Reflection.PropertyInfo pi in properties)
                {
                    var columnName = pi.Name;
                    var columnDescription = default(string);
                    var columnCodes = new List<LegendCsvViewModel.Code>();
                    object[] columnAttrs = pi.GetCustomAttributes(true);
                    foreach (object columnAttr in columnAttrs)
                    {
                        System.ComponentModel.DescriptionAttribute columnDescAttr = columnAttr as System.ComponentModel.DescriptionAttribute;
                        if (columnDescAttr != null)
                        {
                            columnDescription = columnDescAttr.Description;
                            break;
                        }
                    }
                    var nullablePropertyTypeArgument = Nullable.GetUnderlyingType(pi.PropertyType);
                    if (pi.PropertyType.IsEnum) {
                        AddColumnCodes(pi.PropertyType);
                    } else if (nullablePropertyTypeArgument?.IsEnum ?? false) {
                        AddColumnCodes(nullablePropertyTypeArgument);
                    } else if (Type.Equals(typeof(bool), pi.PropertyType)) {
                        columnCodes.Add(new LegendCsvViewModel.Code($"{0}", "No"));
                        columnCodes.Add(new LegendCsvViewModel.Code($"{1}", "Yes"));
                    }
                    columns.Add(new LegendCsvViewModel.Column(columnName, columnDescription, columnCodes));
                    void AddColumnCodes(Type enumType) {
                        foreach (var enumValue in Enum.GetValues(enumType).Cast<int>())
                        {
                            var enumText = Enum.GetName(enumType, enumValue);
                            columnCodes.Add(new LegendCsvViewModel.Code($"{enumValue}", enumText));
                        }
                    }
                }
                // todo mlh refactor first "Replace" below so that we're not duplicating how it is that we turn a type name into a filename (see CsvFile.cs)
                string filename = type.Name.Replace("Model", ".csv");
                filename = filename.Replace("ResponseData", "<ID>_<TestName>");
                files.Add(new LegendCsvViewModel.File(filename, fileDescription, columns.Any() ? columns : default));
            }

            var viewModel = new LegendCsvViewModel(files);
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}