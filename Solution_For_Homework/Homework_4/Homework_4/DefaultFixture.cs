using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Homework_4
{
    [TestFixture]
    public abstract class DefaultFixture
    {
        protected IWebDriver driver;

        protected static IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

        protected string baseDir;
        protected string createdFolder;
        protected string destinationFolder;
        protected string folderName = DateTime.Now.Ticks.ToString();


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            switch (TestContext.Parameters["Browser"]?.ToLowerInvariant())
            {
                case "Chrome":
                case "GoogleChrome":
                    driver = new ChromeDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");

            destinationFolder = Path.GetFullPath(config.GetValue<string>("destinationFolder"));
            baseDir = TestContext.CurrentContext.WorkDirectory;
            createdFolder = Path.Combine(baseDir, folderName);

            if (!Directory.Exists(createdFolder))
            {
                Directory.CreateDirectory(createdFolder);
            }

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
        }

        [SetUp]
        public void SetUp()
        {
            driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/signin");
            driver.FindElement(By.Id("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Id("password")).SendKeys("abc123");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        [TearDown]        
        public void TearDown()
        {
            driver.FindElement(By.ClassName("dropdown-toggle")).Click();
            driver.FindElement(By.XPath("//a[text()='Sign Out']")).Click();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

            driver.Close();

            Directory.Move(createdFolder, Path.Combine(destinationFolder, folderName));
        }
    }
}
