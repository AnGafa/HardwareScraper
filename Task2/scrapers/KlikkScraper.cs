using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;

namespace HardwareScraper
{
    class KlikkScraper : Scraper
    {

        public KlikkScraper()
        {

            
            for (int i = 0; i < numberOfItems; i++)
            {
                XPathParameters param = new XPathParameters
                {
                    XPathNameParameter = "/html/body/div[2]/div/main/section/div/div/div[2]/div/product-grid/div/div[" + (3 + i) + "]/div/div/a/div[2]/p",
                    XPathPriceParameter = "/html/body/div[2]/div/main/section/div/div/div[2]/div/product-grid/div/div[" + (3 + i) + "]/div/div/div[2]/div[1]",
                    XPathAvailabilityParameter = "//*[@id='content']/div/div/div[2]/div/product-grid/div/div[" + (3 + i) + "]/div/div/div[4]/span[2]"
                };
                this.xPathParams.Add(param);
            }

            this.sourceURL = "https://www.klikk.com.mt/";

        
        }

        public override List<ResultItem> Search(string searchTerm)
        {
            ChromeDriver client = ChromeDriverWrapper.Instance.Client;

            List<ResultItem> results = new List<ResultItem>();


            string url =  this.sourceURL + "product?q=" + searchTerm;
            client.Navigate().GoToUrl(url);

            System.Threading.Thread.Sleep(3000);

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
