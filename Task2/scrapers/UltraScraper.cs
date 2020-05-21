using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using System;

namespace HardwareScraper
{
    class UltraScraper : Scraper
    {

        public UltraScraper()
        {

            for (int i = 0; i < numberOfItems; i++)
            {
                XPathParameters param = new XPathParameters
                {
                    XPathNameParameter = "/html/body/section[5]/div/div/div/div[2]/div[2]/div[" + (1 + i) + "]/div[2]/div[1]/a",
                    XPathPriceParameter = "/html/body/section[5]/div/div/div/div[2]/div[2]/div[" + (1 + i) + "]/div[2]/div[2]",
                    XPathAvailabilityParameter = "/html/body/section[5]/div/div/div/div[2]/div[2]/div[" + (1 + i) + "]/div[2]/div[3]"
                };

                this.xPathParams.Add(param);
            }

            this.sourceURL = "https://ultramalta.com/";

        }

        public override List<ResultItem> Search(string searchTerm)
        {

            List<ResultItem> results = new List<ResultItem>();

            ChromeDriver client = ChromeDriverWrapper.Instance.Client;

            string url = this.sourceURL + "search.asp?keyword=" + searchTerm;
            client.Navigate().GoToUrl(url);

            System.Threading.Thread.Sleep(500);

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
