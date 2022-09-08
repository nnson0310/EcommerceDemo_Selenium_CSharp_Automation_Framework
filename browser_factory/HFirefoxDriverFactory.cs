using EcommerceDemo.commons;
using EcommerceDemo.helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace EcommerceDemo.browser_factory
{
    internal class HFirefoxDriverFactory : IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            FirefoxOptions firefoxOptions = new()
            {
                AcceptInsecureCertificates = true
            };
            firefoxOptions.AddArgument("--headless");
            firefoxOptions.AddArguments(MethodHelper.GetDriverOptionArguments());

            return new FirefoxDriver(MethodHelper.GetBrowserDriverDir(), firefoxOptions);
        }
    }
}
