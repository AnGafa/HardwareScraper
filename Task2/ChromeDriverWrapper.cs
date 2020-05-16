using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;

namespace HardwareScraper
{
    public class ChromeDriverWrapper
    {

        private static ChromeDriverWrapper instance = null;

        private ChromeDriver client;


        public static ChromeDriverWrapper Instance 
        { 
            get
            {
                if(instance == null)
                {
                    instance = new ChromeDriverWrapper();
                }

                return instance;
            }
        }


        public ChromeDriver Client
        {
            get
            {
                return client;
            }
        }


        private ChromeDriverWrapper()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Directory.GetCurrentDirectory());
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless");
            client = new ChromeDriver(service, options);
        }

    }
}
