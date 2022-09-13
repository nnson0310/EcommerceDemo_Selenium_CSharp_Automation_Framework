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
    [TestFixture, Description("Verify password field of Create_New_Account page")]
    public class VerifyConfirmPassword : BaseTest
    {
        private IWebDriver driver;
        private CreateNewAccountPage createNewAccountPage;

        private const string confirmPasswordFieldId = "password-confirmation";
        private const string createAnAccountHeaderLinkText = "Create an Account";
        private const string createAnAccountButtonTitle = "Create an Account";
        private const string requireValidationErrorMessage = "This is a required field.";
        private const string matchValidationErrorMessage = "Please enter the same value again.";

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            driver = GetDriver();

            //pre_condition
            CreateNewAccountTest.NavigateToCreateNewAccountPage(driver, createAnAccountHeaderLinkText);
            createNewAccountPage = PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);
        }

        [Test, Description("Verify that confirm_password can not be blank")]
        public void TC_Create_New_Account_08_Confirm_Password_Can_Not_Be_Blank()
        {
            string? testMethod = MethodHelper.GetTestMethodName();


            ReportLog.Info(testMethod + " - Step 01: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, confirmPasswordFieldId);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + requireValidationErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(driver, confirmPasswordFieldId, requireValidationErrorMessage), Is.True);
        }

        [
            Test,
            Description("Verify that confi"),
            TestCaseSource(typeof(CreateNewAccountParameters), nameof(CreateNewAccountParameters.InvalidConfirmPassword))
        ]
        public void TC_Create_New_Account_09_Confirm_Password_Must_Match_Password(string confirmPassword)
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Enter confirm_password = " + confirmPassword);
            createNewAccountPage.EnterToDynamicTextboxById(driver, confirmPassword, confirmPasswordFieldId);

            ReportLog.Info(testMethod + " - Step 02: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, confirmPasswordFieldId);

            ReportLog.Info(testMethod + " - Step 03: Verify that '" + matchValidationErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(driver, confirmPasswordFieldId, matchValidationErrorMessage), Is.True);
        }
    }
}
