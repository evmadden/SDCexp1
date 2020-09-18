using System;
using System.Collections.Generic;
using System.Collections.Specialized;

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
            public Column(string name, string description, IEnumerable<Code> codes)
            {
                Name = name;
                Description = description;
                Codes = codes;
            }

            public string Name { get; }
            public string Description { get; }
            public IEnumerable<Code> Codes { get; }
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
