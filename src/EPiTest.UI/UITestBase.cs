using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using EPiTest.WebDriverExtension;

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

    public class EditModeTestBase : UiTestBase
    {
        public static void VisitEditModeForPage(int id)
        {
            Visit("/systemUI/CMS/Edit/Default.aspx?id=" + id);
        }

        public static void Login(string userName, string password)
        {
            Visit("/util/login.aspx");

            Driver.Within(".epi-credentialsContainer", e =>
            {
                e.FillIn("Name", userName);
                e.FillIn("Password", password);
                e.ClickButton("Log In");
            });
        }
    }
}
