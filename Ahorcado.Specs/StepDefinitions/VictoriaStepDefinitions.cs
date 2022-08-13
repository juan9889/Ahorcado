using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ahorcado.UI.Specs.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Victoria")]
    public class VictoriaStepDefinitions
    {
        readonly string test_url = "https://localhost:7134";
        readonly IWebDriver driver;

        private readonly ScenarioContext context;

        public VictoriaStepDefinitions(ScenarioContext injectedContext)
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
            Thread.Sleep(15000);
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

        [Given(@"I start a new game on Easy difficulty")]
        public void GivenIStartANewGameOnJuego_Difficulty_FacilDifficulty()
        {
            Thread.Sleep(15000);
            IWebElement elegirFacilButton = driver.FindElement(By.Id("btn_elegir_facil"));
            elegirFacilButton.Click();
        }

        [When(@"I type all the letters in the correct word")]
        public void WhenITypeAllTheLettersInARBOL()
        {
            Thread.Sleep(30000);
            IWebElement probarButton = driver.FindElement(By.Id("btn_probar_letra"));
            IWebElement letra_field = driver.FindElement(By.Id("txt_letra_a_probar"));

            string palabra_correcta = driver.FindElement(By.Id("txt_palabra_correcta")).GetAttribute("value");
            char[] charArray = palabra_correcta.ToCharArray();
            char[] distinct_charArray = charArray.Distinct().ToArray();
            charArray = distinct_charArray;
            foreach(char c in charArray)
            {
                Thread.Sleep(1000);
                letra_field.SendKeys(c.ToString());
                probarButton.Click();
            }
        }

        [Then(@"It should display a victory message")]
        public void ThenItShouldDisplayAVictoryMessage()
        {
            Thread.Sleep(15000);
            IWebElement alert_ganaste = driver.FindElement(By.Id("alert"));
            Assert.AreEqual("ganaste", alert_ganaste.GetAttribute("value"));
            driver.Close();
        }
    }
}
