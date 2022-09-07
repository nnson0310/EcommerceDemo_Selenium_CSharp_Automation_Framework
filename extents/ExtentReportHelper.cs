using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using EcommerceDemo.helpers;
using AventStack.ExtentReports.Reporter.Configuration;

namespace EcommerceDemo.extents
{
    public class ExtentReportHelper
    {
        public static ExtentReports? extentReports;

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
                extentReports.AttachReporter(reporter);
            }
            return extentReports;
        }
    }
}
