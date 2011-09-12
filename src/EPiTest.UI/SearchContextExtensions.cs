using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace EPiTest.UI
{
    public static class SearchContextExtensions
    {
        public const string XPathForCheckbox = "//input[@type='checkbox' and (@id=(//label[.='{0}']/@for) or @value='{0}')]";
        public const string XPathForTypeableField = "//input[(@type='text' or @type='password') and (@id=(//label[.='{0}']/@for) or @id='{0}' or @name='{0}' or @value='{0}')] | //textarea[(@id=(//label[.='{0}']/@for) or @id='{0}' or @name='{0}' or @value='{0}')]";

        public static void Within(this ISearchContext searchContext, string css, Action<IWebElement> action)
        {
            searchContext.Within(By.CssSelector(css), action);
        }

        public static void Within(this ISearchContext searchContext, string css = null, string xpath = null, Action<IWebElement> action = null)
        {
            searchContext.Within(Helpers.GetBy(css, xpath), action);
        }


        public static void Within(this ISearchContext searchContext, By by, Action<IWebElement> action)
        {
            searchContext.Within(searchContext.Find(@by), action);
        }

        public static void Within(this ISearchContext searchContext, IWebElement element, Action<IWebElement> action)
        {
            action(element);
        }

        public static void FillIn(this ISearchContext searchContext, string idOrNameOrLabelText, string textToType)
        {
            var elementToTypeIn = searchContext.Find(xpath: String.Format(XPathForTypeableField, idOrNameOrLabelText));

            elementToTypeIn.SendKeys(textToType);
        }

        public static void ClickButton(this ISearchContext searchContext, string idOrValueOrTitle)
        {
            string xpath = String.Format("//input[(@type='button' or @type='submit' or @type='image') and (@value='{0}' or @id='{0}' or @title='{0}')] | //button[(text()='{0}' or @id='{0}' or @name='{0}')]", idOrValueOrTitle);

            searchContext.Find(xpath: xpath).Click();
        }

        public static void FillIn(this ISearchContext searchContext, By by, string text)
        {
            searchContext.Find(@by).SendKeys(text);
        }

        public static void ClickLink(this ISearchContext searchContext, string idOrLinkText)
        {
            string xpath = String.Format("//a[@id='{0}' or contains(.,'{0}')]", idOrLinkText);
            searchContext.Find(xpath: xpath).Click();
        }

        public static IWebElement Find(this ISearchContext searchContext, By by)
        {
            return searchContext.FindElement(@by);
        }

        public static IWebElement Find(this ISearchContext searchContext, string css)
        {
            return searchContext.Find(By.CssSelector(css));
        }

        public static IWebElement Find(this ISearchContext searchContext, string css = null, string xpath = null)
        {
            return searchContext.Find(Helpers.GetBy(css, xpath));
        }

        public static IEnumerable<IWebElement> All(this ISearchContext searchContext, By by)
        {
            return searchContext.FindElements(@by);
        }

        public static IEnumerable<IWebElement> All(this ISearchContext searchContext, string css)
        {
            return searchContext.All(By.CssSelector(css));
        }

        public static IEnumerable<IWebElement> All(this ISearchContext searchContext, string css = null, string xpath = null)
        {
            return searchContext.All(Helpers.GetBy(css, xpath));
        }

        public static void Check(this ISearchContext searchContext, string valueOrLabelText)
        {
            var checkBox = searchContext.Find(xpath: String.Format(XPathForCheckbox, valueOrLabelText));

            if (!checkBox.Selected)
            {
                checkBox.Click();
            }
        }

        public static void Uncheck(this ISearchContext searchContext, string valueOrLabelText)
        {
            var checkBox = searchContext.Find(xpath: String.Format(XPathForCheckbox, valueOrLabelText));

            if (checkBox.Selected)
            {
                checkBox.Click();
            }
        }

        public static IEnumerable<IWebElement> Links(this ISearchContext searchContext)
        {
            return searchContext.All("a");
        }
    }
}
