using EcommerceDemo.page_interfaces;
using OpenQA.Selenium;

namespace EcommerceDemo.page_objects
{
    public class CreateNewAccountPage : CommonPage
    {
        private IWebDriver driver;

        public CreateNewAccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickToCreateAnAccountButton(IWebDriver driver, string buttonTitle)
        {
            WaitForElementClickable(driver, ICreateNewAccount.CREATE_AN_ACCOUNT_BUTTON, buttonTitle);
            ScrollToElement(driver, ICreateNewAccount.CREATE_AN_ACCOUNT_BUTTON, buttonTitle);
            ClickToElementByAction(driver, ICreateNewAccount.CREATE_AN_ACCOUNT_BUTTON, buttonTitle);
        }

        public bool IsValidationErrorMessageDisplayed(IWebDriver driver, string elementId, string errorMessage)
        {
            WaitForElementVisible(driver, ICreateNewAccount.VALIDATION_ERROR_MESSAGE_LABEL_DIV, elementId, errorMessage);
            return IsElementDisplayed(driver, ICreateNewAccount.VALIDATION_ERROR_MESSAGE_LABEL_DIV, elementId, errorMessage);
        }

       public bool IsErrorMessageDisplayed(IWebDriver driver, string errorMessage)
       {
            WaitForElementVisible(driver, ICreateNewAccount.ERROR_MESSAGE_LABEL_DIV, errorMessage);
            return IsElementDisplayed(driver, ICreateNewAccount.ERROR_MESSAGE_LABEL_DIV, errorMessage);
        }
    }
}
