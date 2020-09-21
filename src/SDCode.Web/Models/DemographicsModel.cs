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
        public Hands? Handed{ get; set; }
        [Name(nameof(Impairments))]
        public bool? Impairments{ get; set; } 
        [Name(nameof(Glasses))]
        public bool? Glasses{ get; set; }
        [Name(nameof(Language))]
        public string Language{ get; set; }    
        [Name(nameof(Bilingual))]
        public string Bilingual{ get; set; }   
        [Name(nameof(CurrentCountry))]
        public string CurrentCountry{ get; set; }
        [Name(nameof(Bed))]
        public string Bed{ get; set; }
        [Name(nameof(Wake))]
        public string Wake{ get; set; }   
        [Name(nameof(Latency))]
        public string Latency{ get; set; }  
        [Name(nameof(TST))]
        public string TST{ get; set; }           
    }

    public sealed class DemographicsMap : ClassMap<DemographicsModel>
    {
        public DemographicsMap()
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
            Map(m => m.Bed).Name(nameof(DemographicsModel.Bed));
            Map(m => m.Wake).Name(nameof(DemographicsModel.Wake));
            Map(m => m.Latency).Name(nameof(DemographicsModel.Latency));
            Map(m => m.TST).Name(nameof(DemographicsModel.TST));
        }
    }
}
