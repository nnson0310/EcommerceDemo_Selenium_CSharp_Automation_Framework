
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
    [
         TestFixture,
         FixtureLifeCycle(LifeCycle.SingleInstance),
         Parallelizable(ParallelScope.Fixtures),
         Description("Verify email field of Create_New_Account form")
    ]
    public class VerifyEmail : BaseTest
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

        [Test, Description("Verify that email can not be blank")]
        public void TC_Create_New_Account_03_Email_Can_Not_Be_Blank()
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + ICreateNewAccountUI.RequireErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(
                driver,
                ICreateNewAccountUI.EmailTextboxId,
                ICreateNewAccountUI.RequireErrorMessage),
                Is.True);
        }

        [
            Test,
            TestCaseSource(typeof(CreateNewAccountParameters), nameof(CreateNewAccountParameters.InvalidEmail)),
            Description("Verify that email must be valid format")
        ]
        public void TC_Create_New_Account_04_Email_Must_Be_Valid_Format(string email)
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Enter invalid first name contains special chars = " + email);
            createNewAccountPage.EnterToDynamicTextboxById(driver, email, ICreateNewAccountUI.EmailTextboxId);

            ReportLog.Info(testMethod + " - Step 01: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + ICreateNewAccountUI.InvalidEmailErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(
                driver,
                ICreateNewAccountUI.EmailTextboxId,
                ICreateNewAccountUI.InvalidEmailErrorMessage),
                Is.False);
        }
    }
}
