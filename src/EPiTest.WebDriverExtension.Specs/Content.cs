using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiTest.UI;
using Machine.Specifications;

namespace EPiTest.WebDriverExtension.Specs
{
    public class when_getting_content : TestBase
    {
        static string _content;
        
        Establish context = () =>
            {
                Visit("Page.html");
            };

        Because of = () =>
            {
                _content = Driver.Content();
            };

        It should_return_all_content_within_the_body_tag = () =>
            {
                _content.ShouldEqual("Some text\r\nSome other text");
            };

    }
}
