using System.Threading;
using Machine.Specifications;
using EPiTest.WebDriverExtension;

namespace EPiTest.WebDriverExtension.Specs
{
    public class form_test : TestBase
    {
        Establish context = () =>
        {
            Visit("Form.html");
        };
    }

    public class when_checking_a_checkbox_by_value_that_is_not_checked : form_test
    {
        Because of = () =>
            {
                Driver.Check("Value1");
            };

        It should_have_checked_the_checkbox = () =>
            {
                Driver.Find("#my-checkbox-one").Selected.ShouldBeTrue();
            };
    }

    public class when_checking_a_checkbox_by_label_text_that_is_not_checked : form_test
    {
        Because of = () =>
        {
            Driver.Check("Label for checkbox one");
        };

        It should_have_checked_the_checkbox = () =>
        {
            Driver.Find("#my-checkbox-one").Selected.ShouldBeTrue();
        };
    }

    public class when_checking_a_checkbox_by_value_that_is_checked : form_test
    {
        Because of = () =>
        {
            Driver.Check("Value2");
        };

        It should_have_checked_the_checkbox = () =>
        {
            Driver.Find("#my-checkbox-two").Selected.ShouldBeTrue();
        };
    }

    public class when_unchecking_a_checkbox_by_value_that_is_checked : form_test
    {
        Because of = () =>
        {
            Driver.Uncheck("Value2");
        };

        It should_have_unchecked_the_checkbox = () =>
        {
            Driver.Find("#my-checkbox-two").Selected.ShouldBeFalse();
        };
    }

    public class when_unchecking_a_checkbox_by_label_text_that_is_checked : form_test
    {
        Because of = () =>
        {
            Driver.Uncheck("Label for checkbox two");
        };

        It should_have_unchecked_the_checkbox = () =>
        {
            Driver.Find("#my-checkbox-two").Selected.ShouldBeFalse();
        };
    }

    public class when_typing_in_a_text_field_by_id : form_test
    {
        Because of = () =>
        {
            Driver.FillIn("Name", "Stefan");
        };

        It should_have_entered_the_text_in_the_element = () =>
        {
            Driver.Find("#Name").GetAttribute("value").ShouldEqual("Stefan");
        };
    }

    public class when_typing_in_a_text_field_by_name : form_test
    {
        Because of = () =>
        {
            Driver.FillIn("MyName", "Stefan");
        };

        It should_have_entered_the_text_in_the_element = () =>
        {
            Driver.Find("#Name").GetAttribute("value").ShouldEqual("Stefan");
        };
    }

    public class when_typing_in_a_text_field_by_label_text : form_test
    {
        Because of = () =>
        {
            Driver.FillIn("Label for name", "Stefan");
        };

        It should_have_entered_the_text_in_the_element = () =>
        {
            Driver.Find("#Name").GetAttribute("value").ShouldEqual("Stefan");
        };
    }

    public class when_typing_in_a_text_area_by_id : form_test
    {
        Because of = () =>
        {
            Driver.FillIn("description", "Hello, I'm a developer");
        };

        It should_have_entered_the_text_in_the_element = () =>
        {
            Driver.Find("#description").GetAttribute("value").ShouldEqual("Hello, I'm a developer");
        };
    }

    public class when_typing_in_a_text_area_by_name : form_test
    {
        Because of = () =>
        {
            Driver.FillIn("MyDescription", "Hello, I'm a developer");
        };

        It should_have_entered_the_text_in_the_element = () =>
        {
            Driver.Find("#description").GetAttribute("value").ShouldEqual("Hello, I'm a developer");
        };
    }

    public class when_typing_in_a_text_area_by_label_text : form_test
    {
        Because of = () =>
        {
            Driver.FillIn("Label for description", "Hello, I'm a developer");
        };

        It should_have_entered_the_text_in_the_element = () =>
        {
            Driver.Find("#description").GetAttribute("value").ShouldEqual("Hello, I'm a developer");
        };
    }
}
