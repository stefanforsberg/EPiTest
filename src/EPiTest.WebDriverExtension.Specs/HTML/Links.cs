using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiTest.UI;
using Machine.Specifications;

namespace EPiTest.WebDriverExtension.Specs.HTML
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
