using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using EcommerceDemo.helpers;
using AventStack.ExtentReports.Reporter.Configuration;

namespace EcommerceDemo.extents
{
    public class ExtentReportHelper
    {
        private static ExtentReports? extentReports;

        public static ExtentReports GetExtentReports()
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

                // attach environment information to report
                extentReports.AddSystemInfo("browser", "firefox");
                extentReports.AddSystemInfo("browser_version", "latest");
                extentReports.AddSystemInfo("environment", "local");
                extentReports.AddSystemInfo("platform", "Windows");
                extentReports.AddSystemInfo("platform version", "10");

                // attach specific reporter
                extentReports.AttachReporter(reporter);
            }
            return extentReports;
        }
    }
}
