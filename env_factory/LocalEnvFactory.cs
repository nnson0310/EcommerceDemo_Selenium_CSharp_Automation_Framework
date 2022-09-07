using EcommerceDemo.browser_factory;
using OpenQA.Selenium;
using Browser = EcommerceDemo.enums.EnumCollect.BrowserName;

namespace EcommerceDemo.env_factory
{
    internal class LocalEnvFactory: IEnvFactory
    {
        private readonly string browserName;
        private IWebDriver? driver;

        public LocalEnvFactory(string browserName)
        {
            this.browserName = browserName;
        }

        public IWebDriver InitDriver()
        {
            Browser browser = (Browser)Enum.Parse(typeof(Browser), browserName, true);

            if (browser == Browser.Chrome)
            {
                driver = new ChromeDriverFactory().CreateDriver();
            }
            else if (browser == Browser.Firefox)
            {
                driver = new FirefoxDriverFactory().CreateDriver();
            }
            else if (browser == Browser.Edge)
            {
                driver = new EdgeDriverFactory().CreateDriver();
            }
            else if (browser == Browser.H_Chrome)
            {
                driver = new HChromeDriverFactory().CreateDriver();
            }
            else if (browser == Browser.H_Firefox)
            {
                driver = new HFirefoxDriverFactory().CreateDriver();
            }
            else if (browser == Browser.CocCoc)
            {
                driver = new CocCocDriverFactory().CreateDriver();
            }
            else
            {
                driver = new SafariDriverFactory().CreateDriver();
            }

            return driver;
        }
    }
}
