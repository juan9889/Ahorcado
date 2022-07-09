using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ahorcado.Specs.StepDefinitions
{
    [Binding]
    [Scope(Feature = "LoginValidation")]
    public class LoginValidationStepDefinitions
    {
        readonly string test_url = "https://localhost:7134";
        readonly IWebDriver driver;

        private readonly ScenarioContext context;

        public LoginValidationStepDefinitions(ScenarioContext injectedContext)
        {
            context = injectedContext;

            ChromeOptions chromeOptions = new();
            chromeOptions.AddArguments("--ignore-certificate-errors");
            //chromeOptions.AddArgument("no-sandbox");
            driver = new ChromeDriver(chromeOptions);

            driver.Manage().Window.Maximize();
            driver.Url = test_url;
        }

        [Given(@"I entered (.*) in the username field")]
        public void GivenIEnteredInTheUsernameField(string username)
        {
            Thread.Sleep(5000);
            IWebElement usernameField = driver.FindElement(By.Id("txt_nombreusuario"));
            usernameField.Click();
            usernameField.SendKeys(username);
        }

        [Given(@"I entered (.*) in the password field")]
        public void GivenIEnteredInThePasswordField(string password)
        {
            IWebElement passwordField = driver.FindElement(By.Id("txt_password"));
            passwordField.Click();
            passwordField.SendKeys(password);
        }

        [Then(@"Buttons should be enabled")]
        public void ThenButtonsShouldBeEnabled()
        {
            IWebElement loginButton = driver.FindElement(By.Id("btn_login"));
            IWebElement registerButton = driver.FindElement(By.Id("btn_registro"));

            Assert.That(loginButton.Enabled, Is.True);
            Assert.That(registerButton.Enabled, Is.True);
        }

        [Then(@"Close the browser")]
        public void ThenCloseTheBrowser()
        {
            driver.Close();
        }
    }
}
