using Machine.Specifications;

namespace EPiTest.UI.AlloyTechSample
{
    public class when_entering_all_requiered_fields_and_submitting : UiTestBase
    {
        Establish context = () =>
            {
                Visit("/Contact-Us/");
            };

        Because of = () =>
            {
                Driver.Within("[id$='FormPanel']", e =>
                    {
                        e.FillIn("Your name:", "Stefan");
                        e.FillIn("Your e-mail:", "some@thing.com");
                        e.FillIn("Your message:", "Hello. This is my message");
                        e.ClickButton("Send");
                    });
            };

        It should_display_a_thank_you_message = () =>
            {
                Driver.ShouldHaveCss(".thankyoumessage");
            };
    }

    public class when_a_requiered_field_is_not_given_a_value : UiTestBase
    {
        Establish context = () =>
        {
            Visit("/Contact-Us/");
        };

        Because of = () =>
        {
            Driver.Within("[id$='FormPanel']", e =>
            {
                e.FillIn("Your name:", "Stefan");
                e.FillIn("Your e-mail:", "some@thing.com");
                e.ClickButton("Send");
            });
        };

        It should_indicate_that_the_field_must_have_a_value = () =>
        {
            Driver.ShouldHaveCss("[id$='FormPanel'] tr:nth-child(3) .xformvalidator");
        };
    }
}
