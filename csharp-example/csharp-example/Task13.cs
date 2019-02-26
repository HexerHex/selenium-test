using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class CartTest:BasicTest
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
        public void AddProductToCart()
        {
            int prodToAddCount = 3;
            OpenClientRemote(driver);

            for (int i = 0; i < prodToAddCount; i++)
            {
                driver.FindElement(By.CssSelector("li.product")).Click();

                if (IsElementPresent(driver, By.Name("options[Size]")))
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Name("options[Size]")));
                    new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByIndex(1);
                }

                driver.FindElement(By.Name("add_cart_product")).Click();

                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format("//span[@class='quantity'][contains(.,{0})]", i + 1))));

                driver.FindElement(By.CssSelector(".fa-home")).Click();
            }

            driver.FindElement(By.XPath("//a[contains(.,'Checkout »')]")).Click();

            IWebElement boxCheckoutCart = driver.FindElement(By.Id("box-checkout-cart"));
            int uniqueItemsCount = boxCheckoutCart.FindElements(By.CssSelector("li.item")).Count;

            IWebElement orderSummary = driver.FindElement(By.Id("order_confirmation-wrapper"));

            for (int i = 0; i < uniqueItemsCount; i++)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("order_confirmation-wrapper")));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(.,'Remove')]")));
                driver.FindElement(By.XPath("//button[contains(.,'Remove')]")).Click();

                if (isStale(orderSummary))
                {
                    driver.FindElement(By.XPath("//button[contains(.,'Remove')]")).Click();
                    orderSummary = driver.FindElement(By.Id("order_confirmation-wrapper"));
                }

            }
        }

        private bool isStale(IWebElement el)
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

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}