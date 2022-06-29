using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UITests;

public class Tests
{

    String test_url = "https://localhost:7134/";

    IWebDriver driver;
    [SetUp]
    public void Setup()
    {
        // Local Selenium WebDriver
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("--ignore-certificate-errors");

        driver = new ChromeDriver(chromeOptions);

        driver.Manage().Window.Maximize();
    }

    [Test]
    public void PartidaGanada()
    {
        driver.Url = test_url;

        System.Threading.Thread.Sleep(2000);

        //IWebElement searchText = driver.FindElement(By.CssSelector("[name = 'q']"));

        //searchText.SendKeys("LambdaTest");

        IWebElement loginButton = driver.FindElement(By.Id("btn_login"));
         IWebElement username_field = driver.FindElement(By.Id("txt_nombreusuario"));
        IWebElement pass_field = driver.FindElement(By.Id("txt_password"));

        System.Threading.Thread.Sleep(2000);
        username_field.SendKeys("Juan");
        System.Threading.Thread.Sleep(2000);
        pass_field.SendKeys("1234");
        System.Threading.Thread.Sleep(2000);

        loginButton.Click();
        System.Threading.Thread.Sleep(2000);
        IWebElement elegirFacilButton = driver.FindElement(By.Id("btn_elegir_facil"));
        System.Threading.Thread.Sleep(2000);
        elegirFacilButton.Click();
        System.Threading.Thread.Sleep(8000);
        IWebElement palabra_correcta_field = driver.FindElement(By.Id("txt_palabra_correcta"));
        
        string palabra_correcta = palabra_correcta_field.GetAttribute("value");
        char[] letras = palabra_correcta.ToCharArray();
        IWebElement probarButton = driver.FindElement(By.Id("btn_probar_letra"));
        IWebElement letra_field = driver.FindElement(By.Id("txt_letra_a_probar"));
        foreach (char letra in letras)
        {
            System.Threading.Thread.Sleep(1000);
            letra_field.Clear();
            letra_field.SendKeys(letra.ToString());
            System.Threading.Thread.Sleep(1000);
            probarButton.Click();
            System.Threading.Thread.Sleep(1000);
        }
        IWebElement alert_ganaste= driver.FindElement(By.Id("alert"));
        Assert.Equals(alert_ganaste.GetAttribute("value"), "ganaste");
        

    }

    [TearDown]
    public void close_Browser()
    {
        driver.Quit();
    }
}
