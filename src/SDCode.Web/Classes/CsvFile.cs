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
    // https://medium.com/@didourebai/use-csv-helper-in-net-core-4053b971ea75
    public interface ICsvFile<T, TMap>
    {
        IEnumerable<T> Read();
        void Write(IEnumerable<T> records);
        (IEnumerable<byte> Bytes, string ContentType, string FileName) GetDownload();
        ICsvFile<T, TMap> WithFilename(string filename);
    }

    public class CsvFile<T, TMap> : ICsvFile<T, TMap> where TMap : CsvHelper.Configuration.ClassMap
    {
        private string _filename;
        public ICsvFile<T, TMap> WithFilename(string filename) {
            _filename = filename?.Replace(".csv", "", StringComparison.InvariantCultureIgnoreCase);
            return this;
        }
        public IEnumerable<T> Read()
        {
            IEnumerable<T> result;
            if (File.Exists(FilePath)) {
                using (var reader = new StreamReader(FilePath, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csv.Configuration.RegisterClassMap<TMap>();
                    result = csv.GetRecords<T>().ToList();
                }
            } else {
                result = new List<T>();
            }
            _filename = null;
            return result;
        }

        // todo mlh check Write references to make sure participants aren't being inappropriately duplicated across multiple records of same CSV
        public void Write(IEnumerable<T> records)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.CurrentCulture))
            {
                cw.Configuration.TypeConverterCache.AddConverter<bool>(new CsvBooleanConverter()); // https://stackoverflow.com/a/63523529
                cw.Configuration.TypeConverterCache.AddConverter<IEnumerable<string>>(new CsvStringsConverter()); // https://stackoverflow.com/a/63523529
                cw.Configuration.TypeConverterCache.AddConverter<IEnumerable<int>>(new CsvIntegersConverter()); // https://stackoverflow.com/a/63523529
                cw.WriteHeader<T>();
                cw.NextRecord();
                foreach (T record in records)
                {
                    cw.WriteRecord<T>(record);
                    cw.NextRecord();
                }
            }
            _filename = null;
        }

        public (IEnumerable<byte> Bytes, string ContentType, string FileName) GetDownload() {
            IEnumerable<byte> bytes = File.Exists(FilePath) ? File.ReadAllBytes(FilePath) : Enumerable.Empty<byte>();
            return (bytes, "text/csv", FileName);
        }

        private string FilePath {
            get {
                var result = $"_csv/{FileName}";
                Directory.CreateDirectory(Path.GetDirectoryName(result));
                return result;
            }
        }

        private string FileName {
            get {
                return $"{_filename ?? $"{typeof(T).Name.Replace("Model", "Records")}"}.csv";
            }
        }
    }

    public class CsvBooleanConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if( value == null )
            {
                return string.Empty;
            }
            var boolValue = (bool)value;
            return boolValue ? "1" : "0";
        }
    }

    public class CsvStringsConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            var strings = (IEnumerable<string>)value;
            return string.Join(",", strings);
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text.Split(",").AsEnumerable<string>();
        }
    }

    public class CsvIntegersConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return string.Join(",", ((IEnumerable<int>)value));
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text.Split(",").AsEnumerable<string>().Select(int.Parse);
        }
    }
}
