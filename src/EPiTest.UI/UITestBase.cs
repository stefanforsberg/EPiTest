using System;
using Machine.Specifications;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace EPiTest.UI
{
    public class UiTestBase
    {
        public static IWebDriver Driver;

        public static string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];

        public static void Visit(string relativeUrl)
        {
            Driver.Navigate().GoToUrl(BaseUrl + relativeUrl);
        }

        Establish context = () =>
        {
            Driver = new FirefoxDriver();
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
        };

        Cleanup after = () =>
        {
            Driver.Quit();
        };
    }
}
