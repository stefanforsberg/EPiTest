using System;
using OpenQA.Selenium;

namespace EPiTest.UI
{
    public static class WebDriverExtensions
    {
        public static void WithinFrame(this IWebDriver driver, string frameName, Action<IWebDriver> action)
        {
            driver.SwitchTo().Frame(frameName);
            action(driver);
            driver.SwitchTo().DefaultContent();
        }

        public static string Content(this IWebDriver driver)
        {
            return driver.Find("body").Text;
        }
    }
}
