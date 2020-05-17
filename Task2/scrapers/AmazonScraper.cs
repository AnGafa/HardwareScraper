using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;

namespace HardwareScraper
{
    class AmazonScraper : Scraper
    {

        public AmazonScraper()
        {
            XPathParameters param = new XPathParameters();
            param.XPathNameParameter =             "/html/body/div[1]/div[1]/div[1]/div[2]/div/span[4]/div[1]/div[9]/div/span/div/div/div[2]/div[2]/div/div[1]/div/div/div[1]/h2/a/span";///html/body/div[1]/div[1]/div[1]/div[2]/div/span[4]/div[2]/div[9]/div/span/div/div/div[2]/div[2]/div/div[1]/div/div/div[1]/h2/a/span
            param.XPathPriceParameter =            "/html/body/div[1]/div[1]/div[1]/div[2]/div/span[4]/div[1]/div[9]/div/span/div/div/div[2]/div[2]/div/div[2]/div[1]/div/div/div"; // 3 spans -- symbol, full, cents
            param.XPathAvailabilityParameter =     "/html/body/div[1]/div[1]/div[1]/div[2]/div/span[4]/div[1]/div[9]/div/span/div/div/div[2]/div[2]/div/div[2]/div[1]/div/div[2]/div/span";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter =          "/html/body/div[1]/div[1]/div[1]/div[2]/div/span[4]/div[1]/div[10]/div/span/div/div/div[2]/div[2]/div/div[1]/div/div/div[1]/h2/a/span";
            param.XPathPriceParameter =         "/html/body/div[1]/div[1]/div[1]/div[2]/div/span[4]/div[1]/div[10]/div/span/div/div/div[2]/div[2]/div/div[2]/div[1]/div/div[1]/div/div/a/span"; // sometimes no info -- try statement?
            param.XPathAvailabilityParameter =  "/html/body/div[1]/div[1]/div[1]/div[2]/div/span[4]/div[1]/div[10]/div/span/div/div/div[2]/div[2]/div/div[2]/div[1]/div/div[2]/div/span";// same as ^
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {
            ChromeDriver client = ChromeDriverWrapper.Instance.Client;


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
