using NUnit.Framework;
using OpenQA.Selenium;
using EcommerceDemo.utils;
using EcommerceDemo.extents;
using EcommerceDemo.helpers;

namespace EcommerceDemo.commons
{
    public class BaseTest
    {
        private IWebDriver driver;
        private DriverHelper driverHelper;
        private readonly string? url = MethodHelper.GetEnvironmentParams("url");

        [OneTimeSetUp]
        protected void GlobalSetup()
        {
            ExtentTestManager.CreateParentTest(GetType().Name);
            driverHelper = new();
        }

        [SetUp]
        protected virtual void SetUp()
        {
            ExtentTestManager.CreateChildTest(TestContext.CurrentContext.Test.Name);
            driver = driverHelper.GetDriver();
            driver.Url = url;
        }

        [TearDown]
        protected void TearDown()
        {
            driverHelper.GenerateExtentReport();
            driverHelper.CloseBrowser();
        }

        [OneTimeTearDown]
        protected void GlobalTearDown()
        {
            ExtentReportHelper.GetExtentReports().Flush();
        }

        public IWebDriver GetDriver()
        {
            return driver;
        }
    }
}

