using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class PageContentTest : BasicTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CheckPage()
        {
            OpenClient();

            //Save values from main page

            IWebElement main = driver.FindElement(By.Id("box-campaigns"));
            IWebElement regularPriceEl = main.FindElement(By.ClassName("regular-price"));
            IWebElement campaignPriceEl = main.FindElement(By.ClassName("campaign-price"));

            
            String itemName = main.FindElement(By.ClassName("name")).Text;
            int regularPrice = int.Parse(regularPriceEl.Text.Replace("$", ""));
            int campaignPrice = int.Parse(campaignPriceEl.Text.Replace("$", ""));
            ArrayList regularPriceColor = getElementColor(regularPriceEl);
            ArrayList campaignPriceColor = getElementColor(campaignPriceEl);
            double regularPriceFontSize = getFontSize(regularPriceEl);
            double campaignPriceFontSize = getFontSize(campaignPriceEl);

            // Main page only 
            // Check decor
            Assert.IsTrue(isCrossed(regularPriceEl));
            Assert.IsTrue(isBold(campaignPriceEl));
            // Check color
            Assert.IsTrue(isGrey(regularPriceColor));
            Assert.IsTrue(isRed(campaignPriceColor));
            //Check font size
            Assert.IsTrue(regularPriceFontSize < campaignPriceFontSize);


            //Save values from product page
            main.FindElement(By.CssSelector("li.product")).Click();

            main = driver.FindElement(By.CssSelector("#main"));
            IWebElement regularPriceElProd = main.FindElement(By.ClassName("regular-price"));
            IWebElement campaignPriceElProd = main.FindElement(By.ClassName("campaign-price"));

            String itemNameProd = main.FindElement(By.CssSelector("h1.title")).Text;
            int regularPriceProd = int.Parse(regularPriceElProd.Text.Replace("$", ""));
            int campaignPriceProd = int.Parse(campaignPriceElProd.Text.Replace("$", ""));
            ArrayList regularPriceColorProd = getElementColor(regularPriceElProd);
            ArrayList campaignPriceColorProd = getElementColor(campaignPriceElProd);
            double regularPriceFontSizeProd = getFontSize(regularPriceElProd);
            double campaignPriceFontSizeProd = getFontSize(campaignPriceElProd);


            // Prod page only 
            // Check decor
            Assert.IsTrue(isCrossed(regularPriceElProd));
            Assert.IsTrue(isBold(campaignPriceElProd));
            // Check color
            Assert.IsTrue(isGrey(regularPriceColorProd));
            Assert.IsTrue(isRed(campaignPriceColorProd));
            //Check font size
            Assert.IsTrue(regularPriceFontSizeProd < campaignPriceFontSizeProd);

            // Compare Main to Prod
            // Check names
            Assert.IsTrue(itemName.Equals(itemNameProd));
            // Check prices
            Assert.IsTrue(regularPrice.Equals(regularPriceProd));
            Assert.IsTrue(campaignPrice.Equals(campaignPriceProd));


        }
        private Boolean isCrossed(IWebElement el)
        { 
            return el.GetCssValue("text-decoration").Contains("line-through");
        }
        private Boolean isBold(IWebElement el)
        {
            return el.GetAttribute("tagName").Contains("STRONG");
        }

        private double getFontSize(IWebElement el)
        {
            return Convert.ToDouble(
                           el.GetCssValue("font-size")
                           .Replace("px", "")
                           .Replace(".", ","));
        }

        private ArrayList getElementColor(IWebElement el)
        {
            String elColor = el.GetCssValue("color");
            ArrayList trimmedColor = new ArrayList(elColor.Split(',', ')', '('));
            trimmedColor.RemoveAt(0);
            trimmedColor.RemoveAt(trimmedColor.Count - 1);
            return trimmedColor;
        }
        private Boolean isRed (ArrayList rgba)
        {
            if ((rgba[1] == rgba[2]).Equals(rgba[1] == "0"))
            {
                return true;
            }
            return false;
        }
        private Boolean isGrey(ArrayList rgba)
        {
            if ((rgba[0] == rgba[1]).Equals(rgba[0] == rgba[2]))
            {
                return true;
            }
            return false;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}