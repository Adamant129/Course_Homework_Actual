using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Homework_9_Standalone
{
    public class DefaultFixture
    {
        protected IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
