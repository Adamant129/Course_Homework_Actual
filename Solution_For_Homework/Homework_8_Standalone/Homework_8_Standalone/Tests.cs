using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Threading;

namespace Homework_8_Standalone
{
    public class Tests
    {
        IWebDriver driver;
        IJavaScriptExecutor jexec;
        string downloadDirectory;


        [SetUp]
        public void SetUp()
        {
            downloadDirectory = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "DownloadedPhotos"));

            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("start-maximized");//"headless",
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadDirectory);

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

            var lastElementLocator = By.XPath("(//figure[@itemprop='image'])[last()]");
            var elementsGrid = By.XPath("//figure[@itemprop='image']");
            var allScrolledDistance = 0;
            var temp = Convert.ToInt32(jexec.ExecuteScript("return document.body.scrollHeight"));


            while (allScrolledDistance != temp)
            {
                jexec.ExecuteScript("window.scrollTo(0,document.body.scrollHeight);");
                Thread.Sleep(2000);
                var scrolledDistance = Convert.ToInt32(jexec.ExecuteScript("return document.body.scrollHeight"));
                allScrolledDistance = temp;
                temp = scrolledDistance;
            }

            driver.FindElement(lastElementLocator).Click();
            driver.FindElement(By.XPath("//span[text()='Download free']")).Click();

            Thread.Sleep(3000);
        }
    }
}
