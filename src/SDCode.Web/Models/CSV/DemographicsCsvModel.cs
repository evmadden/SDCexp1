using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models.CSV
{
    public class DemographicsCsvModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(Sex))]
        public Sexes? Sex { get; set; }
        [Name(nameof(Age))]
        public string Age { get; set; }
        [Name(nameof(YearStudy))]
        public string YearStudy { get; set; }
        [Name(nameof(Handed))]
        [Description("Participant notes whether they are right or left handed.")]
        public Hands? Handed{ get; set; } 
        [Name(nameof(Impairments))]
        [Description("Participant notes whether they have any visual impairments.")]
        public bool? Impairments{ get; set; } 
        [Name(nameof(Glasses))]
        public bool? Glasses{ get; set; }
        [Name(nameof(Language))]
        [Description("Participant notes what their native language is.")]
        public string Language{ get; set; }     
        [Name(nameof(Bilingual))]
        [Description("Participant notes whether they speak any other languages.")]
        public string Bilingual{ get; set; }    
        [Name(nameof(CurrentCountry))]
        public string CurrentCountry{ get; set; }

        public sealed class Map : ClassMap<DemographicsCsvModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.Sex).Name(nameof(Sex)).TypeConverter<CsvSexesConverter>();
                Map(m => m.Age).Name(nameof(Age));
                Map(m => m.YearStudy).Name(nameof(YearStudy));
                Map(m => m.Handed).Name(nameof(Handed)).TypeConverter<CsvHandsConverter>();
                Map(m => m.Impairments).Name(nameof(Impairments)).TypeConverter<CsvBooleanConverter>();
                Map(m => m.Glasses).Name(nameof(Glasses)).TypeConverter<CsvBooleanConverter>();
                Map(m => m.Language).Name(nameof(Language));
                Map(m => m.Bilingual).Name(nameof(Bilingual));
                Map(m => m.CurrentCountry).Name(nameof(CurrentCountry));
            }
        }
    }
}
