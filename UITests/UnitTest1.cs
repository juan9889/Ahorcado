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
    public void Test1()
    {
        driver.Url = test_url;

        System.Threading.Thread.Sleep(2000);

        //IWebElement searchText = driver.FindElement(By.CssSelector("[name = 'q']"));

        //searchText.SendKeys("LambdaTest");

        IWebElement searchButton = driver.FindElement(By.Id("btn_login"));

        searchButton.Click();
        
        Console.WriteLine("Test Passed");
    }

    [TearDown]
    public void close_Browser()
    {
        driver.Quit();
    }
}
