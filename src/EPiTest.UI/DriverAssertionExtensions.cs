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
        public static void ShouldHave(this ISearchContext searchContext, By by)
        {
            if (searchContext.FindElements(by).Count == 0)
            {
                throw new SpecificationException(string.Format("Failed to find " + by));
            }
        }

        public static void ShouldNotHave(this ISearchContext searchContext, By by)
        {
            if (searchContext.FindElements(by).Count != 0)
            {
                throw new SpecificationException(string.Format("Should not have found " + by));
            }
        }

        public static void ShouldHaveCss(this ISearchContext searchContext, string css)
        {
            searchContext.ShouldHave(By.CssSelector(css));
        }

        public static void ShouldNotHaveCss(this ISearchContext searchContext, string css)
        {
            searchContext.ShouldNotHave(By.CssSelector(css));
        }

        public static void ShouldHaveXPath(this ISearchContext searchContext, string id)
        {
            searchContext.ShouldHave(By.XPath(id));
        }

        public static void ShouldNotHaveXPath(this ISearchContext searchContext, string id)
        {
            searchContext.ShouldNotHave(By.XPath(id));
        }
    }
}
