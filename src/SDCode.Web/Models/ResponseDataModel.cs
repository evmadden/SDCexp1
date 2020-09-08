using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class ResponseDataModel
    {
        [Name(nameof(Image))]
        public string Image { get; set; }
        [Name(nameof(Congruency))]
        public int Congruency { get; set; }
        [Name(nameof(Context))]
        public int Context { get; set; }
        [Name(nameof(Judgement))]
        public int Judgement { get; set; }
        [Name(nameof(Confidence))]
        public int Confidence { get; set; }
        [Name(nameof(ReactionTime))]
        public long ReactionTime { get; set; }

        public sealed class Map : ClassMap<ResponseDataModel>
        {
            public Map()
            {
                Map(m => m.Image).Name(nameof(Image));
                Map(m => m.Congruency).Name(nameof(Congruency));
                Map(m => m.Context).Name(nameof(Context));
                Map(m => m.Judgement).Name(nameof(Judgement));
                Map(m => m.Confidence).Name(nameof(Confidence));
                Map(m => m.ReactionTime).Name(nameof(ReactionTime));
            }
        }
    }
}
