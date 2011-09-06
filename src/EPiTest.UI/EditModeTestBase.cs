
namespace EPiTest.UI
{
    public class EditModeTestBase : UiTestBase
    {
        public const string FrameEditPanel = "EditPanel";
        public const string FrameEditPanelPreview = "PreviewFrame";

        public static void VisitEditModeForPage(int id)
        {
            Visit("/systemUI/CMS/Edit/Default.aspx?id=" + id);
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