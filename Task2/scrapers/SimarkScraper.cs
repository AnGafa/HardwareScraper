﻿using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;

namespace HardwareScraper
{
    class SimarkScraper : Scraper
    {

        public SimarkScraper()
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                XPathParameters param = new XPathParameters
                {
                    XPathNameParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[" + (2 + i) + "]/td[2]/a",
                    XPathPriceParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[" + (2 + i) + "]/td[3]/span[1]",
                    XPathAvailabilityParameter = "//*[@id='ctl00_MainContent_ProductsListView_DataGrid']/tbody/tr[" + (2 + i) + "]/td[4]/span[2]"
                };
                this.xPathParams.Add(param);
            }

            this.sourceURL = "https://www.simarksupplies.com/";
        }

        public override List<ResultItem> Search(string searchTerm)
        {
            ChromeDriver client = ChromeDriverWrapper.Instance.Client;

            List<ResultItem> results = new List<ResultItem>();

            string url = this.sourceURL + "Product.aspx/Search/" + searchTerm;
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

                result.SourceURL = this.sourceURL;

                results.Add(result);
            }

            return results;
        }

    }
}
