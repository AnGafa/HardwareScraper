using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Directory.GetCurrentDirectory());
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            ChromeDriver client = new ChromeDriver(service, options);
            client.Navigate().GoToUrl("https://www.wikipedia.org/");
            Console.Clear();
            Console.Write("Search term: ");
            string searchTerm = Console.ReadLine();

            IWebElement searchField = client.FindElementById("searchInput");
            searchField.SendKeys(searchTerm);

            IWebElement button = client.FindElement(By.TagName("button"));
            button.Click();
            IWebElement divMainContent = client.FindElementById("mw-content-text");
            IList<IWebElement> paragraphs = divMainContent.FindElements(By.TagName("p"));
            string article = string.Empty;
            foreach (IWebElement p in paragraphs)
            {
                article += p.Text + "\n";
            }
            Console.Clear();
            Console.WriteLine(searchTerm);
            Console.WriteLine();
            Console.WriteLine(article);
            Console.ReadKey();
        }
    }
}

