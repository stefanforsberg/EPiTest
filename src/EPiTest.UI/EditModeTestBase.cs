
namespace EPiTest.UI
{
    public class EditModeTestBase : UiTestBase
    {
        public const string FrameEditPanel = "EditPanel";
        public const string FrameEditPanelPreview = "PreviewFrame";

        public static void VisitEditModeForPage(int id)
        {
            Visit(string.Format("/{0}/CMS/Edit/Default.aspx?id={1}", 
                System.Configuration.ConfigurationManager.AppSettings["UiSlug"], 
                id));
        }

        public static void VisitEditPanelForPage(int id)
        {
            Visit(string.Format("/{0}/CMS/Edit/EditPanel.aspx?id={1}",
                System.Configuration.ConfigurationManager.AppSettings["UiSlug"], 
                id));
        }

        public static void Login(string userName, string password)
        {
            Visit("/util/login.aspx");

            Driver.Within(".epi-credentialsContainer", e =>
                {
                    e.FillIn("Name", userName);
                    e.FillIn("Password", password);
                    e.ClickButton("Log In");
                });
        }
    }
}