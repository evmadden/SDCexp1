using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class EncodingIndexViewModel
    {
        public EncodingIndexViewModel(string participantID, IEnumerable<string> imageUrls, string stanford) {
            ParticipantID = participantID;
            
            ImageUrls = imageUrls;
            Stanford = stanford;
        }

        public string ParticipantID { get; }
        
        public IEnumerable<string> ImageUrls {get;}
        public string Stanford { get; }
    }
}