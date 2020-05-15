using OpenQA.Selenium;
using System.Collections.Generic;

namespace HardwareScraper
{
    class KlikkScraper : Scraper
    {

        public KlikkScraper()
        {
            //client.Navigate().GoToUrl("https://www.klikk.com.mt/");       moved down

            XPathParameters param = new XPathParameters();
            param.XPathNameParameter = "/html/body/div[2]/div/main/section/div/div/div[2]/div/product-grid/div/div[3]/div/div/a/div[2]/p";
            param.XPathPriceParameter = "//*[@id='content']/div/div/div[2]/div/product-grid/div/div[3]/div/div/div[2]/div[1]/text";
            param.XPathAvailabilityParameter = "//*[@id='content']/div/div/div[2]/div/product-grid/div/div[3]/div/div/div[4]/span[2]";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='content']/div/div/div[2]/div/product-grid/div/div[4]/div/div/a/div[2]/p";
            param.XPathPriceParameter = "//*[@id='content']/div/div/div[2]/div/product-grid/div/div[4]/div/div/div[2]/div[1]/text";
            param.XPathAvailabilityParameter = "//*[@id='content']/div/div/div[2]/div/product-grid/div/div[4]/div/div/div[8]/span[2]";
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {

            List<ResultItem> results = new List<ResultItem>();


            string url = "https://www.klikk.com.mt/product?q=" + searchTerm;
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
