using $rootnamespace$.Helpers;
using Machine.Specifications;

namespace $rootnamespace$.Specs.UI
{
    public class StartPageSpecs : UiTestBase
    {
        Because of = () =>
            {
                Driver.Navigate().GoToUrl("http://clt-gcs00p1:17002");
            };
        
        It should_have_correct_title = () =>
            {
                Driver.Title.ShouldEqual("Alloy Technologie");
            };

        It really_should_have_correct_title = () =>
            {
                Driver.Title.ShouldEqual("Alloy Technologies");
            };
    }
}
