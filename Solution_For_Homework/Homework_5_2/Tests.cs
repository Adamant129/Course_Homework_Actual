using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;

namespace Homework_5_2
{
    [Author("Sanya")]
    public class Tests : DefaultFixture
    {
        [Test]
        [MaxTime(10000)]
        [Description("Verifying Sign In button text")]
        public void Test_1()
        {
            try
            {
                driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/");

                var actual = driver.FindElement(By.XPath("//li[1]/a")).Text;
                var expected = "Plans";
                log.Information($"Expected value: {expected}, actual: {actual}");

                Assert.That(actual, Is.EqualTo(expected));
            }
            catch (Exception ex)
            {
                log.Debug($"Exception occured : {ex.Message}, stack trace :{ex.StackTrace}");
            }

        }

        //Forced to always throw error
        [Test]
        [MaxTime(10000)]
        [Description("Verifying Sign Up button text")]
        public void Test_2()
        {
            try
            {
                driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/");

                var actual = driver.FindElement(By.XPath("//li[2]/a")).Text;
                var expected = "Productsgg";
                log.Information($"Expected value: {expected}, actual: {actual}");

                Assert.That(actual, Is.EqualTo(expected));
            }
            catch (Exception ex)
            {
                log.Debug($"Exception occured : {ex.Message}, stack trace :{ex.StackTrace}");
            }
        }


        [Test]
        [Order(1)]
        //[MaxTime(10000)]
        [Description("Verifying Basic subscription title")]
        public void Test_3()
        {
            try
            {
                driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/plans");

                var actual = driver.FindElement(By.XPath("//div/h3[1]")).Text;
                var expected = "Basic";
                log.Information($"Expected value: {expected}, actual: {actual}");

                Assert.That(actual, Is.EqualTo(expected));
            }
            catch (Exception ex)
            {
                log.Debug($"Exception occured : {ex.Message}, stack trace :{ex.StackTrace}");
            }
        }

        [MaxTime(10000)]
        [TestCaseSource(nameof(GetDataFromJson))]
        public void Test_4(int number)
        {
            try
            {
                var rand = TestContext.CurrentContext.Random.NextShort();
                var expected = number + rand;

                driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/calculations");

                driver.FindElement(By.Id("addition-value-1")).SendKeys(number.ToString());
                driver.FindElement(By.Id("addition-value-2")).SendKeys(rand.ToString());
                var fff = driver.FindElement(By.Id("addition-result")).GetAttribute("value");

                var actual = Convert.ToInt32(fff);

                log.Information($"Expected value: {expected}, actual: {actual}");

                Assert.That(actual, Is.EqualTo(expected));
            }
            catch (Exception ex)
            {
                log.Debug($"Exception occured : {ex.Message}, stack trace :{ex.StackTrace}");
            }
        }

        private static IEnumerable<int> GetDataFromJson()
        {
            return config.GetSection("testData").Get<int[]>();
        }
    }
}
