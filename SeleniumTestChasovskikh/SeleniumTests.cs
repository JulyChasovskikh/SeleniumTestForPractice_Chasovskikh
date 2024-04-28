using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestChasovskikh;

public class TestForPractice
{

   public ChromeDriver driver;

   [SetUp]
   public void Setup()
   {
       
       var options = new ChromeOptions();
       options.AddArguments("--no-sandbox","--start-maximized","--disable-extensions");
       //зайти в хром
       driver = new ChromeDriver(options);
       
       Autorization();
       
       driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//неявное ожидание
   }

   [Test]
    public void Login()
    {
      
        // - проверяем что мы находимся на нужной странице
        var news = driver.FindElement(By.CssSelector(("[data-tid='Feed']")));
            
        var currentUrl = driver.Url;
        
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news", 
            "current url = " + currentUrl + "а должен быть https://staff-testing.testkontur.ru/news");
        
    }

    [Test]
    public void CommunityPage()
    {
    
        // клик на сообщества
        var community = driver.FindElement(By.CssSelector(("[data-tid='Community']")));
        community.Click();
        //подтверждаем, что мы на нужной странице
        var communityName = driver.FindElement((By.CssSelector(("[data-tid='Title']"))));
        Assert.That(driver.Url=="https://staff-testing.testkontur.ru/communities");
       
    }

    [Test]

    public void SearchPerson()
    {
        //клик на поле поиск сотрудника
        var search = driver.FindElement(By.CssSelector("[data-tid='SearchBar']"));
        search.Click();
        //Вводим тестовые данные
        var InputPerson = driver.FindElement(By.ClassName("react-ui-1oilwm3"));
        InputPerson.SendKeys("Часовских");
        //Кликаем на появившийся профиль
        var Person = driver.FindElement(By.CssSelector("[data-tid='ComboBoxMenu__item']"));
        Person.Click();
        //чек ссылки на профиль
        Assert.That(driver.Url=="https://staff-testing.testkontur.ru/profile/4f3f5dd7-04f1-4b2b-b5d6-a4f52cb002e6");
    }

    [Test]
    //тестируем кнопку "создать" в разделе "мероприятия"
    public void TestingMapInAddEvents()
    {
        //переходим на страницу "мероприятия"
        var events = driver.FindElement(By.CssSelector(("[data-tid='Events']")));
        events.Click();
        //жмем кнопку "создать"
        var addEvents = driver.FindElement(By.CssSelector("[data-tid='AddButton']"));
        addEvents.Click();
        //чекаем виджет карты
        var mapEvent = driver.FindElement(By.ClassName("map-wrapper"));
        Assert.That(mapEvent.Displayed, message:"Виджет карты не работает");


    }

    [Test]
    public void Logout()
    
    { 
        //вызываем выпадашку у аккаунта
        var profileMenu = driver.FindElement(By.CssSelector("[data-tid='DropdownButton']"));
        profileMenu.Click();
        //жмем "выйти"
        var buttonLogout = driver.FindElement(By.CssSelector("[data-tid='Logout']"));
        buttonLogout.Click();
        var LogoutHead = driver.FindElement(By.ClassName("login-page"));
        Assert.That(LogoutHead.Displayed, message:"Кнопка выйти не работает");
    }


    //метод авторизации
    
    public void Autorization()
    
    {
        //перейти по урлу
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//неявное ожидание
        
        //вводим логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("farille25@gmail.com");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("Dct,eltnpfvtxfntkmyj24031996");
        
        //Клик на кнопку Войти
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        
       
    }

    //закрываем браузер и убиваем процесс драйвера
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}