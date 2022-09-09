using EcommerceDemo.commons;
using EcommerceDemo.page_interfaces;
using OpenQA.Selenium;

namespace EcommerceDemo.page_objects
{
    public class HomePage : CommonPage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickToCreateAnAccountLink()
        {
            ClickToElement(driver, ICommon.CREATE_AN_ACCOUNT_LINK);
        }
    }
}
