using EPiTest.UI;
using System.Linq;
using Machine.Specifications;
using OpenQA.Selenium;

namespace $rootnamespace$
{
    public class BreadcrumbBase : UiTestBase
    {
        protected static string BreadcrumbCss = ".BreadcrumbArea";

        public static IWebElement Breadcrumb
        {
            get { return Driver.All(BreadcrumbCss).FirstOrDefault(); }
        }
    }

    public class when_visiting_the_start_page : BreadcrumbBase
    {
        Establish context = () =>
            {
                Visit("/");
            };

        It should_not_have_any_breadcrumbs = () =>
            {
                Breadcrumb.ShouldBeNull();
            };
    }

    public class when_visiting_a_page_on_the_third_level : BreadcrumbBase
    {
        Establish context = () =>
            {
                Visit("/en/Company/Management/");
            };

        It should_have_an_area_for_breadcrumbs = () =>
            {
                Breadcrumb.ShouldNotBeNull();
            };

        It should_contain_three_links = () =>
            {
                Breadcrumb.Links().Count().ShouldEqual(3);
            };

        It should_the_first_link_lead_to_start_page = () =>
            {
                Breadcrumb.Links().ElementAt(0).Text.ShouldEqual("Start");
            };

        It should_the_second_link_lead_to_a_page_on_the_second_level = () =>
            {
                Breadcrumb.Links().ElementAt(1).Text.ShouldEqual("Company");
            };

        It should_the_third_link_lead_to_the_current_page = () =>
            {
                Breadcrumb.Links().ElementAt(2).GetAttribute("href").ShouldEqual(Driver.Url);
            };
    }
}
