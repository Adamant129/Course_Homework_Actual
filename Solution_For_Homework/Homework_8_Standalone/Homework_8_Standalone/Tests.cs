using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Homework_8_Standalone
{
    public class Tests
    {

        IWebDriver driver;
        IJavaScriptExecutor jexec;

        [SetUp]
        public void SetUp()
        {
            //P.S. everything above should be executed headless.
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("start-maximized");//"headless",
            chromeOptions.AddUserProfilePreference("download.default_directory", Environment.SpecialFolder.Desktop);

            driver = new ChromeDriver(chromeOptions);
            jexec = (IJavaScriptExecutor)driver;
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }

        [Test]
        public void Test_1()
        {
            driver.Url = "https://unsplash.com/search/photos/test";

            var x = 0;
            var y = 0;
            var scrollYAmount = 200;

            while (y != Convert.ToInt32(jexec.ExecuteScript("return window.pageYOffset")))
            {
                y += scrollYAmount;
                jexec.ExecuteScript($"window.scrollBy({x}, {y})");
            }

            //Assert.True(promotionslabel.Displayed);
        }
    }
}
