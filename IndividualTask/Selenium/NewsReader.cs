using System;
using System.Collections.Generic;
using System.Linq;
using IndividualTask.Contracts;
using IndividualTask.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace IndividualTask.Selenium
{
    public class NewsReader : INewsReader
    {
        private const string BASIC_URL = "https://hromadske.ua/";
        private const string CHROME_DRIVER_DIRECTORY = @"C:\recover\LNU\chromedriver_win32";

        public IEnumerable<News> ReadNews()
        {
            var chromeOptions = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(CHROME_DRIVER_DIRECTORY);

            driver.Navigate().GoToUrl(BASIC_URL);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(d => d.FindElement(By.ClassName("HeaderMenu-logoWrapper")));

            var elements = driver.FindElements(By.ClassName("NewsCard"));

            var news = elements
                .Select(el => new News
                {
                    Url = el.GetAttribute("href"),
                }).ToList();

            foreach (var n in news)
            {
                try
                {
                    driver.Navigate().GoToUrl(n.Url);
                    wait.Until(d => d.FindElement(By.ClassName("HeaderMenu-logoWrapper")));
                    n.Title = driver.FindElement(By.ClassName("PostHeader-title")).Text;
                    n.Author = driver.FindElement(By.ClassName("PostAuthor-name")).Text;
                    n.ShortDescription = driver.FindElement(By.ClassName("PostContent-leadTextWrapper")).Text;
                    n.Date = driver.FindElement(By.ClassName("PostHeader-published")).Text;
                    n.Like = false; 
                    n.Dislike = false;


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while parsing an article! Details: {ex}");
                }

                yield return n;
            }

            driver.Close();

           // return news;
        }
    }
}
