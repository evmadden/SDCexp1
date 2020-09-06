using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class DemographicsModel
    {
        [Name(nameof(Sex))]
        public string Sex { get; set; }
        [Name(nameof(Age))]
        public string Age { get; set; }
        [Name(nameof(YearStudy))]
        public string YearStudy { get; set; }
        [Name(nameof(Handed))]
        public string Handed{ get; set; }
        [Name(nameof(Impairments))]
        public string Impairments{ get; set; } 
        [Name(nameof(Glasses))]
        public string Glasses{ get; set; }
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
            Map(m => m.Sex).Name(nameof(DemographicsModel.Sex));
            Map(m => m.Age).Name(nameof(DemographicsModel.Age));
            Map(m => m.YearStudy).Name(nameof(DemographicsModel.YearStudy));
            Map(m => m.Handed).Name(nameof(DemographicsModel.Handed));
            Map(m => m.Impairments).Name(nameof(DemographicsModel.Impairments));
            Map(m => m.Glasses).Name(nameof(DemographicsModel.Glasses));
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
