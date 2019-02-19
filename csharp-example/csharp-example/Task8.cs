﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class StickersTest:BasicTest
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
            driver.Url = "http://localhost/litecart/en/";
            IList<IWebElement> storeItems = driver.FindElements(By.CssSelector("li.product"));

            foreach (IWebElement el in storeItems)
            {
                Assert.IsTrue(IsElementPresent(el, By.CssSelector("div.sticker")));
            }

        }
        [TearDown]
        public void stop()
        {
            //driver.Quit();
            //driver = null;
        }
    }
}