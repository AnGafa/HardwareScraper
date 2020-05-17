using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;

namespace HardwareScraper
{
    class GamersScraper : Scraper
    {

        public GamersScraper()
        {

            for (int i = 0; i < numberOfItems; i++)
            {
                XPathParameters param = new XPathParameters
                {
                    XPathNameParameter = "//*[@id='main']/ul/li [" + (1 + i) + "]/div/div/div[1]/a[2]/h2",
                    XPathPriceParameter = "//*[@id='main']/ul/li[" + (1 + i) + "]/div/div/div[3]/div[1]/span/span/span"
                };

                this.xPathParams.Add(param);
            }

            this.sourceURL = "https://gamerslounge.mt/";
        }

        public override List<ResultItem> Search(string searchTerm)
        {
            ChromeDriver client = ChromeDriverWrapper.Instance.Client;


            List<ResultItem> results = new List<ResultItem>();

            string url = this.sourceURL + "/?s=" + searchTerm + "&post_type=product&dgwt_wcas=1";
            client.Navigate().GoToUrl(url);

            foreach (XPathParameters param in this.xPathParams)
            {
                // start loop
                ResultItem result = new ResultItem();

                IWebElement resultNameElement = client.FindElement(By.XPath(param.XPathNameParameter));
                result.ProductName = resultNameElement.Text;

                IWebElement resultPriceElement = client.FindElement(By.XPath(param.XPathPriceParameter));
                result.Price = resultPriceElement.Text;

                result.SourceURL = this.sourceURL;

                results.Add(result);
            }

            return results;
        }

    }
}
