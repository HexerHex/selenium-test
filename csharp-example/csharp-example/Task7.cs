using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LeftMenuTest:BasicTest
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
        public void ClickMenu()
        {
            Login(driver);

            int count = driver.FindElements(By.XPath("//ul[@id='box-apps-menu']/li")).Count;

            for (int i = 1; i <= count; i++)
            {
                String menuItem = String.Format("//ul[@id='box-apps-menu']/li[{0}]", i);
                driver.FindElement(By.XPath(menuItem)).Click();

                if (AreElementsPresent(driver, By.XPath(menuItem + "/ul")))
                {

                    int subCount = driver.FindElements(By.XPath(menuItem + "//li")).Count;

                    for (int j = 1; j <= subCount; j++)
                    {
                        String submenuItem = menuItem + String.Format("//li[{0}]", j);
                        driver.FindElement(By.XPath(submenuItem)).Click();
                        Assert.IsTrue(IsElementPresent(driver, By.CssSelector("h1")));

                    }

                }

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