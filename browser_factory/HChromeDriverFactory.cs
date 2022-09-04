using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EcommerceDemo.browser_factory
{
    internal class HChromeDriverFactory : IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            ChromeOptions chromeOptions = new()
            {
                AcceptInsecureCertificates = true,
            };
            chromeOptions.AddArgument("--headless");
            
            return new ChromeDriver(chromeOptions);
        }
    }
}
