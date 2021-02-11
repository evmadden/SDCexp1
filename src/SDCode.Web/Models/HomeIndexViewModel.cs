using System.Collections.Generic;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel(IReadOnlyCollection<Config.ContactInfo> researchers) {
            Researchers = researchers;
        }

        public IReadOnlyCollection<Config.ContactInfo> Researchers { get; }
    }
}