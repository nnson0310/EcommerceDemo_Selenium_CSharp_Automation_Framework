using EcommerceDemo.helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace EcommerceDemo.browser_factory
{
    public class MSEdgeDriverFactory : IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            EdgeOptions edgeOptions = new()
            {
                AcceptInsecureCertificates = true
            };

            return new EdgeDriver(MethodHelper.GetBrowserDriverDir(), edgeOptions);
        }
    }
}
