using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using System;

namespace HardwareScraper
{
    class RedditScraper : Scraper
    {

        public RedditScraper()
        {

            for (int i = 0; i < numberOfItems; i++)
            {
                XPathParameters param = new XPathParameters
                {
                    XPathNameParameter = "/html/body/div[1]/div/div/div/div[2]/div/div/div/div[2]/div[3]/div[1]/div[3]/div[" + (1 + i) + "]/div/div/div[2]/div/div[2]/div[1]/div[2]/a/div/h3",
                    XPathExternalUrl = "/html/body/div[1]/div/div/div/div[2]/div/div/div/div[2]/div[3]/div[1]/div[3]/div[" + (1 + i) + "]/div/div/div[2]/div/div[2]/div[1]/a"
                };


                this.xPathParams.Add(param);
            }

            this.sourceURL = "https://www.reddit.com/r/buildapcsales/";

        }

        public override List<ResultItem> Search(string searchTerm)
        {

            List<ResultItem> results = new List<ResultItem>();

            ChromeDriver client = ChromeDriverWrapper.Instance.Client;

            string url = this.sourceURL + "search?q=" + searchTerm + "&restrict_sr=1";
            client.Navigate().GoToUrl(url);

            System.Threading.Thread.Sleep(500);

            foreach (XPathParameters param in this.xPathParams)
            {
                // start loop
                ResultItem result = new ResultItem();

                IWebElement resultNameElement = client.FindElement(By.XPath(param.XPathNameParameter));
                result.ProductName = resultNameElement.Text;

                result.SourceURL = this.sourceURL;

                results.Add(result);
            }

            return results;
        }

    }
}
