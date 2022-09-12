
using EcommerceDemo.commons;
using EcommerceDemo.extents;
using EcommerceDemo.page_objects;
using EcommerceDemo.testcases.smokeTest_pre_condition;
using EcommerceDemo.testdata;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection;

namespace EcommerceDemo.testcases.smokeTest_00_create_new_account
{
    [TestFixture]
    public class VerifyEmail : BaseTest
    {
        private IWebDriver driver;
        private CreateNewAccountPage createNewAccountPage;

        private const string emailFieldId = "email_address";
        private const string createAnAccountHeaderLinkText = "Create an Account";
        private const string createAnAccountButtonTitle = "Create an Account";
        private const string requireValidationErrorMessage = "This is a required field.";
        private const string invalidEmailValidationErrorMessage = "Please enter a valid email address (Ex: johndoe@domain.com).";

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            driver = GetDriver();

            //pre_condition
            CreateNewAccountTest.NavigateToCreateNewAccountPage(driver, createAnAccountHeaderLinkText);
            createNewAccountPage = PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);
        }

        [Test]
        public void TC_Create_New_Account_03_Email_Can_Not_Be_Blank()
        {
            string testMethod = MethodBase.GetCurrentMethod()!.Name;

            ReportLog.Info(testMethod + " - Step 01: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, emailFieldId);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + requireValidationErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(driver, emailFieldId, requireValidationErrorMessage), Is.True);
        }

        [TestCaseSource(typeof(CreateNewAccountParameters), nameof(CreateNewAccountParameters.InvalidEmail))]
        public void TC_Create_New_Account_04_Email_Must_Be_Valid_Format(string email)
        {
            string testMethod = MethodBase.GetCurrentMethod()!.Name;

            ReportLog.Info(testMethod + " - Step 01: Enter invalid first name contains special chars = " + email);
            createNewAccountPage.EnterToDynamicTextboxById(driver, email, emailFieldId);

            ReportLog.Info(testMethod + " - Step 01: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, emailFieldId);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + invalidEmailValidationErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(driver, emailFieldId, invalidEmailValidationErrorMessage), Is.True);
        }
    }
}
