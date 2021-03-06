using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ahorcado.UI.Specs.StepDefinitions
{
    [Binding]
    [Scope(Feature ="Derrota")]
    public class DerrotaStepDefinitions
    {
        readonly string test_url = "https://localhost:7134";
        readonly IWebDriver driver;

        private readonly ScenarioContext context;

        public DerrotaStepDefinitions(ScenarioContext injectedContext)
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
            Thread.Sleep(10000);
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
            Thread.Sleep(10000);
            IWebElement elegirFacilButton = driver.FindElement(By.Id("btn_elegir_facil"));
            elegirFacilButton.Click();
        }

        [When(@"I type the same incorrect letter six times in a row")]
        public void WhenITypeTheSameIncorrectLetterSixTimesInARow()
        {
            Thread.Sleep(30000);
            IWebElement probarButton = driver.FindElement(By.Id("btn_probar_letra"));
            IWebElement letra_field = driver.FindElement(By.Id("txt_letra_a_probar"));
            char[] letras = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'Z', 'Y' };
            string palabra_correcta = driver.FindElement(By.Id("txt_palabra_correcta")).GetAttribute("value");
            char[] charArray = palabra_correcta.ToCharArray();
            char letra_a_probar = 'X';
            for (int i = 0; i < charArray.Length; i++)
            {
                if (!charArray.Contains(letras[i]))
                {
                    letra_a_probar = letras[i];
                    break;
                }
            }
            for (int i = 0; i < 6; i++)
            {
                letra_field.SendKeys(letra_a_probar.ToString());
                probarButton.Click();
            }
        }

        [Then(@"It should display a defeat message")]
        public void ThenItShouldDisplayADefeatMessage()
        {
            Thread.Sleep(10000);
            IWebElement alert_perdiste = driver.FindElement(By.Id("alert"));
            Assert.AreEqual("perdiste", alert_perdiste.GetAttribute("value"));
            driver.Close();
        }
    }
}
