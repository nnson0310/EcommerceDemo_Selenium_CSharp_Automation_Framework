using EcommerceDemo.env_factory;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using EcommerceDemo.helpers;
using static EcommerceDemo.enums.EnumCollect;
using EcommerceDemo.utils;
using EcommerceDemo.extents;

namespace EcommerceDemo.commons
{
    internal class BaseTest
    {
        private IWebDriver? driver;
        private DriverHelper? driverHelper;
        private readonly string? url = MethodHelper.GetEnvironmentParams("url");

        [OneTimeSetUp]
        public void GlobalSetup()
        {
           driverHelper = new();
           driver = driverHelper.GetDriver();
           ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [SetUp]
        public void Setup()
        {
            driver!.Url = url;
            ExtentTestManager.CreateChildTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            driverHelper!.CloseBrowserAndKillProcess();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            ExtentReportHelper.GetExtentReports().Flush();
        }
       
    }
}
