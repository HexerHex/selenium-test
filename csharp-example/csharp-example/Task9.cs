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
    public class GeoTest:BasicTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        List<String> unsortedList = new List<String>();
        List<String> sortedList = new List<String>();
        List<String> zoneUrls = new List<String>();

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CheckCountries()
        {
            OpenAdmin();
            driver.FindElement(By.XPath("//*[text()='Countries']")).Click();

            IList<IWebElement> Rows = driver.FindElements(By.CssSelector(".row"));

            foreach (IWebElement el in Rows)
            {
                unsortedList.Add(el.FindElement(By.CssSelector("td:nth-child(5)")).GetAttribute("textContent"));
                if (!(el.FindElement(By.CssSelector("td:nth-child(6)")).Text.Equals("0")))
                {
                    zoneUrls.Add(el.FindElement(By.CssSelector("td:nth-child(5) a")).GetAttribute("href"));
                }
            }
            sortedList.AddRange(unsortedList);
            sortedList.Sort();

            Assert.IsTrue(unsortedList.SequenceEqual(sortedList));

            sortedList.Clear();
            unsortedList.Clear();

            foreach (String url in zoneUrls)
            {
                driver.Url = url;
                IList<IWebElement> GeoRows = driver.FindElements(By.CssSelector("table#table-zones tr:not(.header)"));

                foreach (IWebElement el in GeoRows)
                {
                    unsortedList.Add(el.FindElement(By.CssSelector("td:nth-child(3)")).GetAttribute("textContent"));
                }

                unsortedList.RemoveAt(unsortedList.Count - 1);
                sortedList.AddRange(unsortedList);
                sortedList.Sort();

                Assert.IsTrue(unsortedList.SequenceEqual(sortedList));

                sortedList.Clear();
                unsortedList.Clear();
            }
            zoneUrls.Clear();
        }
        [Test]
        public void CheckGeo()
        {
            OpenAdmin();
            driver.FindElement(By.XPath("//*[text()='Geo Zones']")).Click();

            IList<IWebElement> Rows = driver.FindElements(By.CssSelector(".row"));

            foreach (IWebElement el in Rows)
            {
                zoneUrls.Add(el.FindElement(By.CssSelector("a")).GetAttribute("href"));
            }

            foreach (String url in zoneUrls)
            {
                driver.Url = url;
                IList<IWebElement> GeoRows = driver.FindElements(By.CssSelector("select[name *= 'zone_code']"));

                foreach (IWebElement el in GeoRows)
                {
                    SelectElement selectedValue = new SelectElement(el);
                    unsortedList.Add(selectedValue.SelectedOption.Text);
                }

                sortedList.AddRange(unsortedList);
                sortedList.Sort();

                Assert.IsTrue(unsortedList.SequenceEqual(sortedList));

                sortedList.Clear();
                unsortedList.Clear();
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