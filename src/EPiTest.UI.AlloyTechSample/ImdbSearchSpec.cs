using EPiTest.UI;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.UI.AlloyTechSample
{
    public class ImdbSearchBase : UiTestBase
    {
        protected static IWebElement SearchResultForRow(int row)
        {
            return Driver.Find(xpath: "//table[2]//tr[" + row + "]");
        }
    }

    public class when_I_search_for_the_greatest_tv_series_of_all_time : ImdbSearchBase
    {
        Establish context = () =>
            {
                Visit("/");
            };

        Because of = () =>
            {
                Driver.Within("#navbar-form", e =>
                    {
                        e.FillIn("q", "Twin peaks");
                        e.ClickButton("Go");
                    });
            };

        It should_return_the_greatest_series_of_all_time_as_first_result = () =>
            {
                SearchResultForRow(1).Text.ShouldContain("\"Twin Peaks\"");
            };

        It should_return_the_not_quite_as_good_spinoff_movie_as_the_second_result = () =>
            {
                SearchResultForRow(2).Text.ShouldContain("Twin Peaks: Fire Walk with Me");
            };
    }
}