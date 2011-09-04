using System;
using System.Linq;
using System.Threading;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.WebDriverExtension.Specs
{
    [Subject("Switching frames")]
    public class when_switching_to_a_frame : TestBase
    {
        static IWebElement _element;

        Establish context = () =>
            {
                Visit("Frames.html");
            };

        It should_be_able_to_find_an_element_within_that_frame = () =>
            {
                Driver.WithinFrame("frame1", d =>
                {
                    d.Find("#div-in-frame-1").Text.ShouldEqual("Text in div in frame 1");
                });
            };

        It should_be_able_to_find_an_element_in_the_main_content_after_locating_an_element_within_a_frame = () =>
        {
            Driver.WithinFrame("frame1", d =>
                {
                    d.Find("#div-in-frame-1");
                });

            Driver.Find("#div1").Text.ShouldEqual("Some text");
        };
    }

    [Subject("Switching frames")]
    public class when_switching_to_a_frame_and_clicking_a_link_in_that_frame : TestBase
    {
        static IWebElement _element;

        Establish context = () =>
        {
            Visit("Frames.html");
        };

        It should_perform_the_click_action = () =>
        {
            Driver.WithinFrame("frame1", d =>
            {
                d.ClickLink("View as Visitor Group");
                d.Url.ShouldEndWith("SimpleElements.html");
            });            
        };
    }

    [Subject("Switching frames")]
    public class when_switching_to_a_frame_within_frame : TestBase
    {
        static IWebElement _element;

        Establish context = () =>
        {
            Visit("Frames.html");
        };

        It should_be_able_to_find_an_element_within_the_frame_within_a_frame = () =>
        {
            Driver.WithinFrame("frame2", d =>
            {
                d.WithinFrame("frame3", d2 =>
                    {
                        d2.Find("#div-in-frame-3").Text.ShouldEqual("Text in div in frame 3");        
                    });
            });
        };

        It should_be_able_to_find_an_element_in_the_main_content_after_locating_an_element_within_a_frame = () =>
        {
            Driver.WithinFrame("frame1", d =>
            {
                d.Find("#div-in-frame-1");
            });

            Driver.Find("#div1").Text.ShouldEqual("Some text");
        };
    }
}