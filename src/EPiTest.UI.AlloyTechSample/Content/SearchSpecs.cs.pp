using EPiTest.UI;
using System.Linq;
using Machine.Specifications;
using OpenQA.Selenium;

namespace $rootnamespace$
{
    public class SearchBase : UiTestBase
    {
        protected static void SearchFor(string searchPhrase)
        {
            Driver.Within(".QuickSearchArea", e =>
                {
                    e.FillIn("Search here", searchPhrase);
                    e.ClickButton("Search");
                });
        }

        protected static string SearchBoxValue()
        {
            return Driver.Find("#SearchArea [id$='SearchText']").GetAttribute("value");
        }

        protected static IWebElement SearchResults()
        {
            return Driver.All("#ResultArea").FirstOrDefault();
        }
    }

    public class when_performing_a_search : SearchBase
    {
        Establish context = () =>
            {
                Visit("/");
            };

        Because of = () =>
            {
                SearchFor("Products");
            };

        It should_redirect_to_the_search_page = () =>
            {
                Driver.Url.ShouldContain("Search/");
            };

        It should_enter_the_search_phrase_in_the_search_box = () =>
            {
                SearchBoxValue().ShouldEqual("Products");
            };

        It should_show_the_search_results = () =>
            {
                SearchResults().ShouldNotBeNull();
            };
    }
}
