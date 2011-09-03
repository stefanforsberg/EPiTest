using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace EPiTest.WebDriverExtension
{
    public static class WebElementExtensions
    {
        public static IWebElement Element(this IWebElement element, By by)
        {
            return element.FindElement(@by);
        }

        public static IWebElement Element(this IWebElement element, string css)
        {
            return element.Element(By.CssSelector(css));
        }

        public static IWebElement Element(this IWebElement element, string css = null, string xpath = null)
        {
            return element.Element(Helpers.GetBy(css, xpath));
        }
    }
}
