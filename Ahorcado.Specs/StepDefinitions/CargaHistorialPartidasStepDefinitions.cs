using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ahorcado.UI.Specs.StepDefinitions
{
    [Binding]
    [Scope(Feature = "CargaHistorialPartidas")]
    public class CargaHistorialPartidasStepDefinitions
    {
        readonly string test_url = "https://localhost:7134";
        readonly IWebDriver driver;

        private readonly ScenarioContext context;

        public CargaHistorialPartidasStepDefinitions(ScenarioContext injectedContext)
        {
            context = injectedContext;

            ChromeOptions chromeOptions = new();
            chromeOptions.AddArguments("--ignore-certificate-errors");
            //chromeOptions.AddArgument("no-sandbox");
            driver = new ChromeDriver(chromeOptions);

            driver.Manage().Window.Maximize();
            driver.Url = test_url;
        }

        [Given(@"I logged in with credentials (.*) and (.*)")]
        public void GivenILoggedIn(string username, string password)
        {
            Thread.Sleep(5000);
            IWebElement usernameField = driver.FindElement(By.Id("txt_nombreusuario"));
            usernameField.Click();
            usernameField.SendKeys(username);
            IWebElement passwordField = driver.FindElement(By.Id("txt_password"));
            passwordField.Click();
            passwordField.SendKeys(password);
            Thread.Sleep(5000);
            IWebElement loginButton = driver.FindElement(By.Id("btn_login"));
            loginButton.Click();
        }

        [When(@"I click on the Game History button")]
        public void WhenIClickOnTheGameHistoryButton()
        {
            Thread.Sleep(10000);
            IWebElement navHistoryButton = driver.FindElement(By.Id("btn-nav-history"));
            navHistoryButton.Click();
        }

        [Then(@"It should display the user game history")]
        public void ThenItShouldDisplayTheUserGameHistory()
        {
            Thread.Sleep(30000);
            IWebElement alert_datos_incorrectos = driver.FindElement(By.Id("table-partidas"));
            Assert.IsNotNull(alert_datos_incorrectos);
            driver.Close();
        }
    }
}
