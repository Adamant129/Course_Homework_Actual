using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Homework_5
{
    [TestFixture]
    public abstract class DefaultFixture
    {
        #region Variables declaration 
        protected IWebDriver driver;

        protected static IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

        protected string baseDir;
        protected string createdFolder;
        protected string folderName;
        protected static ILogger log;
        #endregion

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

            folderName = DateTime.Now.ToString("dd-MM-yyyy_hh-mm");

            baseDir = TestContext.CurrentContext.WorkDirectory;
            createdFolder = Path.Combine(baseDir, folderName);

            log = new LoggerConfiguration()
                 .Enrich.WithProcessId()
                 .MinimumLevel.Debug()
                 .WriteTo.Console()
                 .WriteTo.File(Path.Combine(createdFolder, "detailedLogs.txt"), outputTemplate :
                    "[{Timestamp:HH:mm:ss} {Level:u3} ProcessId: {ProcessId}] {Message:lj}{NewLine}")
                 .WriteTo.File(Path.Combine(createdFolder, "logs.txt"), restrictedToMinimumLevel: LogEventLevel.Information)
                 .CreateLogger();

            if (!Directory.Exists(createdFolder))
            {
                Directory.CreateDirectory(createdFolder);
            }
        }

        [SetUp]
        public void SetUp()
        {
            var login = "admin@mail.com";
            var password = "abc123";

            driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/signin");
            driver.FindElement(By.Id("email")).SendKeys(login);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();

            log.Information($"Sign in atata-framework.github.io with credentials login = {login}, password = {password}");
        }

        [TearDown]
        public void TearDown()
        {
            driver.FindElement(By.ClassName("dropdown-toggle")).Click();
            driver.FindElement(By.XPath("//a[text()='Sign Out']")).Click();

            log.Information("Sign out atata-framework.github.io");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Close();
            Log.CloseAndFlush();
        }
    }
}
