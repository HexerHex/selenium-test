using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    public class BaseOld
    {

        public static void OpenAdmin(IWebDriver driver)
            {
                driver.Url = "http://localhost/litecart/admin/login.php";
                driver.FindElement(By.Name("username")).SendKeys("admin");
                driver.FindElement(By.Name("password")).SendKeys("admin");
                driver.FindElement(By.Name("login")).Click();
            }


        public static void OpenClient(IWebDriver driver)
        {
            driver.Url = "http://localhost/litecart/en/";

        }

        public static void OpenClientRemote(IWebDriver driver)
        {
            driver.Url = "http://litecart.stqa.ru/en/";

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


