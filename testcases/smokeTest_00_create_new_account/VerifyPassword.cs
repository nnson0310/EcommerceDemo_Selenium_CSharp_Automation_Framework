
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
    [TestFixture, Description("Verify password field of Create_New_Account form")]
    public class VerifyPassword : BaseTest
    {
        private IWebDriver driver;
        private CreateNewAccountPage createNewAccountPage;

        private const string passwordFieldId = "password";
        private const string createAnAccountHeaderLinkText = "Create an Account";
        private const string createAnAccountButtonTitle = "Create an Account";
        private const string requireValidationErrorMessage = "This is a required field.";
        private const string invalidLengthValidationErrorMessage = "Minimum length of this field must be equal or greater than 8 symbols. " +
            "Leading and trailing spaces will be ignored.";
        private const string minimumClassCharValidationErrorMessage = "Minimum of different classes of characters in password is 3. " +
            "Classes of characters: Lower Case, Upper Case, Digits, Special Characters.";

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            driver = GetDriver();

            //pre_condition
            CreateNewAccountTest.NavigateToCreateNewAccountPage(driver, createAnAccountHeaderLinkText);
            createNewAccountPage = PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);
        }

        [Test, Description("Verify that password can not be blank")]
        public void TC_Create_New_Account_05_Password_Can_Not_Be_Blank()
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, passwordFieldId);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + requireValidationErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(driver, passwordFieldId, requireValidationErrorMessage), Is.True);
        }

        [
            Test,
            TestCaseSource(typeof(CreateNewAccountParameters), nameof(CreateNewAccountParameters.InvalidLengthPassword)),
            Description("Verify that password must be at least 8 chars length")
        ]
        public void TC_Create_New_Account_06_Password_Must_Be_At_Least_8_Chars_Length(string password)
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01 Enter password = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, passwordFieldId);

            ReportLog.Info(testMethod + " - Step 02: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, passwordFieldId);

            ReportLog.Info(testMethod + " - Step 03: Verify that '" + invalidLengthValidationErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(driver, passwordFieldId, invalidLengthValidationErrorMessage), Is.True);
        }


        [
            Test,
            TestCaseSource(typeof(CreateNewAccountParameters), nameof(CreateNewAccountParameters.ThreeClassesCharPassword)),
            Description("Verify that password must contain at least 3 different classes of chars")
        ]
        public void TC_Create_New_Account_07_Password_Must_Be_At_Least_3_Different_Char_Classes(string password)
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01 Enter password = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, passwordFieldId);

            ReportLog.Info(testMethod + " - Step 02: Click to '" + createAnAccountButtonTitle + "' button");
            createNewAccountPage.ClickToCreateAnAccountButton(driver, createAnAccountButtonTitle, passwordFieldId);

            ReportLog.Info(testMethod + " - Step 03: Verify that '" + minimumClassCharValidationErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(driver, passwordFieldId, minimumClassCharValidationErrorMessage), Is.True);
        }
    }
}
