using System.Collections.Generic;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class ContactViewModel
    {
        public ContactViewModel(IReadOnlyCollection<Config.ContactInfo> researchers) {
            Researchers = researchers;
        }

        public IReadOnlyCollection<Config.ContactInfo> Researchers { get; }
    }
}