using EcommerceDemo.commons;
using EcommerceDemo.extents;
using EcommerceDemo.helpers;
using EcommerceDemo.page_objects;
using EcommerceDemo.page_objects.user_common;
using EcommerceDemo.testcases.smokeTest_pre_condition;
using EcommerceDemo.utils;
using NUnit.Framework;
using OpenQA.Selenium;

namespace EcommerceDemo.testcases.smokeTest_00_create_new_account
{
    [TestFixture, Description("Verify that user can create new account success")]
    public class CreateAccountSuccess : BaseTest
    {
        private IWebDriver driver;
        private CreateNewAccountPage createNewAccountPage;
        private CustomerAccountPage? customerAccountPage;

        private const string firstNameFieldId = "firstname";
        private const string lastNameFieldId = "lastname";
        private const string emailFieldId = "email_address";
        private const string passwordFieldId = "password";
        private const string confirmPasswordFieldId = "password-confirmation";
        private const string createAnAccountHeaderLinkText = "Create an Account";
        private const string createAnAccountButtonTitle = "Create an Account";
        private const string successMessage = "Thank you for registering with Fake Online Clothing Store.";

        //test data
        private string firstName;
        private string lastName;
        private string fullName;
        private string contactInfo;
        private string email;
        private string password;

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            driver = GetDriver();

            //pre_condition
            CreateNewAccountTest.NavigateToCreateNewAccountPage(driver, createAnAccountHeaderLinkText);
            createNewAccountPage = PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);

            firstName = DataFaker.GetRandomFirstName();
            lastName = DataFaker.GetRandomLastName();
            email = DataFaker.GetRandomEmail();
            password = "Password123";
            fullName = firstName + " " + lastName;
            contactInfo = fullName + email;
        }

        [
            Test,
            Description("Verify that user can create new account with valid information")
        ]
        public void TC_Create_New_Account_10_Create_New_Account_Success()
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Enter first_name = " + firstName);
            createNewAccountPage.EnterToDynamicTextboxById(driver, firstName, firstNameFieldId);

            ReportLog.Info(testMethod + " - Step 02: Enter last_name = " + lastName);
            createNewAccountPage.EnterToDynamicTextboxById(driver, lastName, lastNameFieldId);

            ReportLog.Info(testMethod + " - Step 03: Enter email = " + email);
            createNewAccountPage.EnterToDynamicTextboxById(driver, email, emailFieldId);

            ReportLog.Info(testMethod + " - Step 04: Enter password = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, passwordFieldId);

            ReportLog.Info(testMethod + " - Step 05: Enter confirm_password = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, confirmPasswordFieldId);

            ReportLog.Info(testMethod + " - Step 06: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, passwordFieldId);
            customerAccountPage = PageInitManager.GetPageInitManager().GetCustomerAccountPage(driver);

            ReportLog.Info(testMethod + " - Step 07: Verify that '" + successMessage + "' error message is displayed");
            Assert.That(customerAccountPage.GetSuccessMessageText(driver), Is.EqualTo(successMessage));

            ReportLog.Info(testMethod + " - Step 08: Verify that contact information is correct. Registered full_name = " + fullName + " and registered email = " + email);
            Assert.That(customerAccountPage.GetContactInfo(driver), Is.EqualTo(contactInfo));
        }
    }
}
