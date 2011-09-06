using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.UI.AlloyTechSample
{
    public class BreadcrumbBase : UiTestBase
    {
        protected static string BreadcrumbCss = ".BreadcrumbArea";
    }

    public class when_visiting_the_start_page : BreadcrumbBase
    {
        Establish context = () =>
            {
                Visit("/");
            };

        It should_not_have_any_breadcrumbs = () =>
            {
                Driver.ShouldNotHaveCss(BreadcrumbCss);
            };
    }

    public class when_visiting_a_page_on_the_third_level : BreadcrumbBase
    {
        static IEnumerable<IWebElement> _breadcrumbLinks; 
            
        Establish context = () =>
            {
                Visit("/en/Company/Management/");
            };

        Because of = () =>
            {
                _breadcrumbLinks = Driver.All(BreadcrumbCss + " a");
            };

        It should_have_an_area_for_breadcrumbs = () =>
            {
                Driver.ShouldHaveCss(BreadcrumbCss);
            };

        It should_contain_three_links = () =>
            {
                _breadcrumbLinks.Count().ShouldEqual(3);
            };

        It should_the_first_link_lead_to_start_page = () =>
            {
                _breadcrumbLinks.ElementAt(0).Text.ShouldEqual("Start");
            };

        It should_the_second_link_lead_to_a_page_on_the_second_level = () =>
        {
            _breadcrumbLinks.ElementAt(1).Text.ShouldEqual("Company");
        };

        It should_the_third_link_lead_to_the_current_page = () =>
        {
            _breadcrumbLinks.ElementAt(2).GetAttribute("href").ShouldEqual(Driver.Url);
        };
    }
}
