
using EcommerceDemo.page_objects.user_common;
using OpenQA.Selenium;

namespace EcommerceDemo.page_objects
{
    public class PageInitManager
    {
        private static PageInitManager? pageInitManager;

        public static PageInitManager GetPageInitManager()
        {
            pageInitManager ??= new PageInitManager();
            return pageInitManager;
        }

        public HomePage GetHomePage(IWebDriver driver)
        {
            return new HomePage(driver);
        }

        public CreateNewAccountPage GetCreateNewAccountPage(IWebDriver driver)
        {
            return new CreateNewAccountPage(driver);
        }

        public CustomerAccountPage GetCustomerAccountPage(IWebDriver driver)
        {
            return new CustomerAccountPage(driver);
        }
    }
}
