using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace csharp_example
{
    public class Application
    {
        public IWebDriver driver;
        public WebDriverWait wait;
        public HomePage Home;
        public ItemPage Item;
        public CartPage Cart;

        public Application()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            Home = new HomePage(driver);
            Item = new ItemPage(driver);
            Cart = new CartPage(driver);
        }

        public Application AddToCartAndCount(int count)
        {
            Home.OpenClientRemote();

            for (int i = 0; i < count; i++)
            {
                Home.ClickFirstProduct();

                Item.AddToCart()
                    .CountTo(i)
                    .NavigateToHome();
            }

            return this;
        }

        public Application RemoveAllCartItems()
        {
            Cart.Open().RemoveItemsAll();
            return this;
        }

        public Application OpenAdmin()
        {
            Home.OpenAdmin();
            return this;
        }

        public Application OpenClient()
        {
            Home.OpenClient();
            return this;

        }

        public Application OpenClientRemote()
        {
            Home.OpenClientRemote();
            return this;

        }

        public void Quit()
        {
            driver.Quit();
        }
    }
}

