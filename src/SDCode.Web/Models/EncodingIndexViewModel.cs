using System.Collections.Generic;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class EncodingIndexViewModel
    {
        public EncodingIndexViewModel(string participantID, IEnumerable<string> imageUrls, Sleepinesses stanford) {
            ParticipantID = participantID;
            
            ImageUrls = imageUrls;
            Stanford = stanford;
        }

        public string ParticipantID { get; }
        
        public IEnumerable<string> ImageUrls {get;}
        public Sleepinesses Stanford { get; }
    }
}