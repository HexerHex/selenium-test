using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    public class ItemPage : Page
    {
        public ItemPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public ItemPage AddToCart()
        {
            if (BasicTest.IsElementPresent(driver, By.Name("options[Size]")))
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("options[Size]")));
                new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByIndex(1);
            }

            driver.FindElement(By.Name("add_cart_product")).Click();
            return this;

        }

        public ItemPage CountTo(int count)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format("//span[@class='quantity'][contains(.,{0})]", count + 1))));
            return this;
        }

        public ItemPage NavigateToHome()
        {
            driver.FindElement(By.CssSelector(".fa-home")).Click();
            return this;
        }
    }
}