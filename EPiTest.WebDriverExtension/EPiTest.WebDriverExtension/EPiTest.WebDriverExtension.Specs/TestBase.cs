using System;
using System.IO;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.WebDriverExtension.Specs
{
    public class TestBase
    {
        protected static IWebDriver Driver;

        Establish context = () =>
            {
                Driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
                Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            };

        protected static void Visit(string htmlFileName)
        {
            var uri = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HTML/" + htmlFileName));

            Driver
                .Navigate()
                .GoToUrl(uri);
        }

        Cleanup after = () =>
            {
                Driver.Close();
                Driver.Dispose();
            };
    }
}