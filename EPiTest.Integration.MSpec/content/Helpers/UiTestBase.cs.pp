using Machine.Specifications;
using OpenQA.Selenium.Firefox;

namespace $rootnamespace$.Helpers
{
    public abstract class UiTestBase
    {
        public static FirefoxDriver Driver;

        Establish context = () =>
            {
                Driver = new FirefoxDriver();
        };

        Cleanup after = () =>
            {
                Driver.Quit();
            };
    }
}
