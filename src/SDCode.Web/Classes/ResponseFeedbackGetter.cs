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
            imageName = Path.GetFileNameWithoutExtension(imageName);
            var correctJudgement = imageName.Contains('N') ? Judgements.New : Judgements.Old;
            var result =  correctJudgement == judgement ? Feedbacks.Correct : Feedbacks.Incorrect;
            return result;
        }
    }
}
