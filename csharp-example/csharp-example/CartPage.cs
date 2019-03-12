using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    public class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public CartPage Open()
        {
            driver.FindElement(By.XPath("//a[contains(.,'Checkout »')]")).Click();
            return this;
        }

        public CartPage RemoveItemsAll()
        {
            IWebElement boxCheckoutCart = driver.FindElement(By.Id("box-checkout-cart"));
            int uniqueItemsCount = boxCheckoutCart.FindElements(By.CssSelector("li.item")).Count;

            IWebElement orderSummary = driver.FindElement(By.Id("order_confirmation-wrapper"));

            for (int i = 0; i < uniqueItemsCount; i++)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(.,'Remove')]")));
                driver.FindElement(By.XPath("//button[contains(.,'Remove')]")).Click();

                if (BasicTest.isStale(orderSummary))
                {
                    driver.FindElement(By.XPath("//button[contains(.,'Remove')]")).Click();
                    orderSummary = driver.FindElement(By.Id("order_confirmation-wrapper"));
                }
            }
            return this;
        }

        public void NavigateToHome()
        {
            driver.FindElement(By.CssSelector(".fa-home")).Click();
        }
    }
}