using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ahorcado.UI.Specs.StepDefinitions
{
    [Binding]
    [Scope(Feature = "RegistroNombreUsuarioYaExistente")]
    public class RegistroNombreUsuarioYaExistenteStepDefinitions
    {
        readonly string test_url = "https://localhost:7134";
        readonly IWebDriver driver;

        private readonly ScenarioContext context;

        public RegistroNombreUsuarioYaExistenteStepDefinitions(ScenarioContext injectedContext)
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
            Thread.Sleep(10000);
            IWebElement usernameField = driver.FindElement(By.Id("txt_nombreusuario"));
            Thread.Sleep(500);
            usernameField.Click();
            char[] username_chars = username.ToCharArray();
            foreach(char c in username_chars)
            {
                Thread.Sleep(500);
                usernameField.SendKeys(c.ToString());
            }
            Thread.Sleep(500);
            //usernameField.SendKeys(username);
        }

        [Given(@"I entered (.*) in the password field")]
        public void GivenIEnteredInThePasswordField(string password)
        {
            Thread.Sleep(500);
            IWebElement passwordField = driver.FindElement(By.Id("txt_password"));
            Thread.Sleep(500);
            passwordField.Click();
            Thread.Sleep(500);
            char[] pass_chars = password.ToCharArray();
            foreach (char c in pass_chars)
            {
                Thread.Sleep(500);
                passwordField.SendKeys(c.ToString());
            }
            Thread.Sleep(500);
            //passwordField.SendKeys(password);
        }

        [When(@"I click the Register button")]
        public void WhenIClickTheRegisterButton()
        {
            Thread.Sleep(5000);
            IWebElement registerButton = driver.FindElement(By.Id("btn_registro"));
            registerButton.Click();
        }

        [Then(@"I should get an error message saying username is already registered")]
        public void ThenIShouldGetAnErrorMessageSayingUsernameIsAlreadyRegistered()
        {
            Thread.Sleep(15000);
            IWebElement alert_usuario_ya_registrado = driver.FindElement(By.Id("alert_nombre_usuario_ya_registrado"));
            Assert.IsNotNull(alert_usuario_ya_registrado);
            driver.Close();
        }
    }
}
