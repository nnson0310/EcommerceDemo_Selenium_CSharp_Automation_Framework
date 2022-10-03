
using EcommerceDemo.commons;
using EcommerceDemo.element_indentifiers;
using EcommerceDemo.extents;
using EcommerceDemo.helpers;
using EcommerceDemo.page_objects;
using EcommerceDemo.testcases.smokeTest_pre_condition;
using EcommerceDemo.testdata;
using NUnit.Framework;
using OpenQA.Selenium;

namespace EcommerceDemo.testcases.smokeTest_00_create_new_account
{
    [TestFixture, Description("Verify password field of Create_New_Account form")]
    public class VerifyPassword : BaseTest
    {
        private IWebDriver driver;
        private CreateNewAccountPage createNewAccountPage;

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            driver = GetDriver();

            //pre_condition
            CreateNewAccountTest.NavigateToCreateNewAccountPage(driver, ICommonUI.CreateAccountHeaderLinkText);
            createNewAccountPage = PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);
        }

        [Test, Description("Verify that password can not be blank")]
        public void TC_Create_New_Account_05_Password_Can_Not_Be_Blank()
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + ICreateNewAccountUI.RequireErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(
                driver,
                ICreateNewAccountUI.PasswordTextboxId,
                ICreateNewAccountUI.RequireErrorMessage),
                Is.True);
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
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, ICreateNewAccountUI.PasswordTextboxId);

            ReportLog.Info(testMethod + " - Step 02: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 03: Verify that '" + ICreateNewAccountUI.MinLengthErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(
                driver,
                ICreateNewAccountUI.PasswordTextboxId,
                ICreateNewAccountUI.MinLengthErrorMessage),
                Is.True);
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
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, ICreateNewAccountUI.PasswordTextboxId);

            ReportLog.Info(testMethod + " - Step 02: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 03: Verify that '" + ICreateNewAccountUI.ClassCharErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(
                driver,
                ICreateNewAccountUI.PasswordTextboxId,
                ICreateNewAccountUI.ClassCharErrorMessage),
                Is.True);
        }
    }
}
