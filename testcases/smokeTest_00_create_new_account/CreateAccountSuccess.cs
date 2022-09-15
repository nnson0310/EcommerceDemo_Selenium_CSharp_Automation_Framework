using EcommerceDemo.commons;
using EcommerceDemo.element_indentifiers;
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
            CreateNewAccountTest.NavigateToCreateNewAccountPage(driver, ICommonUI.CreateAccountHeaderLinkText);
            createNewAccountPage = PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);


            //test data
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
            createNewAccountPage.EnterToDynamicTextboxById(driver, firstName, ICreateNewAccountUI.FirstNameTextboxId);

            ReportLog.Info(testMethod + " - Step 02: Enter last_name = " + lastName);
            createNewAccountPage.EnterToDynamicTextboxById(driver, lastName, ICreateNewAccountUI.LastNameTextboxId);

            ReportLog.Info(testMethod + " - Step 03: Enter email = " + email);
            createNewAccountPage.EnterToDynamicTextboxById(driver, email, ICreateNewAccountUI.EmailTextboxId);

            ReportLog.Info(testMethod + " - Step 04: Enter password = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, ICreateNewAccountUI.PasswordTextboxId);

            ReportLog.Info(testMethod + " - Step 05: Enter confirm_password = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, ICreateNewAccountUI.ConfirmPasswordTextboxId);

            ReportLog.Info(testMethod + " - Step 06: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);
            customerAccountPage = PageInitManager.GetPageInitManager().GetCustomerAccountPage(driver);

            ReportLog.Info(testMethod + " - Step 07: Verify that '" + ICreateNewAccountUI.CreateSuccessMessage + "' message is displayed");
            Assert.That(customerAccountPage.GetSuccessMessageText(driver), Is.EqualTo(ICreateNewAccountUI.CreateSuccessMessage));

            ReportLog.Info(testMethod + " - Step 08: Verify that contact information is correct. Registered full_name = " + fullName + " and registered email = " + email);
            Assert.That(customerAccountPage.GetContactInfo(driver), Is.EqualTo(contactInfo));
        }
    }
}
