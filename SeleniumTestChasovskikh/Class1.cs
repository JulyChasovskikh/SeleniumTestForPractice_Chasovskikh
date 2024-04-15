using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestChasovskikh;

public class TestForPractice
{
    [Test]
    public void Authorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox","--start-maximized","--disable-extensions");
        //зайти в хром
        var driver = new ChromeDriver(options);
        //перейти по урлу
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        Thread.Sleep(3000);
        //вводим логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("farille25@gmail.com");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("Dct,eltnpfvtxfntkmyj24031996");
        
        Thread.Sleep(3000);
        
        //Клик на кнопку Войти
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        
        Thread.Sleep(3000);

        var currentUrl = driver.Url;
        Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");
        
        //закрываем браузер и убиваем процесс драйвера
        driver.Quit();
    }
}