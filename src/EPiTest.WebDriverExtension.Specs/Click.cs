using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiTest.UI;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.WebDriverExtension.Specs
{
    public class with_click : TestBase
    {
        Establish context = () =>
        {
            Visit("Click.html");
        };
    }

    [Subject("Clicking elements")]
    public class when_clicking_a_link_by_selector : with_click
    {
        
        Because of = () =>
            {
                Driver.Find("#FirstLink").Click();
            };

        Behaves_like<LinkHasBeenClickedBehaviors> the_link_has_been_clicked;
    }

    [Subject("Clicking elements")]
    public class when_clicking_a_link_by_id : with_click
    {
        Because of = () =>
        {
            Driver.ClickLink("FirstLink");
        };

        Behaves_like<LinkHasBeenClickedBehaviors> the_link_has_been_clicked;
    }

    [Subject("Clicking elements")]
    public class when_clicking_a_link_by_link_text : with_click
    {
        Because of = () =>
        {
            Driver.ClickLink("Link text");
        };

        Behaves_like<LinkHasBeenClickedBehaviors> the_link_has_been_clicked;
    }

    [Subject("Clicking elements")]
    public class when_clicking_a_normal_button_by_id : with_click
    {
        Because of = () =>
        {
            Driver.ClickButton("a-button");
        };

        Behaves_like<LinkHasBeenClickedBehaviors> the_link_has_been_clicked;
    }

    [Subject("Clicking elements")]
    public class when_clicking_a_normal_button_by_value : with_click
    {
        Because of = () =>
        {
            Driver.ClickButton("My button");
        };

        Behaves_like<LinkHasBeenClickedBehaviors> the_link_has_been_clicked;
    }

    [Subject("Clicking elements")]
    public class when_clicking_an_image_button_by_title : with_click
    {
        Because of = () =>
        {
            Driver.ClickButton("Search");
        };

        Behaves_like<LinkHasBeenClickedBehaviors> the_link_has_been_clicked;
    }

    [Behaviors]
    public class LinkHasBeenClickedBehaviors
    {
        protected static IWebDriver Driver;

        It should_perform_the_click_action = () =>
        {
            Driver.Url.ShouldEndWith("SimpleElements.html");
        };
    }
}
