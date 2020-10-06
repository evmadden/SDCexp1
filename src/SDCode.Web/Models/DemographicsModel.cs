using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class DemographicsModel : IParticipantModel
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

        public sealed class Map : ClassMap<DemographicsModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(DemographicsModel.ParticipantID));
                Map(m => m.Sex).Name(nameof(DemographicsModel.Sex)).TypeConverter<CsvSexesConverter>();
                Map(m => m.Age).Name(nameof(DemographicsModel.Age));
                Map(m => m.YearStudy).Name(nameof(DemographicsModel.YearStudy));
                Map(m => m.Handed).Name(nameof(DemographicsModel.Handed)).TypeConverter<CsvHandsConverter>();
                Map(m => m.Impairments).Name(nameof(DemographicsModel.Impairments)).TypeConverter<CsvBooleanConverter>();
                Map(m => m.Glasses).Name(nameof(DemographicsModel.Glasses)).TypeConverter<CsvBooleanConverter>();
                Map(m => m.Language).Name(nameof(DemographicsModel.Language));
                Map(m => m.Bilingual).Name(nameof(DemographicsModel.Bilingual));
                Map(m => m.CurrentCountry).Name(nameof(DemographicsModel.CurrentCountry));
            }
        }
    }
}
