using OpenQA.Selenium;

namespace EcommerceDemo.env_factory
{
    internal interface IEnvFactory
    {
        public IWebDriver InitDriver();
    }
}
