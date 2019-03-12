using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    public class HomePage : Page
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }


        public HomePage OpenAdmin()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            return this;
        }

        public HomePage OpenClient()
        {
            driver.Url = "http://localhost/litecart/en/";
            return this;

        }

        public HomePage OpenClientRemote()
        {
            driver.Url = "http://litecart.stqa.ru/en/";
            return this;

        }

        public void ClickFirstProduct()
        {
            driver.FindElement(By.CssSelector("li.product")).Click();
        }
    }
}