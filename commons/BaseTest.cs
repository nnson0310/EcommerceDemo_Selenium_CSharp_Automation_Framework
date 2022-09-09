using NUnit.Framework;
using OpenQA.Selenium;
using EcommerceDemo.helpers;
using EcommerceDemo.utils;
using EcommerceDemo.extents;
using System.Diagnostics;

namespace EcommerceDemo.commons
{
    public class BaseTest
    {
        private IWebDriver? driver;
        private DriverHelper? driverHelper;
        private readonly string? url = MethodHelper.GetEnvironmentParams("url");

        protected IWebDriver? GetDriver()
        {
            return driver;
        }

        [OneTimeSetUp]
        protected void GlobalSetup()
        {
            driverHelper = new();
            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [SetUp]
        protected void Setup()
        {
            ExtentTestManager.CreateChildTest(TestContext.CurrentContext.Test.Name);
            driver = driverHelper!.GetDriver();
            driver!.Url = url;
        }

        [TearDown]
        protected void TearDown()
        {
            driverHelper!.GenerateReportAndCloseBrowser();
        }

        [OneTimeTearDown]
        protected void GlobalTearDown()
        {
            ExtentReportHelper.GetExtentReports().Flush();
        }

    }
}
