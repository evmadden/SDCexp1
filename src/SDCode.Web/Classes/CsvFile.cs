using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.Text;
using System.IO;
using System.Globalization;

namespace SDCode.Web.Classes
{
    // https://medium.com/@didourebai/use-csv-helper-in-net-core-4053b971ea75
    public interface ICsvFile<T, TMap>
    {
        IEnumerable<T> Read();
        void Write(IEnumerable<T> records);
        (IEnumerable<byte> Bytes, string ContentType, string FileName) GetDownload();
    }

public class CsvFile<T, TMap> : ICsvFile<T, TMap> where TMap : CsvHelper.Configuration.ClassMap
{
    public IEnumerable<T> Read()
    {
        IEnumerable<T> result;
        if (File.Exists(FileName)) {
            using (var reader = new StreamReader(FileName, Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                csv.Configuration.RegisterClassMap<TMap>();
                result = csv.GetRecords<T>().ToList();
            }
        } else {
            result = new List<T>();
        }
        return result;
    }

    public void Write(IEnumerable<T> records)
    {
        using (StreamWriter sw = new StreamWriter(FileName, false, new UTF8Encoding(true)))
        using (CsvWriter cw = new CsvWriter(sw, CultureInfo.CurrentCulture))
        {
            cw.WriteHeader<T>();
            cw.NextRecord();
            foreach (T record in records)
            {
                cw.WriteRecord<T>(record);
                cw.NextRecord();
            }
        }
    }

    public (IEnumerable<byte> Bytes, string ContentType, string FileName) GetDownload() {
        IEnumerable<byte> bytes = File.Exists(FileName) ? File.ReadAllBytes(FileName) : Enumerable.Empty<byte>();
        return (bytes, "text/csv", FileName);
    }

    private string FileName {
        get {
            return $"{typeof(T).Name.Replace("Model", "Records")}.csv";
        }
    }
}


}
