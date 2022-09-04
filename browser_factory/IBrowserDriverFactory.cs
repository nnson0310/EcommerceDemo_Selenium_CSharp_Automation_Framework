using OpenQA.Selenium;

namespace EcommerceDemo.browser_factory
{
    internal interface IBrowserDriverFactory
    {
        public IWebDriver CreateDriver();
    }
}
