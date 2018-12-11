using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Homework_8_Standalone
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            //P.S. everything above should be executed headless.
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless", "start-maximized");
            chromeOptions.AddUserProfilePreference("download.default_directory", Environment.SpecialFolder.Desktop);

            driver = new ChromeDriver(chromeOptions);
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
        }
    }
}
