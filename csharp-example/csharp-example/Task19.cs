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
    public class CartPageObjectTest : BasicTest
    {
        [Test]
        public void AddProduct()
        {
            int prodToAddCount = 3;

            OpenClientRemote(driver);

            for (int i = 0; i < prodToAddCount; i++)
            {
                Home.clickFirstProduct();

                Item.AddToCart()
                    .CountTo(i)
                    .NavigateToHome();
            }

            Cart.Open()
                .RemoveItemsAll();

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}