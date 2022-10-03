using EcommerceDemo.commons;
using EcommerceDemo.element_indentifiers;
using EcommerceDemo.extents;
using EcommerceDemo.helpers;
using EcommerceDemo.page_interfaces;
using EcommerceDemo.page_objects;
using EcommerceDemo.testcases.smokeTest_pre_condition;
using EcommerceDemo.testdata;
using NUnit.Framework;
using OpenQA.Selenium;

namespace EcommerceDemo.testcases.smokeTest_00_create_new_account
{
    [
      TestFixture,
      FixtureLifeCycle(LifeCycle.SingleInstance),
      Parallelizable(ParallelScope.Fixtures),
      Description("Verify password field of Create_New_Account page")
    ]
    public class VerifyConfirmPassword : BaseTest
    {
        private IWebDriver driver;
        private HomePage homePage;
        private CreateNewAccountPage createNewAccountPage;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            driver = GetDriver();

            homePage = PageInitManager.GetPageInitManager().GetHomePage(driver);
            //pre_condition
            CreateNewAccountTest.NavigateToCreateNewAccountPage(driver, ICommonUI.CreateAccountHeaderLinkText, homePage);
            createNewAccountPage = PageInitManager.GetPageInitManager().GetCreateNewAccountPage(driver);
        }

        [Test, Description("Verify that confirm_password can not be blank")]
        public void TC_Create_New_Account_08_Confirm_Password_Can_Not_Be_Blank()
        {
            string? testMethod = MethodHelper.GetTestMethodName();


            ReportLog.Info(testMethod + " - Step 01: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + ICreateNewAccountUI.RequireErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(
                driver,
                ICreateNewAccountUI.ConfirmPasswordTextboxId,
                ICreateNewAccountUI.RequireErrorMessage),
                Is.True);
        }

        [
            Test,
            Description("Verify that confirm_password must be same as password"),
            TestCaseSource(typeof(CreateNewAccountParameters), nameof(CreateNewAccountParameters.InvalidConfirmPassword))
        ]
        public void TC_Create_New_Account_09_Confirm_Password_Must_Match_Password(string confirmPassword)
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Enter confirm_password = " + confirmPassword);
            createNewAccountPage.EnterToDynamicTextboxById(driver, confirmPassword, ICreateNewAccountUI.ConfirmPasswordTextboxId);

            ReportLog.Info(testMethod + " - Step 02: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 03: Verify that '" + ICreateNewAccountUI.MatchPasswordErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(
                driver,
                ICreateNewAccountUI.ConfirmPasswordTextboxId,
                ICreateNewAccountUI.MatchPasswordErrorMessage),
                Is.True);
        }
    }
}
