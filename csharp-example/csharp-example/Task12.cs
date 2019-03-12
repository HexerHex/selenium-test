using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class AddProductTest: BaseOld
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
        public void AddNewProduct()
        {
            OpenAdmin(driver);
            driver.FindElement(By.XPath("//span[contains(.,'Catalog')]")).Click();

            driver.FindElement(By.XPath("//a[contains(text(),' Add New Product')]")).Click();
            driver.FindElement(By.Name("name[en]")).SendKeys("Test Duck");
            driver.FindElement(By.Name("code")).SendKeys("rd000");
            new Actions(driver).MoveToElement(driver.FindElement(By.XPath("//*[@data-name='Rubber Ducks']"))).Click().Perform();
            new Actions(driver).MoveToElement(driver.FindElement(By.XPath("//td[contains(text(),'Unisex')]/preceding-sibling::td[1]"))).Click().Perform();
            driver.FindElement(By.Name("quantity")).SendKeys("100");
            new SelectElement(driver.FindElement(By.Name("sold_out_status_id"))).SelectByIndex(1);

            // File upload
            string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\assets\\test-duck.png");
            string path = Path.GetFullPath(relativePath);
            driver.FindElement(By.Name("new_images[]")).SendKeys(path);

            SetDatepicker(driver, "input[name='date_valid_from']", "01/01/2019");
            SetDatepicker(driver, "input[name='date_valid_to']", "26/02/2019");

            // Information Tab
            driver.FindElement(By.XPath("//a[contains(.,'Information')]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("manufacturer_id")));
            new SelectElement(driver.FindElement(By.Name("manufacturer_id"))).SelectByIndex(1);


            driver.FindElement(By.Name("keywords")).SendKeys("test, duck");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("Test duck");
            driver.FindElement(By.CssSelector("div.trumbowyg-editor")).SendKeys("It's a duck");

            driver.FindElement(By.Name("head_title[en]")).SendKeys("Test Duck");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("Test Duck");


            // Prices Tab
            driver.FindElement(By.XPath("//a[contains(.,'Prices')]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("purchase_price")));
            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys("30");
            new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code"))).SelectByIndex(1);


            driver.FindElement(By.Name("prices[USD]")).SendKeys("5");
            driver.FindElement(By.Name("prices[EUR]")).SendKeys("5");

            driver.FindElement(By.Name("save")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1[contains(.,'Catalog')]")));

            Assert.IsTrue(IsElementPresent(driver, By.XPath("//a[contains(.,'Test Duck')]")));

        }
        private void SetDatepicker(IWebDriver driver, string cssSelector, string date)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until<bool>(
                d => driver.FindElement(By.CssSelector(cssSelector)).Displayed);

            driver.FindElement(By.CssSelector(cssSelector)).SendKeys(date);
            driver.FindElement(By.CssSelector("body")).Click();
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}