using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace SDCode.Web.Classes
{
    public class ParticipantVerifyFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) {}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("participantID", out var value)) {
                var participantID = (string)value;
                if (!string.IsNullOrWhiteSpace(participantID) && !string.Equals(context.RouteData.Values["action"], "Login")) {
                    var participantEnrollmentVerifier = new ParticipantEnrollmentVerifier();
                    var (isEnrolled, isActive) = participantEnrollmentVerifier.Verify(participantID);
                    if (isEnrolled) {
                        if (!isActive) {
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary {{ "controller", "Finished" }, { "action", "Index" }});
                        }
                    } else {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary {{ "controller", "Participant" }, { "action", "NotEnrolled" }, {"id", participantID}});
                    }
                }
            }
        }
    }
}
