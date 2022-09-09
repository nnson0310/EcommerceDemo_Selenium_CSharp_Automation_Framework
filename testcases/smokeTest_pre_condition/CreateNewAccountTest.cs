using AventStack.ExtentReports;
using EcommerceDemo.commons;
using EcommerceDemo.extents;
using EcommerceDemo.page_objects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace EcommerceDemo.testcases.smokeTest_pre_condition
{
    public class CreateNewAccountTest : BaseTest
    {
        HomePage? homePage;
        IWebDriver? driver;
        ExtentTest? testLog = ExtentTestManager.GetTest();

        [SetUp]
        protected new void Setup()
        {
            base.Setup();
            driver = base.GetDriver();
            homePage = PageInitManager.GetPageInitManager().GetHomePage(driver);

            testLog.Info("Pre condition: Click to 'Create an Account' button");
            homePage.ClickToCreateAnAccountLink();
        }
    }
}
