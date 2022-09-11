using EcommerceDemo.commons;
using EcommerceDemo.page_interfaces;
using OpenQA.Selenium;

namespace EcommerceDemo.page_objects
{
    public class HomePage : CommonPage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
