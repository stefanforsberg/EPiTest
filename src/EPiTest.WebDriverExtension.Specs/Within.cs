using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiTest.UI;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.WebDriverExtension.Specs
{
    [Subject("Within")]
    public class when_getting_an_element_within_another_element_that_has_a_css_class : TestBase
    {
        static IWebElement _element;

        Establish context = () =>
        {
            Visit("Within.html");
        };

        It should_get_the_text_for_element_with_class_in_wrapper_one = () =>
        {
            Driver.Within("#wrapper-one", e =>
                {
                    e.Find(".some-class").Text.ShouldEqual("Wrapped span one");
                });
        };

        It should_get_the_text_for_element_with_class_in_wrapper_two = () =>
        {
            Driver.Within("#wrapper-two", e =>
            {
                e.Find(".some-class").Text.ShouldEqual("Wrapped span two");
            });
        };
    }
}
