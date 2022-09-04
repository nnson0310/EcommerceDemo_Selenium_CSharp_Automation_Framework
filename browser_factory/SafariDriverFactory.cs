using OpenQA.Selenium;
using OpenQA.Selenium.Safari;

namespace EcommerceDemo.browser_factory
{
    internal class SafariDriverFactory: IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            SafariOptions safariOptions = new()
            {
                AcceptInsecureCertificates = true
            };

            return new SafariDriver(safariOptions);
        }
    }
}
