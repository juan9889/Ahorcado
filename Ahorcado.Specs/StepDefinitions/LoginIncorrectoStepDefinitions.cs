using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ahorcado.UI.Specs.StepDefinitions
{
    [Binding]
    [Scope(Feature = "LoginIncorrecto")]
    public class LoginIncorrectoStepDefinitions
    {
        readonly string test_url = "https://localhost:7134";
        readonly IWebDriver driver;

        private readonly ScenarioContext context;

        public LoginIncorrectoStepDefinitions(ScenarioContext injectedContext)
        {
            context = injectedContext;

            ChromeOptions chromeOptions = new();
            chromeOptions.AddArguments("--ignore-certificate-errors");
            //chromeOptions.AddArgument("no-sandbox");
            driver = new ChromeDriver(chromeOptions);

            driver.Manage().Window.Maximize();
            driver.Url = test_url;
        }

        [Given(@"I entered a GUID in the username field")]
        public void GivenIEnteredAGUIDInTheUsernameField()
        {
            Thread.Sleep(10000);
            IWebElement usernameField = driver.FindElement(By.Id("txt_nombreusuario"));
            usernameField.Click();
            usernameField.SendKeys("33afcef7-b1fd-467c-a555-8119a28f7c7c");
        }

        [Given(@"I entered another GUID in the password field")]
        public void GivenIEnteredAnotherGUIDInThePasswordField()
        {
            IWebElement passwordField = driver.FindElement(By.Id("txt_password"));
            passwordField.Click();
            passwordField.SendKeys("f5f1fe2f-0264-41aa-a06e-9ed74d0aca58");
        }

        [When(@"I click on the Login button")]
        public void WhenIClickTheLoginButton()
        {
            Thread.Sleep(10000);
            IWebElement loginButton = driver.FindElement(By.Id("btn_login"));
            loginButton.Click();
        }

        [Then(@"I should get an error message saying user does not exist")]
        public void ThenIShouldGetAnErrorMessageSayingUserDoesNotExist()
        {
            Thread.Sleep(15000);
            IWebElement alert_datos_incorrectos = driver.FindElement(By.Id("alert_datos_incorrectos"));
            Assert.IsNotNull(alert_datos_incorrectos);
            driver.Close();
        }
    }
}
