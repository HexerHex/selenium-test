using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    public class ItemPage : Page
    {


        [FindsBy(How = How.Name, Using = "options[Size]")]
        IWebElement sizeSelector;

        [FindsBy(How = How.CssSelector, Using = "button[name =\"add_cart_product\"]")]
        IWebElement addToCartBtn;

        [FindsBy(How = How.CssSelector, Using = ".fa-home")]
        IWebElement homeBtn;


        public ItemPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public ItemPage AddToCart()
        {
            try
            {
                new SelectElement(sizeSelector).SelectByIndex(1);
            }
            catch (NoSuchElementException ex)
            {

            }

            addToCartBtn.Click();
            return this;

        }

        public ItemPage CountTo(int count)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format("//span[@class='quantity'][contains(.,{0})]", count + 1))));
            return this;
        }

        public void NavigateToHome()
        {
            homeBtn.Click();
        }

    }
}