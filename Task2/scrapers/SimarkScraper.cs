using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;

namespace HardwareScraper
{
    class SimarkScraper : Scraper
    {

        public SimarkScraper()
        {
            XPathParameters param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[2]/td[2]/a";
            param.XPathPriceParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[2]/td[3]/span[1]"; 
            param.XPathAvailabilityParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[2]/td[4]/span[2]";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[3]/td[2]/a";
            param.XPathPriceParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[3]/td[3]/span[1]";
            param.XPathAvailabilityParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[3]/td[4]/span[2]";
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {
            ChromeDriver client = ChromeDriverWrapper.Instance.Client;


            List<ResultItem> results = new List<ResultItem>();

            string url = "https://www.simarksupplies.com/Product.aspx/Search/" + searchTerm;
            client.Navigate().GoToUrl(url);

            foreach (XPathParameters param in this.xPathParams)
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
