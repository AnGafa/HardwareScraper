﻿using OpenQA.Selenium;
using System.Collections.Generic;

namespace HardwareScraper
{
    class AmazonScraper : Scraper
    {

        public AmazonScraper()
        {
            XPathParameters param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='search']/div[1]/div[2]/div/span[4]/div[1]/div[1]/div/span/div/div/div[2]/div[2]/div/div[1]/div/div/div[1]/h2/a/span";
            param.XPathPriceParameter = "//*[@id='search']/div[1]/div[2]/div/span[4]/div[1]/div[1]/div/span/div/div/div[2]/div[2]/div/div[2]/div[1]/div/div[1]/div/div/a/span/span[2]"; // 3 spans -- symbol, full, cents
            param.XPathAvailabilityParameter = "//*[@id='search']/div[1]/div[2]/div/span[4]/div[1]/div[1]/div/span/div/div/div[2]/div[2]/div/div[2]/div[1]/div/div[2]/div/span";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='search']/div[1]/div[2]/div/span[4]/div[1]/div[2]/div/span/div/div/div[2]/div[2]/div/div[1]/div/div/div[1]/h2/a/span";
            param.XPathPriceParameter = "//*[@id='search']/div[1]/div[2]/div/span[4]/div[1]/div[2]/div/span/div/div/div[2]/div[2]/div/div[2]/div[1]/div/div[1]/div/div/a/span/span[2]"; // sometimes no info -- if loop?
            param.XPathAvailabilityParameter = "//*[@id='search']/div[1]/div[2]/div/span[4]/div[1]/div[2]/div/span/div/div/div[2]/div[2]/div/div[2]/div[1]/div/div[2]/div/span";// same as ^
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {
            List<ResultItem> results = new List<ResultItem>();

            string url = "https://www.amazon.com/s?k=" + searchTerm;
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