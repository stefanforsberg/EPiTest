using System.Linq;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.UI.AlloyTechSample
{
    public class ContactUsBase : UiTestBase
    {
        public static string Form { get { return "[id$='FormPanel']"; } }

        public static IWebElement ThankYou
        {
            get { return Driver.All(".thankyoumessage").FirstOrDefault() ; }
        }

        public static IWebElement ValidationError(int forTableRow)
        {
            return Driver
                .All(string.Format("{0} tr:nth-child({1}) .xformvalidator", Form, forTableRow))
                .FirstOrDefault();
        }
    }

    public class when_entering_all_requiered_fields_and_submitting : ContactUsBase
    {
        Establish context = () =>
            {
                Visit("/Contact-Us/");
            };

        Because of = () =>
            {
                Driver.Within(Form, e =>
                    {
                        e.FillIn("Your name:", "Stefan");
                        e.FillIn("Your e-mail:", "some@thing.com");
                        e.FillIn("Your message:", "Hello. This is my message");
                        e.ClickButton("Send");
                    });
            };

        It should_display_a_thank_you_message = () =>
            {
                ThankYou.ShouldNotBeNull();
            };
    }

    public class when_a_requiered_field_is_not_given_a_value : ContactUsBase
    {
        Establish context = () =>
        {
            Visit("/Contact-Us/");
        };

        Because of = () =>
        {
            Driver.Within(Form, e =>
            {
                e.FillIn("Your name:", "Stefan");
                e.FillIn("Your e-mail:", "some@thing.com");
                e.ClickButton("Send");
            });
        };

        It should_indicate_that_the_field_must_have_a_value = () =>
        {
            ValidationError(3).ShouldNotBeNull();
        };
    }
}
