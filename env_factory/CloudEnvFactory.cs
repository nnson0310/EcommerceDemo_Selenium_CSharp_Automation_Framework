using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace EcommerceDemo.env_factory
{
    public class CloudEnvFactory: IEnvFactory
    {
        private string browserName;
        private string browserVersion;
        private string os;
        private string osVersion;

        //private IWebDriver? driver;

        public CloudEnvFactory(string browserName, string browserVersion, string os, string osVersion)
        {
            this.browserName = browserName;
            this.browserVersion = browserVersion;
            this.os = os;
            this.osVersion = osVersion;
        }

        public IWebDriver InitDriver()
        {
            throw new NotImplementedException();
        }
    }
}