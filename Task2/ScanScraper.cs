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
using System.Xml.Schema;

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


            var xPathName = new List<string>();
            xPathName.Add("//*[@id='maincontent']/div[3]/div[1]/div[3]/div[2]/ol/li[1]/div/div[2]/strong/a");
            xPathName.Add("/html/body/div[1]/main/div[3]/div[1]/div[3]/div[2]/ol/li[2]/div/div[2]/strong/a");

            var xPathPrice = new List<string>();
            xPathPrice.Add("/html/body/div[1]/main/div[3]/div[1]/div[3]/div[2]/ol/li[1]/div/div[2]/div[2]/span/span");
            xPathPrice.Add("/html/body/div[1]/main/div[3]/div[1]/div[3]/div[2]/ol/li[2]/div/div[2]/div[2]/span/span");

            var xPathAvailability = new List<string>();
            xPathAvailability.Add("//li[contains(concat(' ',normalize-space(@class),' '),' item ')][1]//button[contains(concat(' ',normalize-space(@class),' '),' action ')]//span");
            xPathAvailability.Add("/html/body/div[1]/main/div[3]/div[1]/div[3]/div[2]/ol/li[2]/div/div[2]/div[3]/div/div[1]/form/button/span");

            IWebElement searchField = client.FindElementById("search");
            searchField.SendKeys(searchTerm);

            //IWebElement button = client.FindElement(By.TagName("button"));
            IWebElement button = client.FindElement(By.XPath("//button[@title='Search']"));
            button.Click();


            for(int x=0; x<2; x++)
            {
                // start loop
                ResultItem result = new ResultItem();

                /*
                IWebElement mainResult = client.FindElement(By.XPath(xPathStr[0]));
                mainResult.Click();*/

                IWebElement resultNameElement = client.FindElement(By.XPath(xPathName[x]));
                result.ProductName = resultNameElement.Text;

                IWebElement resultPriceElement = client.FindElement(By.XPath(xPathPrice[x]));
                result.Price= resultPriceElement.Text;

                IWebElement resultAvailabilityElement = client.FindElement(By.XPath(xPathAvailability[x]));
                result.Availability = resultAvailabilityElement.Text;

                results.Add(result);

                // end of loop
            }
            
            // here you will have array of results

            string article = string.Empty;
            return article;
        }
    }
}
