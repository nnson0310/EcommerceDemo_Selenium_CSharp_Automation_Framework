using EcommerceDemo.helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EcommerceDemo.browser_factory
{
    public class ChromeDriverFactory : IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            ChromeOptions chromeOptions = new()
            {
                AcceptInsecureCertificates = true
            };

            chromeOptions.AddArguments(MethodHelper.GetDriverOptionArguments());

            return new ChromeDriver(MethodHelper.GetBrowserDriverDir(), chromeOptions);
        }
    }
}
