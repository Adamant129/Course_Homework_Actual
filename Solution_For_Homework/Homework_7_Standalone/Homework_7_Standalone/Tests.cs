using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Homework_7_Standalone
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            //P.S. everything above should be executed headless.
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");

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
            #region Variables declaration
            var hyperLinkIcon = By.XPath("//img[@alt='Link']");
            var goToHomeLinks = By.XPath("//div[@id='contentblock']//a");
            var droppableLinks = By.XPath("//a[text()='Droppable']");
            var dropTargetLocator = By.Id("droppable");
            var draggableLocator = By.Id("draggable");
            #endregion

            driver.Manage().Window.Maximize();

            //Go to http://www.leafground.com/home.html
            driver.Url = "http://www.leafground.com/home.html";

            //Open “HyperLink” page in new tab and switch to it
            new Actions(driver).KeyDown(Keys.Control).Click(driver.FindElement(hyperLinkIcon)).Perform();
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            //Hover on “Go to Home Page” link
            new Actions(driver).MoveToElement(driver.FindElements(goToHomeLinks).First()).Perform();

            //Take a screenshot and save it somewhere
            var screenshot = (driver as ChromeDriver).GetScreenshot();
            var destinationPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var screenshotPath = Path.Combine(destinationPath, "screenshot.png");
            screenshot.SaveAsFile(screenshotPath);

            //Close the tab
            driver.Close();

            //Switch to first tab
            driver.SwitchTo().Window(driver.WindowHandles.First());

            //Go to https://jqueryui.com/demos/
            driver.Navigate().GoToUrl("https://jqueryui.com/demos/");

            //Navigate to “Droppable” demo (Interactions section)
            driver.FindElements(droppableLinks).First().Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            //Switch to frame
            driver.SwitchTo().Frame(driver.FindElement(By.ClassName("demo-frame")));
            Thread.Sleep(1000);

            //Drag & Drop the small box into a big one
            var dropTargetElement = driver.FindElement(dropTargetLocator);
            var draggableElement = driver.FindElement(draggableLocator);
            var actions = new Actions(driver);
            actions.DragAndDrop(draggableElement, dropTargetElement).Build().Perform();
            var text = driver.FindElement(dropTargetLocator).Text;

            //Verify that big box now contains text “Dropped!”
            Assert.AreEqual("Dropped!", text);
        }
    }
}
