using EcommerceDemo.page_interfaces.user_common;
using OpenQA.Selenium;

namespace EcommerceDemo.page_objects.user_common
{
    public class UserCommonPage : CommonPage
    {
        public string GetSuccessMessageText(IWebDriver driver)
        {
            WaitForElementVisible(driver, IUserCommon.SUCCESS_MESSAGE_LABEL_DIV);
            return GetInnerTextOfElement(driver, IUserCommon.SUCCESS_MESSAGE_LABEL_DIV);
        }
    }
}
