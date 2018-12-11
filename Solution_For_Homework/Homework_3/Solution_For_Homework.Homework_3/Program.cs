using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Homework_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var driver = new ChromeDriver(Directory.GetCurrentDirectory()))
                {
                    Task1_Do(driver); /*   Get elements from https://atata-framework.github.io/atata-sample-app/#!/products
             *   under column Amount that have number 5 in their text using XPath 
             *   and CSS selectors   */

                    Task2_Do(driver); /*   Get from https://atata-framework.github.io/atata-sample-app/#!/plans
             *   numbers of projects from payment plans using XPath and CSS selectors    */

                    Task3_Do(driver); /*   Get timer element from http://www.seleniumframework.com/Practiceform/ 
             *   when there is 35 seconds remaining Explicitly 
             *   and when there is 30 seconds remaining Implicitly   */

                    Task4_Do(driver); /*   From https://tern.gp.gov.ua/ua/search.html
             *   select "За зменьшенням" from search dropdown and 
             *   select "Сортувати по: Даті" RadioButton    */

                    Task5_Do(driver); /*  From http://www.seleniumframework.com/Practiceform/
             *  fill in and submit "Subscribe" form     */
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Task1_Do(ChromeDriver driver)
        {
            driver.Navigate().
                GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/products");

            var elementsToRetrieve = driver.
                FindElementsByXPath("//div[@class='table-responsive']//td[3][contains(text(), '5')]");
        }

        private static void Task2_Do(ChromeDriver driver)
        {
            driver.Navigate().
                GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/plans");

            var elementsToRetrieve = driver.
                FindElementsByXPath("//b[@class = 'projects-num']");
        }

        private static void Task3_Do(ChromeDriver driver)
        {
            driver.Navigate().
                GoToUrl("http://www.seleniumframework.com/Practiceform/");

            var explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));

            var explicitWaitElement = explicitWait.
                Until(d => d.FindElement(By.Id("clock")).Text == "Seconds remaining: 35" ?
                    d.FindElement(By.Id("clock")) :
                    null).Text;

            driver.Navigate().Refresh();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            var implicitWaitElement = driver.FindElementByXPath("//span[@id='clock'][text()='Seconds remaining: 30']").Text;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        private static void Task4_Do(ChromeDriver driver)
        {
            driver.Navigate().
                GoToUrl("https://tern.gp.gov.ua/ua/search.html");

            driver.FindElementByXPath("//option[@value='desc']").Click();

            driver.FindElementByXPath("//input[@value='date']").Click();
        }

        private static void Task5_Do(ChromeDriver driver)
        {
            driver.Navigate().
                GoToUrl("http://www.seleniumframework.com/Practiceform/");

            driver.FindElementByXPath("//section[@id='text-11']//input[@name='email']").
                SendKeys("Example@gmail.com");

            driver.FindElementByXPath("//input[@value='Subscribe']").
                Click();
        }
    }
}

