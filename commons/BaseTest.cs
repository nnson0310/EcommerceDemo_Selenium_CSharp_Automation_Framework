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

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            driverHelper = new();
            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [SetUp]
        public void Setup()
        {
            ExtentTestManager.CreateChildTest(TestContext.CurrentContext.Test.Name);
            driver = driverHelper!.GetDriver();
            driver!.Url = url;
        }

        [TearDown]
        public void TearDown()
        {
            driverHelper!.GenerateReportAndCloseBrowser();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            ExtentReportHelper.GetExtentReports().Flush();
        }

    }
}
