using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace EcommerceDemo.browser_factory
{
    internal class EdgeDriverFactory: IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            EdgeOptions edgeOptions = new()
            {
                AcceptInsecureCertificates = true
            };

            return new EdgeDriver(edgeOptions);
        }
    }
}
