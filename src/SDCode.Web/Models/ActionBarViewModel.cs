namespace SDCode.Web.Models
{
    public class ActionBarViewModel
    {
        public ActionBarViewModel(string buttonName) {
            ButtonName = buttonName;
        }

        public string ButtonName { get; }
    }
}