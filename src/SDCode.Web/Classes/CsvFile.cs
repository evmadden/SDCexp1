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

        public void Write(IEnumerable<T> records)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.CurrentCulture))
            {
                // https://stackoverflow.com/a/63523529
                cw.Configuration.TypeConverterCache.AddConverter<bool>(new CsvBooleanConverter());
                cw.Configuration.TypeConverterCache.AddConverter<IEnumerable<string>>(new CsvStringsConverter());
                cw.Configuration.TypeConverterCache.AddConverter<IEnumerable<int>>(new CsvIntegersConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Judgements>(new CsvJudgementsConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Confidences>(new CsvConfidencesConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Congruencies>(new CsvCongruenciesConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Contexts>(new CsvContextsConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Feedbacks>(new CsvFeedbacksConverter());
                cw.Configuration.TypeConverterCache.AddConverter<FrequenciesWeekly>(new CsvFrequenciesWeeklyConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Qualities>(new CsvQualitiesConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Problems>(new CsvProblemsConverter());
                cw.Configuration.TypeConverterCache.AddConverter<BedPartners>(new CsvBedPartnersConverter());
                cw.Configuration.TypeConverterCache.AddConverter<ChancesDozing>(new CsvChancesDozingConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Sleepinesses>(new CsvSleepinessesConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Hands>(new CsvHandsConverter());
                cw.Configuration.TypeConverterCache.AddConverter<Sexes>(new CsvHandsConverter());
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

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return string.Equals(text, "1");
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
            var result = new List<string>();
            if (!string.IsNullOrWhiteSpace(text)) {
                result.AddRange(text.Split(",").AsEnumerable<string>());
            }
            return result;
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
            var result = new List<int>();
            if (!string.IsNullOrWhiteSpace(text)) {
                result.AddRange(text.Split(",").AsEnumerable<string>().Select(int.Parse));
            }
            return result;
        }
    }

    public class CsvJudgementsConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(Judgements)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (Judgements) Enum.Parse(typeof(Judgements), text);
        }
    }

    public class CsvConfidencesConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(Confidences)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (Confidences) Enum.Parse(typeof(Confidences), text);
        }
    }

    public class CsvCongruenciesConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(Congruencies)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (Congruencies) Enum.Parse(typeof(Congruencies), text);
        }
    }

    public class CsvContextsConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(Contexts)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (Contexts) Enum.Parse(typeof(Contexts), text);
        }
    }

    public class CsvFeedbacksConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(Feedbacks)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (Feedbacks) Enum.Parse(typeof(Feedbacks), text);
        }
    }

    public class CsvFrequenciesWeeklyConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(FrequenciesWeekly)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (FrequenciesWeekly) Enum.Parse(typeof(FrequenciesWeekly), text);
        }
    }

    public class CsvQualitiesConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(Qualities)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (Qualities) Enum.Parse(typeof(Qualities), text);
        }
    }

    public class CsvProblemsConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(Problems)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (Problems) Enum.Parse(typeof(Problems), text);
        }
    }

    public class CsvBedPartnersConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(BedPartners)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (BedPartners) Enum.Parse(typeof(BedPartners), text);
        }
    }

    public class CsvChancesDozingConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(ChancesDozing)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return (ChancesDozing) Enum.Parse(typeof(ChancesDozing), text);
        }
    }

    public class CsvSleepinessesConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(int)(Sleepinesses)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var result = string.IsNullOrWhiteSpace(text) ? default(Sleepinesses?) : (Sleepinesses) Enum.Parse(typeof(Sleepinesses), text);
            return result;
        }
    }

    public class CsvHandsConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(char)(Hands)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var result = string.IsNullOrWhiteSpace(text) ? default(Hands?) : (Hands) Enum.Parse(typeof(Hands), $"{(int)text[0]}");
            return result;
        }
    }

    public class CsvSexesConverter : DefaultTypeConverter {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if ( value == null )
            {
                return string.Empty;
            }
            return $"{(char)(Sexes)value}";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var result = string.IsNullOrWhiteSpace(text) ? default(Sexes?) : (Sexes) Enum.Parse(typeof(Sexes), $"{(int)text[0]}");
            return result;
        }
    }

}
