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
       // var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
       
       Autorization();
   }

   [Test]
    public void Authorization()
    {
      
        // - проверяем что мы находимся на нужной странице
        var news = driver.FindElement(By.CssSelector(("[data-tid='Feed']")));
            
        var currentUrl = driver.Url;
        
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news", 
            "current url = " + currentUrl + "а должен быть https://staff-testing.testkontur.ru/news");
        
    }

    [Test]
    public void CommunityTest()
    {
    
        // клик на сообщества
        var community = driver.FindElement(By.CssSelector(("[data-tid='Community']")));
        community.Click();
        
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//неявное ожидание
        
        var communityName = driver.FindElement((By.CssSelector(("[data-tid='Title']"))));
        Assert.That(driver.Url=="https://staff-testing.testkontur.ru/communities");
       
    }

    [Test]

    public void SearchPerson()
    {
        //клик на поле поиск сотрудника
        var search = driver.FindElement(By.CssSelector("[data-tid='SearchBar']"));
        search.Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//неявное ожидание
        var InputPerson = driver.FindElement(By.ClassName("react-ui-1oilwm3"));
        InputPerson.SendKeys("Часовских");
        var Person = driver.FindElement(By.CssSelector("[data-tid='ComboBoxMenu__item']"));
        Person.Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//неявное ожидание
        
        //я же правильно понимаю что нужно еще добавить проверку что мы действительно в профиль перешли?
    }

    [Test]
    //тестируем кнопку "создать" в разделе "мероприятия"
    public void Event()
    {
        //переходим на страницу "мероприятия"
        var events = driver.FindElement(By.CssSelector(("[data-tid='Events']")));
        events.Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//неявное ожидание
        var addEvents = driver.FindElement(By.CssSelector("[data-tid='AddButton']"));
        addEvents.Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//неявное ожидание
        
        //наверное с точки зрения полноты сценария лучше прям заполнить форму? Или можно сделать проверку что всплывашка "создать мероприятие" появилась и ок?
    }

    [Test]
    //logout

    public void Logout()
    
    {
        var profileMenu = driver.FindElement(By.CssSelector("[data-tid='DropdownButton']"));
        profileMenu.Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//неявное ожидание
        
        var buttonLogout = driver.FindElement(By.CssSelector("[data-tid='Logout']"));
        buttonLogout.Click();
        //проверяем что мы находимся на нужной странице, вот тут случился затык, не очень понимаю за что можно зацепиться
        //пыталась за ссылку, тест работает, но вылетает ассерт 
        
       
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