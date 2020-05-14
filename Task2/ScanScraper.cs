using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HardwareScraper
{
    class ScanScraper
    {
        ChromeDriver client;

        public ScanScraper()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Directory.GetCurrentDirectory());
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            client = new ChromeDriver(service, options);
            client.Navigate().GoToUrl("http://scanmalta.com");
        }

        public string Search(string searchTerm)
        {
            List<ResultItem> results = new List<ResultItem>();
            
            
            ResultItem result = new ResultItem();

            IWebElement searchField = client.FindElementById("search");
            searchField.SendKeys(searchTerm);

            //IWebElement button = client.FindElement(By.TagName("button"));
            IWebElement button = client.FindElement(By.XPath("//button[@title='Search']"));
            button.Click();
            //IWebElement divMainContent = client.FindElement(By.XPath(""));

            string xPathStr = "//li[contains(concat(' ',normalize-space(@class),' '),' item ')][1]//a[contains(concat(' ',normalize-space(@class),' '),' product-item-link ')]";
            IWebElement mainResult = client.FindElement(By.XPath(xPathStr));
            mainResult.Click();

            string xPathName = "//span[contains(concat(' ',normalize-space(@class),' '),' base ')]";
            IWebElement resultNameElement = client.FindElement(By.XPath(xPathName));
            result.ProductName = resultNameElement.Text;

            string xPathPrice1 = "//div[contains(concat(' ',normalize-space(@class),' '),' product-info-price ')]//span[contains(concat(' ',normalize-space(@class),' '),' special-price ')]//span[contains(concat(' ',normalize-space(@class),' '),' price ')]";
            string xPathPrice2 = "//div[contains(concat(' ',normalize-space(@class),' '),' product-info-price ')]//span[contains(concat(' ',normalize-space(@class),' '),' price ')]";
            IWebElement resultPriceElement = client.FindElement(By.XPath(xPathPrice2));
            result.Price = resultPriceElement.Text;

            string xPathAvailability = "//div[contains(concat(' ',normalize-space(@class),' '),' product-info-stock-sku ')]//span";
            IWebElement resultAvailabilityElement = client.FindElement(By.XPath(xPathAvailability));
            result.Availability = resultAvailabilityElement.Text;


            string article = string.Empty;

            return article;
        }
    }
}
