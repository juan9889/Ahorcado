using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ahorcado.UI.Specs.StepDefinitions
{
    [Binding]
    public class JuegoStepDefinitions
    {
        readonly string test_url = "https://localhost:7134";
        readonly IWebDriver driver;

        private readonly ScenarioContext context;

        public JuegoStepDefinitions(ScenarioContext injectedContext)
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
            Thread.Sleep(2000);
            IWebElement usernameField = driver.FindElement(By.Id("txt_nombreusuario"));
            usernameField.Click();
            usernameField.SendKeys(username);
            IWebElement passwordField = driver.FindElement(By.Id("txt_password"));
            passwordField.Click();
            passwordField.SendKeys(password);
            Thread.Sleep(2000);
            IWebElement loginButton = driver.FindElement(By.Id("btn_login"));
            loginButton.Click();
            Thread.Sleep(3000);
        }

        [Given(@"I start a new game on Easy difficulty")]
        public void GivenIStartANewGameOnJuego_Difficulty_FacilDifficulty()
        {
            IWebElement elegirFacilButton = driver.FindElement(By.Id("btn_elegir_facil"));
            elegirFacilButton.Click();
            Thread.Sleep(2000);
        }

        [When(@"I type all the letters in ARBOL")]
        public void WhenITypeAllTheLettersInARBOL()
        {
            IWebElement probarButton = driver.FindElement(By.Id("btn_probar_letra"));
            IWebElement letra_field = driver.FindElement(By.Id("txt_letra_a_probar"));
            if (letra_field.Displayed)
            {
                Console.WriteLine("HAY UN FIELD PARA LETRAS");
            }
            letra_field.SendKeys("A");
            probarButton.Click();
            Thread.Sleep(200);
            letra_field.SendKeys("R");
            probarButton.Click();
            Thread.Sleep(200);
            letra_field.SendKeys("B");
            probarButton.Click();
            Thread.Sleep(200);
            letra_field.SendKeys("O");
            probarButton.Click();
            Thread.Sleep(200);
            letra_field.SendKeys("L");
            probarButton.Click();
            Thread.Sleep(2000);
        }

        [Then(@"It should display a victory message")]
        public void ThenItShouldDisplayAVictoryMessage()
        {
            IWebElement alert_ganaste = driver.FindElement(By.Id("alert"));
            Assert.AreEqual("ganaste", alert_ganaste.GetAttribute("value"));
        }
    }
}
