using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using SDCode.Base64.Classes;

namespace SDCode.Base64
{
    class Program
    {
        //public static readonly string ImagesPath = System.IO.Path.Join("..","assets","img","Stimuli", "Animacy");
        //static readonly string FileExtension = "jpg";

        //public static readonly string ImagesPath = System.IO.Path.Join("..","assets","img","Stimuli", "Congruency");
        //static readonly string FileExtension = "jpg";

        public static readonly string ImagesPath = System.IO.Path.Join("..","assets","img","Stimuli", "Verbal");
        static readonly string FileExtension = "png";
        
        static void Main(string[] args)
        {
            var stimuliImageDataUrlGetter = new StimuliImageDataUrlGetter(FileExtension);
            var directoryInfo = new DirectoryInfo(ImagesPath);
            var indexes = directoryInfo.GetFiles().Where(x=>(x.Attributes & FileAttributes.Hidden)==0).Select(x=>x.Name).Select(System.IO.Path.GetFileNameWithoutExtension);
            var indexTypes = indexes.Select(x=>Regex.Replace(x, "[0-9]", string.Empty));
            var distinctIndexTypes = indexTypes.Distinct();
            var indexesByLetter = distinctIndexTypes.ToDictionary(x=>x, x=>indexes.Where(y=>y.StartsWith(x)));
            var indexesToThird = new List<string>{"N", "NI"};
            foreach (var index in indexesToThird)
            {
                if (indexesByLetter.ContainsKey(index)) {
                    indexesByLetter[$"{index}1"] = indexesByLetter[index].Take(Decimal.ToInt32(indexesByLetter[index].Count()/3));
                    var secondThird = indexesByLetter[index].Except(indexesByLetter[$"{index}1"]);
                    indexesByLetter[$"{index}2"] = secondThird.Take(Decimal.ToInt32(secondThird.Count()/2));
                    indexesByLetter[$"{index}3"] = secondThird.Except(indexesByLetter[$"{index}2"]);
                    indexesByLetter.Remove(index);                
                }
            }

            var zipFilePaths = new List<string>();
            foreach ((var indexType, var typeIndexes) in indexesByLetter)
            {
                var data = typeIndexes.ToDictionary(x=>x, stimuliImageDataUrlGetter.Get);
                var value = System.Text.Json.JsonSerializer.Serialize(data);
                var directory = System.IO.Path.GetTempPath();
                var path = System.IO.Path.Join(directory, $"{indexType}.json");
                System.IO.File.WriteAllText(path, value);
                zipFilePaths.Add(path);
            }
            var zipFiles = zipFilePaths.Select(x=>(x, System.IO.File.ReadAllBytes(x))).ToList();
            var zipArchive = GetZipArchive(zipFiles);
            var outputPath = System.IO.Path.Join("..","assets","base64");
            System.IO.Directory.CreateDirectory(outputPath);
            System.IO.File.WriteAllBytes(System.IO.Path.Join(outputPath,$"MemoryStudy_Base64_{DateTime.Now.ToString("yyyyMMddHHmmss")}.zip"), zipArchive);
            zipFilePaths.ForEach(System.IO.File.Delete);
        }

        public static byte[] GetZipArchive(List<(string FileName, byte[] Content)> files)
        {
            byte[] archiveFile;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(System.IO.Path.GetFileName(file.FileName), CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open())
                            zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }
                archiveFile = archiveStream.ToArray();
            }
            return archiveFile;
        }
    }
}
