using EcommerceDemo.commons;
using EcommerceDemo.helpers;
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

            firefoxOptions.AddArguments(MethodHelper.GetDriverOptionArguments());

            return new FirefoxDriver(MethodHelper.GetBrowserDriverDir(), firefoxOptions);
        }
    }
}
