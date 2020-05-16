using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;

namespace HardwareScraper
{
    class ScanScraper : Scraper
    {

        public ScanScraper()
        {
            

            XPathParameters param = new XPathParameters();
            param.XPathNameParameter            = "//*[@id='maincontent']/div[3]/div[1]/div[3]/div[2]/ol/li[1]/div/div[2]/strong/a";
            param.XPathPriceParameter           = "/html/body/div[1]/main/div[3]/div[1]/div[3]/div[2]/ol/li[1]/div/div[2]/div[2]/span/span";
            param.XPathAvailabilityParameter    = "//li[contains(concat(' ',normalize-space(@class),' '),' item ')][1]//button[contains(concat(' ',normalize-space(@class),' '),' action ')]//span";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "/html/body/div[1]/main/div[3]/div[1]/div[3]/div[2]/ol/li[2]/div/div[2]/strong/a";
            param.XPathPriceParameter = "/html/body/div[1]/main/div[3]/div[1]/div[3]/div[2]/ol/li[2]/div/div[2]/div[2]/span/span";
            param.XPathAvailabilityParameter = "/html/body/div[1]/main/div[3]/div[1]/div[3]/div[2]/ol/li[2]/div/div[2]/div[3]/div/div[1]/form/button/span";
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {
            ChromeDriver client = ChromeDriverWrapper.Instance.Client;


            List<ResultItem> results = new List<ResultItem>();

            string url = "https://www.scanmalta.com/shop/catalogsearch/result/?q=" + searchTerm;
            client.Navigate().GoToUrl(url);

            foreach(XPathParameters param in this.xPathParams)
            {
                // start loop
                ResultItem result = new ResultItem();

                IWebElement resultNameElement = client.FindElement(By.XPath(param.XPathNameParameter));
                result.ProductName = resultNameElement.Text;

                IWebElement resultPriceElement = client.FindElement(By.XPath(param.XPathPriceParameter));
                result.Price = resultPriceElement.Text;

                IWebElement resultAvailabilityElement = client.FindElement(By.XPath(param.XPathAvailabilityParameter));
                result.Availability = resultAvailabilityElement.Text;

                results.Add(result);
            }

            return results;
        }

    }
}
