using EPiTest.UI;
using Machine.Specifications;

namespace $rootnamespace$
{
    public class when_viewing_startpage_as_a_job_seeker : EditModeTestBase
    {
        Establish context = () =>
            {
                Login("sf", "p@ssword!");
                VisitEditPanelForPage(3);
            };

        Because of = () =>
            {
                Driver.Within("#visitorGroupDropDown", e =>
                     {
                         e.ClickLink("View as Visitor Group");
                         e.Check(" Job Seeker ");
                         e.ClickButton("Apply");
                     });
            };

        It should_print_text_tailored_to_the_choosen_visitor_group = () =>
        {
            Driver.WithinFrame(FrameEditPanelPreview, preview =>
                {
                    preview.Content().ShouldContain("We are recruiting");
                });
        };
    }
}
