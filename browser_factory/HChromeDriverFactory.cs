using EcommerceDemo.commons;
using EcommerceDemo.helpers;
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
            chromeOptions.AddArguments(MethodHelper.GetDriverOptionArguments());

            return new ChromeDriver(MethodHelper.GetBrowserDriverDir(), chromeOptions);
        }
    }
}
