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
        public Congruencies Congruency { get; set; }
        [Name(nameof(Context))]
        public Contexts Context { get; set; }
        [Name(nameof(Judgement))]
        [Description("How the user judged the image.")]
        public Judgements Judgement { get; set; }
        [Name(nameof(Confidence))]
        public Confidences Confidence { get; set; }
        [Name(nameof(ReactionTime))]
        public long ReactionTime { get; set; }
        [Name(nameof(Feedback))]
        public Feedbacks Feedback { get; set; }
        [Name(nameof(WhenUtc))]
        public DateTime WhenUtc { get; set; }

        public sealed class Map : ClassMap<ResponseDataModel>
        {
            public Map()
            {
                Map(m => m.Image).Name(nameof(Image));
                Map(m => m.Congruency).Name(nameof(Congruency)).TypeConverter<CsvCongruenciesConverter>();
                Map(m => m.Context).Name(nameof(Context)).TypeConverter<CsvContextsConverter>();
                Map(m => m.Judgement).Name(nameof(Judgement)).TypeConverter<CsvJudgementsConverter>();
                Map(m => m.Confidence).Name(nameof(Confidence)).TypeConverter<CsvConfidencesConverter>();
                Map(m => m.ReactionTime).Name(nameof(ReactionTime));
                Map(m => m.Feedback).Name(nameof(Feedback)).TypeConverter<CsvFeedbacksConverter>();;
                Map(m => m.WhenUtc).Name(nameof(WhenUtc));
            }
        }
    }
}
