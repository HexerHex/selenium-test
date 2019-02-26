using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class RegistrationTest:BasicTest
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
        public void RegisterUser()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string email = "test" + timestamp + "@email.com";
            string password = "123456";
            string firstname = "John";
            string lastname = "Doe";
            string address1 = "Some street";
            string postcode = "98101";
            string city = "Seattle";
            string state = "Washington";
            string phone = "+1 222 333 4444";
            string country = "United States";

            OpenClient(driver);

            driver.FindElement(By.XPath("//a[contains(., 'New customers click here')]")).Click();

            driver.FindElement(By.Name("firstname")).SendKeys(firstname);
            driver.FindElement(By.Name("lastname")).SendKeys(lastname);
            driver.FindElement(By.Name("address1")).SendKeys(address1);
            driver.FindElement(By.Name("postcode")).SendKeys(postcode);
            driver.FindElement(By.Name("city")).SendKeys(city);

            // Select Country
            driver.FindElement(By.CssSelector(".select2-selection__arrow")).Click();
            driver.FindElement(By.CssSelector(".select2-search__field")).SendKeys(country);
            new Actions(driver).SendKeys(Keys.Enter).Perform();

            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys(phone);

            IWebElement zoneSelector = driver.FindElement(By.XPath("//select[@name='zone_code']"));

            // Hack if zone selector is disabled
            if (zoneSelector.GetAttribute("disabled").Equals("true"))
            {
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.Name("confirmed_password")).SendKeys(password);
                driver.FindElement(By.Name("create_account")).Click();

                zoneSelector = driver.FindElement(By.XPath("//select[@name='zone_code']"));
            }

            SelectElement stateDropdown = new SelectElement(zoneSelector);
            stateDropdown.SelectByText(state);

            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(password);
            driver.FindElement(By.Name("create_account")).Click();

            driver.FindElement(By.XPath("//a[contains(.,'Logout')]")).Click();

            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();

            driver.FindElement(By.XPath("//a[contains(.,'Logout')]")).Click();

        }


        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}