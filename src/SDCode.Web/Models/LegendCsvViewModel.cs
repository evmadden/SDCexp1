using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class LegendCsvViewModel
    {
        public LegendCsvViewModel(IEnumerable<File> files)
        {
            Files = files;
        }

        public IEnumerable<File> Files { get; }

        public class File {
            public File(string name, string description, IEnumerable<Column> columns)
            {
                Name = name;
                Description = description;
                Columns = columns;
            }

            public string Name { get; }
            public string Description { get; }
            public IEnumerable<Column> Columns { get; }
        }
        public class Column {
            public Column(string name, string description, IEnumerable<Code> codes, string dataTypeDescription)
            {
                Name = name;
                Description = description;
                Codes = codes;
                DataTypeDescription = dataTypeDescription;
            }

            public string Name { get; }
            public string Description { get; }
            public IEnumerable<Code> Codes { get; }
            public string DataTypeDescription { get; }
        }

        public class Code {
            public Code(string value, string meaning)
            {
                Value = value;
                Meaning = meaning;
            }

            public string Value { get; }
            public string Meaning { get; }
        }
    }
}
