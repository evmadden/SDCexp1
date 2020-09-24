using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IDataTypeDescriptionGetter
    {
        string Get(Type dataType);
    }

    public class DataTypeDescriptionGetter : IDataTypeDescriptionGetter
    {
        private static readonly IEnumerable<Type> NumberTypes = new List<Type>{typeof(int), typeof(long)};
        public string Get(Type dataType)
        {
            var result = GetNumberOrTypeName(dataType);
            var isEnumerable = dataType.GetInterface(nameof(IEnumerable)) != null && dataType != typeof(string);
            if (isEnumerable) {
                var listType = dataType.GetGenericArguments().SingleOrDefault();
                if (listType != null) {
                    var listTypeName = GetNumberOrTypeName(listType);
                    result = $"List of {listTypeName} (comma-delimited)";
                }
            } else {
                var underlyingType = Nullable.GetUnderlyingType(dataType);
                if (underlyingType != null) {
                    result = GetNumberOrTypeName(underlyingType);
                }
            }
            result = result.Replace(typeof(bool).Name, "[1:Yes] [0:No]");
            return result;
        }

        private string GetNumberOrTypeName(Type dataType) {
            var result = NumberTypes.Contains(dataType) ? "Number" : dataType.Name;
            return result;
        }
    }
}