using EcommerceDemo.commons;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace EcommerceDemo.browser_factory
{
    internal class FirefoxDriverFactory : IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            FirefoxOptions firefoxOptions = new()
            {
                AcceptInsecureCertificates = true,
                // disable all browser logs except fatal errors
                LogLevel = FirefoxDriverLogLevel.Fatal
            };

            firefoxOptions.AddArguments(GlobalConstants.optionArguments);

            return new FirefoxDriver("D:\\browser_driver\\", firefoxOptions);
        }
    }
}
