using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using OpenQA.Selenium;

namespace EPiTest.UI
{
    public static class DriverAssertionExtensions
    {
        public static void ShouldHave(this IWebDriver driver, By by)
        {
            if (driver.FindElements(by).Count == 0)
            {
                throw new SpecificationException(string.Format("Failed to find " + by));
            }
        }

        public static void ShouldNotHave(this IWebDriver driver, By by)
        {
            if (driver.FindElements(by).Count != 0)
            {
                throw new SpecificationException(string.Format("Should not have found " + by));
            }
        }

        public static void ShouldHaveCss(this IWebDriver driver, string css)
        {
            driver.ShouldHave(By.CssSelector(css));
        }

        public static void ShouldNotHaveCss(this IWebDriver driver, string css)
        {
            driver.ShouldNotHave(By.CssSelector(css));
        }

        public static void ShouldHaveXPath(this IWebDriver driver, string id)
        {
            driver.ShouldHave(By.XPath(id));
        }

        public static void ShouldNotHaveXPath(this IWebDriver driver, string id)
        {
            driver.ShouldNotHave(By.XPath(id));
        }
    }
}
