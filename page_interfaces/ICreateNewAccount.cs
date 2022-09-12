namespace EcommerceDemo.page_interfaces
{
    public interface ICreateNewAccount
    {
        public const string CREATE_AN_ACCOUNT_BUTTON = "xpath=//span[text()='{0}']//parent::button";
        public const string VALIDATION_ERROR_MESSAGE_LABEL_DIV = "xpath=//input[@id='{0}']//following-sibling::div[text()='{1}']";
        public const string ERROR_MESSAGE_LABEL_DIV = "xpath=//div[contains(@class, 'message-error')]//div[text()='{0}']";
    }
}
