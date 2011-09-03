using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace EPiTest.WebDriverExtension
{
    public static class WebDriverExtensions
    {
        public const string XPathForCheckbox = "//input[@type='checkbox' and (@id=(//label[.='{0}']/@for) or @value='{0}')]";
        public const string XPathForTypeableField = "//input[@type='text' and (@id=(//label[.='{0}']/@for) or @id='{0}' or @name='{0}' or @value='{0}')] | //textarea[(@id=(//label[.='{0}']/@for) or @id='{0}' or @name='{0}' or @value='{0}')]";

        public static IWebElement Find(this IWebDriver driver, By by)
        {
            return driver.FindElement(by);
        }

        public static IWebElement Find(this IWebDriver driver, string css)
        {
            return driver.Find(By.CssSelector(css));
        }

        public static IWebElement Find(this IWebDriver driver, string css = null, string xpath = null)
        {
            return driver.Find(Helpers.GetBy(css, xpath));
        }

        public static IEnumerable<IWebElement> All(this IWebDriver driver, By by)
        {
            return driver.FindElements(by);
        }

        public static IEnumerable<IWebElement> All(this IWebDriver driver, string css)
        {
            return driver.All(By.CssSelector(css));
        }

        public static IEnumerable<IWebElement> All(this IWebDriver driver, string css = null, string xpath = null)
        {
            return driver.All(Helpers.GetBy(css, xpath));
        }

        public static void ClickLink(this IWebDriver driver, string idOrLinkText)
        {
            string xpath = string.Format("//a[@id='{0}' or .='{0}']", idOrLinkText);
            
            driver.Find(xpath: xpath).Click();
        }

        public static void Check(this IWebDriver driver, string valueOrLabelText)
        {
            var checkBox = driver.Find(xpath: string.Format(XPathForCheckbox, valueOrLabelText));

            if(!checkBox.Selected)
            {
                checkBox.Click();
            }
        }

        public static void Uncheck(this IWebDriver driver, string valueOrLabelText)
        {
            var checkBox = driver.Find(xpath: string.Format(XPathForCheckbox, valueOrLabelText));

            if (checkBox.Selected)
            {
                checkBox.Click();
            }
        }

        public static void FillIn(this IWebDriver driver, string idOrNameOrLabelText, string textToType)
        {
            var elementToTypeIn = driver.Find(xpath: string.Format(XPathForTypeableField, idOrNameOrLabelText));

            elementToTypeIn.SendKeys(textToType);
        }

        public static void ClickButton(this IWebDriver driver, string idOrValue)
        {
            string xpath = string.Format("//input[(@type='button' or @type='submit') and (@value='My submit button' or @id='button')]", idOrValue);

            driver.Find(xpath: xpath).Click();
        }
        
        public static void FillIn(this IWebDriver driver, By by, string text)
        {
            driver.Find(by).SendKeys(text);
        }

        public static void Within(this IWebDriver driver, string css, Action<IWebElement> action)
        {
            driver.Within(By.CssSelector(css), action);
        }

        public static void Within(this IWebDriver driver, string css = null, string xpath = null, Action<IWebElement> action = null)
        {
            driver.Within(Helpers.GetBy(css, xpath), action);
        }

        public static void WithinFrame(this IWebDriver driver, string frameName, Action<IWebDriver> action)
        {
            driver.SwitchTo().Frame(frameName);
            action(driver);
            driver.SwitchTo().DefaultContent();
        }

        public static void Within(this IWebDriver driver, By by, Action<IWebElement> action)
        {
            driver.Within(driver.Find(by), action);
        }

        public static void Within(this IWebDriver driver, IWebElement element, Action<IWebElement> action)
        {
            action(element);
        }
    }
}
