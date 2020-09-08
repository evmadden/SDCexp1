using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class ResponseDataModel
    {
        [Name(nameof(Congruency))]
        public int Congruency { get; set; }
        [Name(nameof(Context))]
        public int Context { get; set; }

        public sealed class Map : ClassMap<ResponseDataModel>
        {
            public Map()
            {
                Map(m => m.Congruency).Name(nameof(Congruency));
                Map(m => m.Context).Name(nameof(Context));
            }
        }
    }
}
