using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace CDPSevenTask
{
    [TestClass]
    public class UnitTest1
    {
        private static const string Login = ConfigurationManager.AppSettings["login"];
        private static const string Password = ConfigurationManager.AppSettings["password"];
        private static const string UserName = ConfigurationManager.AppSettings["userName"]; 
		private static const string Facebook = ConfigurationManager.AppSettings["userName"]; 
        private IWebDriver _driver;

        [TestInitialize]
        public void TestInit()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(Facebook);
        }

        [TestMethod]
        public void loginTest()
        {
            InputLogin(Login);
            InputPassword(Password);
            ClickLogin();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            CheckUserName(UserName);

        }

        private void InputLogin(string login)
        {
            var emailField = _driver.FindElement(By.XPath("//input[@id = 'email']"));
            emailField.SendKeys(login);
        }


        private void InputPassword(string password)
        {
            var passwordField = _driver.FindElement(By.XPath("//input[@id = 'pass']"));
            passwordField.SendKeys(password);
        }

        private void CheckUserName(string userName)
        {
            var nameLink = _driver.FindElement(By.XPath("//a[@class  = 'fbxWelcomeBoxName']")).Text;
            Assert.IsTrue(nameLink.Contains(UserName), "User name doesn't match expected result");
        }

        private void ClickLogin()
        {
            var loginButton = _driver.FindElement(By.XPath("//input[@type = 'submit']"));
            loginButton.Click();

        }
        [TestCleanup]
        public void TestCleanUp()
        {
            _driver.Quit();
        }
    }
}
