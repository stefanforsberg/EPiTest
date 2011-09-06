using System;
using OpenQA.Selenium;

namespace EPiTest.UI
{
    public static class Helpers
    {
        public static By GetBy(string css, string xpath)
        {
            if (!String.IsNullOrEmpty(css))
            {
                return By.CssSelector(css);
            }

            if (!String.IsNullOrEmpty(xpath))
            {
                return By.XPath(xpath);
            }

            throw new ArgumentException("Must provide either a css or xpath argument");
        }
    }
}
