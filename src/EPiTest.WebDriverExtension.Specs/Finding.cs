using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.WebDriverExtension.Specs
{
    [Subject("Finding elements")]
    public class when_getting_an_element_by_css_selector : TestBase
    {
        static IWebElement _element;
        
        Establish context = () =>
            {
                Visit("SimpleElements.html");
            };

        Because of = () =>
            {
                _element = Driver.Find("#div1");
            };

        It should_be_able_to_find_the_element = () =>
            {
                _element.Text.ShouldEqual("Some text");
            };
    }

    [Subject("Finding elements")]
    public class when_getting_an_element_by_xpath : TestBase
    {
        static IWebElement _element;

        Establish context = () =>
        {
            Visit("SimpleElements.html");
        };

        Because of = () =>
        {
            _element = Driver.Find(xpath: "//div[@class='some-class']");
        };

        It should_be_able_to_find_the_element = () =>
        {
            _element.Text.ShouldEqual("Another text");
        };
    }

    [Subject("Finding elements")]
    public class when_getting_all_elements_by_css : TestBase
    {
        static IEnumerable<IWebElement> _elements;

        Establish context = () =>
        {
            Visit("SimpleElements.html");
        };

        Because of = () =>
        {
            _elements = Driver.All(".pop");
        };

        It should_be_able_to_find_all_elements = () =>
        {
            _elements.Count().ShouldEqual(3);
        };
    }

    [Subject("Finding elements")]
    public class when_getting_all_elements_by_xpath : TestBase
    {
        static IEnumerable<IWebElement> _elements;

        Establish context = () =>
        {
            Visit("SimpleElements.html");
        };

        Because of = () =>
        {
            _elements = Driver.All(xpath: "//table[@id='my-table']//td");
        };

        It should_be_able_to_find_all_elements = () =>
        {
            _elements.Count().ShouldEqual(5);
        };
    }
}
