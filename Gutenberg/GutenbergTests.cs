using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Gutenberg
{
    public class GutenbergTests
    {
        private WebDriver driver;



        [SetUp]
        public void Setup()
        {

            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            

        }

        [TearDown]

        public void TearDown()
        {
            driver.Quit();  
        }

        [Test]
        public void Test_SearchBook()
        {
            driver.Url = "https://www.gutenberg.org/";

            var searchButton = driver.FindElement(By.Id("menu-book-search"));
            searchButton.Clear();
            searchButton.SendKeys("Tom Sawyer");

            var goButton = driver.FindElement(By.Name("submit_search"));
            goButton.Click();

            Thread.Sleep(3000);

            var firstResult = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/div/ul/li[5]/a"));
            
            Assert.That(firstResult.Text, Is.EqualTo("The Adventures of Tom Sawyer, Complete\r\nMark Twain\r\n9989 downloads"));
            Assert.That(firstResult.Text.Contains("Tom Sawyer"));
        }

        [Test]
        public void Test_SearchNonExistingBook()
        {
            driver.Url = "https://www.gutenberg.org/";

            var searchButton = driver.FindElement(By.Id("menu-book-search"));
            searchButton.Clear();
            searchButton.SendKeys("bebebebebe");

            var goButton = driver.FindElement(By.Name("submit_search"));
            goButton.Click();

            Thread.Sleep(3000);

            var noRecord = driver.FindElement(By.ClassName("title"));

            Assert.That(noRecord.Text, Is.EqualTo("No records found."));
        }

    }
}