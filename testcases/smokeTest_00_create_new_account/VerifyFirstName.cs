using EcommerceDemo.commons;
using EcommerceDemo.extents;
using EcommerceDemo.helpers;
using EcommerceDemo.page_objects;
using EcommerceDemo.testcases.smokeTest_pre_condition;
using EcommerceDemo.testdata;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection;

namespace EcommerceDemo.testcases.smokeTest_00_create_new_account
{
    [TestFixture, Description("Verify that first_name field of Create_New_Account page")]
    public class VerifyFirstName : BaseTest
    {
        private IWebDriver driver;
        private CreateNewAccountPage createNewAccountPage;

        private const string firstNameFieldId = "firstname";
        private const string lastNameFieldId = "lastname";
        private const string emailFieldId = "email_address";
        private const string passwordFieldId = "password";
        private const string passwordConfirmFieldId = "password-confirmation";
        private const string createAnAccountHeaderLinkText = "Create an Account";
        private const string createAnAccountButtonTitle = "Create an Account";
        private const string requireValidationErrorMessage = "This is a required field.";
        private const string errorMessage = "First Name is not valid!";

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            driver = GetDriver();

            //pre_condition
            CreateNewAccountTest.NavigateToCreateNewAccountPage(driver, createAnAccountHeaderLinkText);
            createNewAccountPage = PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);
        }

        [Test, Description("Verify that first_name can not be blank")]
        public void TC_Create_New_Account_01_First_Name_Can_Not_Be_Blank()
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, firstNameFieldId);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + requireValidationErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(driver, firstNameFieldId, requireValidationErrorMessage), Is.True);
        }

        [
            Test,
            TestCaseSource(typeof(CreateNewAccountParameters), nameof(CreateNewAccountParameters.InvalidFirstName)),
            Description("Verify that first_name can not contain special chars")
        ]
        public void TC_Create_New_Account_01_First_Name_Can_Not_Contain_Special_Chars(
            string firstName,
            string lastName,
            string email,
            string password,
            string passwordConfirm
        )
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Enter invalid first name contains special chars = " + firstName);
            createNewAccountPage.EnterToDynamicTextboxById(driver, firstName, firstNameFieldId);

            ReportLog.Info(testMethod + " - Step 02: Enter last name = " + lastName);
            createNewAccountPage.EnterToDynamicTextboxById(driver, lastName, lastNameFieldId);

            ReportLog.Info(testMethod + " - Step 03: Enter email = " + email);
            createNewAccountPage.EnterToDynamicTextboxById(driver, email, emailFieldId);

            ReportLog.Info(testMethod + " - Step 04: Enter password = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, passwordFieldId);

            ReportLog.Info(testMethod + " - Step 05: Enter password_confirm = " + passwordConfirm);
            createNewAccountPage.EnterToDynamicTextboxById(driver, passwordConfirm, passwordConfirmFieldId);

            ReportLog.Info(testMethod + " - Step 06: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, firstNameFieldId);

            ReportLog.Info(testMethod + " - Step 07: Verify that error message = '" + errorMessage + "' is displayed");
            Assert.That(createNewAccountPage.IsErrorMessageDisplayed(driver, errorMessage), Is.True);
        }
    }
}
