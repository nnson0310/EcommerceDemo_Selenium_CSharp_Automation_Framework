using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using EcommerceDemo.helpers;
using AventStack.ExtentReports.Reporter.Configuration;
using EcommerceDemo.utils;

namespace EcommerceDemo.extents
{
    public class ExtentReportHelper
    {
        private static ExtentReports? extentReports;

        public static ExtentReports InitExtentReports()
        {
            if (extentReports is null)
            {
                extentReports = new ExtentReports();
                string reportDir = Path.Combine(MethodHelper.GetProjectRootDir(), "reports");
                if (!Directory.Exists(reportDir))
                {
                    Directory.CreateDirectory(reportDir);
                }

                string pathToReport = Path.Combine(reportDir, "index.html");
                var reporter = new ExtentHtmlReporter(pathToReport);
                reporter.Config.DocumentTitle = "Extent Report";
                reporter.Config.ReportName = MethodHelper.GetProjectName() + " Report";
                reporter.Config.Theme = Theme.Standard;

                // attach specific reporter
                extentReports.AttachReporter(reporter);
            }
            return extentReports;
        }

        public static ExtentReports GetExtentReports()
        {
            if (extentReports is null)
            {
                throw new ArgumentNullException("Instance of ExtentReports class can not be intialized.");
            }

            //attach runtime information to extent report
            extentReports.AddSystemInfo("browser", DriverHelper.browser);
            extentReports.AddSystemInfo("browser_version", DriverHelper.browserVersion);
            extentReports.AddSystemInfo("environment", DriverHelper.environmentName);
            extentReports.AddSystemInfo("os", DriverHelper.os);
            extentReports.AddSystemInfo("os version", DriverHelper.osVersion);

            return extentReports;
        }
    }
}
