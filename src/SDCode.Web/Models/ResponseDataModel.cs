using System;
using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    [Description("A participant's many responses for a given test.")]
    public class ResponseDataModel
    {
        [Name(nameof(Image))]
        [Description("The image index viewed.")]

        public string Image { get; set; }
        [Name(nameof(Congruency))]
        public int Congruency { get; set; }
        [Name(nameof(Context))]
        public int Context { get; set; }
        [Name(nameof(Judgement))]
        [Description("How the user judged the image.")]
        public Judgements Judgement { get; set; }
        [Name(nameof(Confidence))]
        public Confidences Confidence { get; set; }
        [Name(nameof(ReactionTime))]
        public long ReactionTime { get; set; }
        [Name(nameof(Feedback))]
        public bool Feedback { get; set; }
        [Name(nameof(WhenUtc))]
        public DateTime WhenUtc { get; set; }

        public sealed class Map : ClassMap<ResponseDataModel>
        {
            public Map()
            {
                Map(m => m.Image).Name(nameof(Image));
                Map(m => m.Congruency).Name(nameof(Congruency));
                Map(m => m.Context).Name(nameof(Context));
                Map(m => m.Judgement).Name(nameof(Judgement)).TypeConverter<CsvJudgementsConverter>();
                Map(m => m.Confidence).Name(nameof(Confidence)).TypeConverter<CsvConfidencesConverter>();
                Map(m => m.ReactionTime).Name(nameof(ReactionTime));
                Map(m => m.Feedback).Name(nameof(Feedback));
                Map(m => m.WhenUtc).Name(nameof(WhenUtc));
            }
        }
    }
}
