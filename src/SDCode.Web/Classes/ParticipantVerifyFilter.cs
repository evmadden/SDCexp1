using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public class ParticipantVerifyFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) {}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("participantID", out object value)) {
                var participantID = (string)value;
                if (!string.IsNullOrWhiteSpace(participantID)) {
                    var participantCsvFile = new CsvFile<ParticipantModel, ParticipantModel.Map>();
                    var isVerified = participantCsvFile.Read().Any(x=>string.Equals(x.ID, participantID));
                    if (!isVerified) {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary {{ "controller", "Participant" }, { "action", "NotEnrolled" }, {"id", participantID}});
                    }
                }
            }
        }
    }
}
