using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    public class BasicTest
    {
        protected Application App;

        [SetUp]
        public void start()
        {
            App = new Application();
        }

        public static bool IsElementPresent(IWebDriver driver, By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }
        public static bool IsElementPresent(IWebElement el, By locator)
        {
            try
            {
                el.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }

        public static bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }
        public static bool AreElementsPresent(IWebElement el, By locator)
        {
            return el.FindElements(locator).Count > 0;
        }

        public static bool isStale(IWebElement el)
        {
            try
            {
                el.Click();
                return false;
            }
            catch (StaleElementReferenceException ex)
            {
                return true;
            }
        }

    }
}