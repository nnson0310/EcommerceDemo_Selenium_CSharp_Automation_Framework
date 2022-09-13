using EcommerceDemo.helpers;
using EcommerceDemo.page_interfaces.user_common;
using OpenQA.Selenium;

namespace EcommerceDemo.page_objects.user_common
{
    public class CustomerAccountPage : UserCommonPage
    {
        public IWebDriver driver;

        public CustomerAccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetContactInfo(IWebDriver driver)
        {
            WaitForElementVisible(driver, ICustomerAccount.CONTACT_INFO_LABEL_P);
            Console.WriteLine(MethodHelper.CleanString(GetInnerTextOfElement(driver, ICustomerAccount.CONTACT_INFO_LABEL_P)));
            return MethodHelper.CleanString(GetInnerTextOfElement(driver, ICustomerAccount.CONTACT_INFO_LABEL_P));
        }
    }
}
