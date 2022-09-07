using EcommerceDemo.commons;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EcommerceDemo.browser_factory
{
    internal class ChromeDriverFactory : IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            ChromeOptions chromeOptions = new()
            {
                AcceptInsecureCertificates = true
            };

            chromeOptions.AddArguments(GlobalConstants.optionArguments);

            return new ChromeDriver(chromeOptions);
        }
    }
}
