using EcommerceDemo.helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace EcommerceDemo.browser_factory
{
    public class HFirefoxDriverFactory : IBrowserDriverFactory
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
