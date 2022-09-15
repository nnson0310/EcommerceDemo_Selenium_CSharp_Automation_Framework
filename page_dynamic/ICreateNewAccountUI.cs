namespace EcommerceDemo.element_indentifiers
{
    public interface ICreateNewAccountUI
    {
        public const string FirstNameTextboxId = "firstname";

        public const string LastNameTextboxId = "lastname";

        public const string EmailTextboxId = "email_address";

        public const string PasswordTextboxId = "password";

        public const string ConfirmPasswordTextboxId = "password-confirmation";

        public const string CreateAccountButtonName = "Create an Account";

        public const string RequireErrorMessage = "This is a required field.";

        public const string InvalidEmailErrorMessage = "Please enter a valid email address (Ex: johndoe@domain.com).";

        public const string MatchPasswordErrorMessage = "Please enter the same value again.";

        public const string InvalidFirstNameErrorMessage = "First Name is not valid!";

        public const string MinLengthErrorMessage = "Minimum length of this field must be equal or greater than 8 symbols. " +
            "Leading and trailing spaces will be ignored.";

        public const string ClassCharErrorMessage = "Minimum of different classes of characters in password is 3. " +
            "Classes of characters: Lower Case, Upper Case, Digits, Special Characters.";

        public const string CreateSuccessMessage = "Thank you for registering with Fake Online Clothing Store.";
    }
}
