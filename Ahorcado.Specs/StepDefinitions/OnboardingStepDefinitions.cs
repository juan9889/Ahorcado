using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ahorcado.Specs.StepDefinitions
{
    [Binding]
    public class OnboardingStepDefinitions
    {
        readonly string test_url = "http://168.197.48.101";
        readonly IWebDriver driver;

        private readonly ScenarioContext context;

        public OnboardingStepDefinitions(ScenarioContext injectedContext)
        {
            context = injectedContext;

            ChromeOptions chromeOptions = new();
            chromeOptions.AddArguments("--ignore-certificate-errors");
            driver = new ChromeDriver(chromeOptions);

            driver.Manage().Window.Maximize();
            driver.Url = test_url;
        }

        [Given(@"I entered (.*) in the username field")]
        public void GivenIEnteredInTheUsernameField(string username)
        {
            Thread.Sleep(2000);
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
