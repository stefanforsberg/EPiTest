using System.Linq;
using EPiTest.UI;
using Machine.Specifications;

namespace EPiTest.WebDriverExtension.Specs
{
    public class when_getting_all_links_contained_in_an_element : TestBase
    {
        Establish context = () =>
            {
                Visit("Links.html");
            };

        It should_find_all_links = () =>
            {
                Driver.Find("#a-div").Links().Count().ShouldEqual(4);
            };
    }
}
