using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiTest.WebDriverExtension;
using Machine.Specifications;

namespace EPiTest.UI.AlloyTechSample
{
    public class when_viewing_startpage_as_a_job_seeker : EditModeTestBase
    {
        Establish context = () =>
            {
                Login("sf", "p@ssword!");
                Visit("/systemUI/CMS/Edit/EditPanel.aspx?id=3");
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
