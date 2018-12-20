using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Bogus;
using Homework_9_Standalone.Models;

namespace Homework_9_Standalone
{
    // TODO: MAke it
    public class Tests : DefaultFixture
    {
        //1. Navigate to https://hwdecoration20181219063908.azurewebsites.net/
        //2. Click register
        //3. Fill all fields
        //4. Click "Hello {Email}"
        //5. Verify the user data
        //6. Click log off
        //7. Click log in
        //8. Enter user login and password
        //9. Verify that user is logged in currectly

        [Test]
        public void Test1()
        {
            var emailLocator = By.Id("Email");
            var passwordLocator = By.Id("Password");
            var manageLinkLocator = By.XPath("//a[@title = 'Manage']");

            var userModel = new RegisterUserModel().GenerateFakeUser();

            driver.Navigate().GoToUrl("https://hwdecoration20181219063908.azurewebsites.net");
            driver.FindElement(By.Id("registerLink")).Click();
            driver.FindElement(By.Id("Email")).SendKeys(userModel.Email);
            driver.FindElement(By.Id("Name")).SendKeys(userModel.Name);
            driver.FindElement(By.Id("Surname")).SendKeys(userModel.Surname);
            driver.FindElement(By.Id("Company")).SendKeys(userModel.Company);
            driver.FindElement(passwordLocator).SendKeys(userModel.Password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(userModel.Password);

            driver.FindElement(By.XPath("//input[@type = 'submit']")).Click();

            driver.FindElement(manageLinkLocator).Click();

            driver.FindElement(By.Id("userName")).Text.Should().BeEquivalentTo(userModel.Name);
            driver.FindElement(By.Id("userSurname")).Text.Should().BeEquivalentTo(userModel.Surname);
            driver.FindElement(By.Id("UserCompany")).Text.Should().BeEquivalentTo(userModel.Company);
            driver.FindElement(By.Id("userEmail")).Text.Should().BeEquivalentTo(userModel.Email);

            driver.FindElement(By.XPath("//a[text() = 'Log off']")).Click();

            driver.FindElement(By.Id("loginLink")).Click();

            driver.FindElement(emailLocator).SendKeys(userModel.Email);
            driver.FindElement(passwordLocator).SendKeys(userModel.Password);

            driver.FindElement(By.XPath("//input[@value = 'Log in']")).Click();

            driver.FindElement(manageLinkLocator).Text.Should().BeEquivalentTo($"Hello {userModel.Email}!");

        }
    }
}
