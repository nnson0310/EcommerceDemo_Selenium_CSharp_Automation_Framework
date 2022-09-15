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
    [TestFixture, Description("Verify that first_name field of Create_New_Account page")]
    public class VerifyFirstName : BaseTest
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

        [Test, Description("Verify that first_name can not be blank")]
        public void TC_Create_New_Account_01_First_Name_Can_Not_Be_Blank()
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 02: Verify that '" + ICreateNewAccountUI.RequireErrorMessage + "' error message is displayed");
            Assert.That(createNewAccountPage.IsValidationErrorMessageDisplayed(
                driver,
                ICreateNewAccountUI.FirstNameTextboxId,
                ICreateNewAccountUI.RequireErrorMessage),
                Is.True);
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
            string password
        )
        {
            string? testMethod = MethodHelper.GetTestMethodName();

            ReportLog.Info(testMethod + " - Step 01: Enter invalid first name contains special chars = " + firstName);
            createNewAccountPage.EnterToDynamicTextboxById(driver, firstName, ICreateNewAccountUI.FirstNameTextboxId);

            ReportLog.Info(testMethod + " - Step 02: Enter last name = " + lastName);
            createNewAccountPage.EnterToDynamicTextboxById(driver, lastName, ICreateNewAccountUI.LastNameTextboxId);

            ReportLog.Info(testMethod + " - Step 03: Enter email = " + email);
            createNewAccountPage.EnterToDynamicTextboxById(driver, email, ICreateNewAccountUI.EmailTextboxId);

            ReportLog.Info(testMethod + " - Step 04: Enter password = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, ICreateNewAccountUI.PasswordTextboxId);

            ReportLog.Info(testMethod + " - Step 05: Enter password_confirm = " + password);
            createNewAccountPage.EnterToDynamicTextboxById(driver, password, ICreateNewAccountUI.ConfirmPasswordTextboxId);

            ReportLog.Info(testMethod + " - Step 06: Click to '" + ICreateNewAccountUI.CreateAccountButtonName + "' button");
            createNewAccountPage.ClickToCreateAccountButton(driver, ICreateNewAccountUI.CreateAccountButtonName);

            ReportLog.Info(testMethod + " - Step 07: Verify that error message = '" + ICreateNewAccountUI.InvalidFirstNameErrorMessage + "' is displayed");
            Assert.That(createNewAccountPage.IsErrorMessageDisplayed(driver, ICreateNewAccountUI.InvalidEmailErrorMessage), Is.True);
        }
    }
}
