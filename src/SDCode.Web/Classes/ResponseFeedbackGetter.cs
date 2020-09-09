using System.IO;

namespace SDCode.Web.Classes
{
    public interface IResponseFeedbackGetter
    {
        bool GetJudgementIsCorrect(string imageName, int judgement);
    }

    public class ResponseFeedbackGetter : IResponseFeedbackGetter
    {
        public bool GetJudgementIsCorrect(string imageName, int judgement)
        {
            imageName = Path.GetFileNameWithoutExtension(imageName);
            var correctJudgement = imageName.Contains('N') ? Judgements.New : Judgements.Old;
            var result =  correctJudgement == judgement;
            return result;
        }
    }
}
