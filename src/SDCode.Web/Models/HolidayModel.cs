using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class HolidayModel
    {
        [Name(nameof(Name))]
        public string Name { get; set; }
        [Name(nameof(MonthNumber))]
        public int MonthNumber { get; set; }
        [Name(nameof(DayNumber))]
        public int DayNumber { get; set; }
    }

    public sealed class HolidayMap : ClassMap<HolidayModel>
    {
        public HolidayMap()
        {
            Map(m => m.Name).Name(nameof(HolidayModel.Name));
            Map(m => m.MonthNumber).Name(nameof(HolidayModel.MonthNumber));
            Map(m => m.DayNumber).Name(nameof(HolidayModel.DayNumber));
        }
    }
}
