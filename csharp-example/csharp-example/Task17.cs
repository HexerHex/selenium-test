using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LogTest:BasicTest
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
        public void CheckErrors()
        {
            OpenAdmin();

            wait.Until(ExpectedConditions.TitleContains("My Store"));

            driver.FindElement(By.XPath("//span[contains(., 'Catalog')]")).Click();
            driver.FindElement(By.XPath("//a[contains(., 'Rubber Ducks')]")).Click();

            IList<IWebElement> elements = driver.FindElements(By.XPath("//img/following-sibling::a[1]"));
            List<string> links = new List<string>();

            foreach (IWebElement el in elements)
            {
                links.Add(el.GetAttribute("href"));
            }

            foreach (string link in links)
            {
                driver.Url = link;
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1")));
                ForEach(driver.Manage().Logs.GetLog("browser"), i => Assert.IsNull(i));
            }

        }
        private void ForEach<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}