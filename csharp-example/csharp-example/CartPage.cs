using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    public class CartPage : Page
    {

        [FindsBy(How = How.XPath, Using = "//a[contains(.,'Checkout »')]")]
        IWebElement checkoutBtn;

        [FindsBy(How = How.XPath, Using = "//button[contains(.,'Remove')]")]
        IWebElement removeBtn;

        [FindsBy(How = How.CssSelector, Using = ".fa-home")]
        IWebElement homeBtn;


        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public CartPage Open()
        {
            checkoutBtn.Click();
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
                removeBtn.Click();

                if (BasicTest.isStale(orderSummary))
                {
                    removeBtn.Click();
                    orderSummary = driver.FindElement(By.Id("order_confirmation-wrapper"));
                }
            }
            return this;
        }

        public void NavigateToHome()
        {
            homeBtn.Click();
        }
    }
}