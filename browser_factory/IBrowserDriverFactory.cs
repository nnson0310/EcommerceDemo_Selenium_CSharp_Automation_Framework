using OpenQA.Selenium;

namespace EcommerceDemo.browser_factory
{
    public interface IBrowserDriverFactory
    {
        public IWebDriver CreateDriver();
    }
}
