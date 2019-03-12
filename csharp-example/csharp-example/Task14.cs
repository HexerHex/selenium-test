using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class NewWindow:BasicTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void OpenSwitchClose()
        {
            OpenAdmin();

            driver.FindElement(By.XPath("//span[contains(.,'Countries')]")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("content")));

            driver.FindElement(By.XPath("//a[contains(., 'Austria')]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//td[@id='content']")));

            IList<IWebElement> externalLinks = driver.FindElements(By.XPath("//i[@class='fa fa-external-link']"));

            string originalWindow = driver.CurrentWindowHandle;

            foreach (IWebElement link in externalLinks)
            {
                int windowsCountBefore = driver.WindowHandles.Count;

                link.Click();

                wait.Until(driver => driver.WindowHandles.Count == (windowsCountBefore + 1));

                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();

                driver.SwitchTo().Window(originalWindow);

            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}