using System.IO;

namespace SDCode.Web.Classes
{
    public interface IResponseFeedbackGetter
    {
        Feedbacks Get(string imageName, Judgements judgement);
    }

    public class ResponseFeedbackGetter : IResponseFeedbackGetter
    {
        public Feedbacks Get(string imageName, Judgements judgement)
        {
            var expectedJudgement = imageName.Contains('N') ? Judgements.New : Judgements.Old;
            var result =  judgement == expectedJudgement ? Feedbacks.Correct : Feedbacks.Incorrect;
            return result;
        }
    }
}
