using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class EncodingIndexViewModel
    {
        public EncodingIndexViewModel(string participantID, IEnumerable<string> imageUrls) {
            ParticipantID = participantID;
            
            ImageUrls = imageUrls;
        }

        public string ParticipantID { get; }
        
        public IEnumerable<string> ImageUrls {get;}
    }
}