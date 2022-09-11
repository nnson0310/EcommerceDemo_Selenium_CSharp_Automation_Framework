using EcommerceDemo.commons;
using EcommerceDemo.helpers;
using EcommerceDemo.page_interfaces;
using OpenQA.Selenium;

namespace EcommerceDemo.page_objects
{
    public class CommonPage : BasePage
    {
        public CreateNewAccountPage ClickToCreateAnAccountLink(IWebDriver driver, string linkText)
        {
            WaitForElementClickable(driver, ICommon.CREATE_AN_ACCOUNT_LINK, linkText);
            ClickToElement(driver, ICommon.CREATE_AN_ACCOUNT_LINK, linkText);
            return PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);
        }

        public void EnterToDynamicTextboxById(IWebDriver driver, string value, string elemenId)
        {
            WaitForElementVisible(driver, ICommon.DYNAMIC_TEXTBOX_BY_ID, elemenId);
            SendKeyToElement(driver, ICommon.DYNAMIC_TEXTBOX_BY_ID, value, elemenId);
        }
    }
}
