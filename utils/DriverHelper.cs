using AventStack.ExtentReports;
using EcommerceDemo.commons;
using EcommerceDemo.env_factory;
using EcommerceDemo.extents;
using EcommerceDemo.helpers;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System.Diagnostics;

namespace EcommerceDemo.utils
{
    public class DriverHelper
    {
        private IWebDriver? driver;

        public static string? Url = MethodHelper.GetEnvironmentParams("url");
        public static string? EnvironmentName = MethodHelper.GetEnvironmentParams("env");
        public static string? Browser = MethodHelper.GetEnvironmentParams("browser");
        public static string? BrowserVersion = MethodHelper.GetEnvironmentParams("browser_version");
        public static string? IpAddress = MethodHelper.GetEnvironmentParams("ip_address");
        public static string? Port = MethodHelper.GetEnvironmentParams("port");
        public static string? Os = MethodHelper.GetEnvironmentParams("os");
        public static string? OsVersion = MethodHelper.GetEnvironmentParams("os_version");
        public static string? Platform;

        public IWebDriver GetDriver()
        {
            try
            {
                driver = InitDriver();
                ReportLog.Pass("Init browser Driver = " + driver.ToString() + " successfully.");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                ReportLog.Fail("Failed to init browser driver.");
            }

            if (driver is null)
            {
                throw new ArgumentNullException("Web driver can not be intialized.");
            }

            // get browser info to attach to extent reports
            ICapabilities cap = ((WebDriver)driver).Capabilities;
            Browser = cap.GetCapability("browserName").ToString();
            BrowserVersion = cap.GetCapability("browserVersion").ToString();
            Platform = Environment.OSVersion.VersionString;

            return driver;
        }

        private IWebDriver InitDriver()
        {
            // assign default value if null
            Browser ??= "msedge";
            EnvironmentName ??= "local";
            IpAddress ??= "localhost";
            Port ??= "4444";

            driver = EnvironmentName.ToLower() switch
            {
                "local" => new LocalEnvFactory(Browser).InitDriver(),
                _ => new GridEnvFactory(Browser, IpAddress, Port).InitDriver(),
            };

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(GlobalConstants.longTimeout);
            return driver;
        }

        private MediaEntityModelProvider CaptureScreenshot(string testName)
        {
            var screenshot = ((ITakesScreenshot)driver!).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, testName).Build();
        }

        public void GenerateExtentReport()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var errorMessage = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                    ? ""
                    : string.Format("<pre>{0}<pre>", TestContext.CurrentContext.Result.Message);
                var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}<pre>", TestContext.CurrentContext.Result.StackTrace);

                switch (status)
                {
                    case TestStatus.Skipped:
                        ReportLog.Skip("Test skipped");
                        break;
                    case TestStatus.Passed:
                        ReportLog.Pass("Test passed");
                        break;
                    case TestStatus.Failed:
                        ReportLog.Fail("Test failed");
                        ReportLog.Fail(errorMessage);
                        ReportLog.Fail(stackTrace);
                        ReportLog.Fail("Attached screenshot", CaptureScreenshot(TestContext.CurrentContext.Test.Name));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void CloseBrowser()
        {
            if (driver is not null)
            {
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Quit();
            }
        }

        public void KillAllDriverProcess()
        {
            string cmd = "";
            try
            {
                string driverInstanceName = driver!.ToString()!.ToLower();

                if (driverInstanceName.Contains("chrome"))
                {

                    cmd = "taskkill /F /FI \"IMAGENAME eq chromedriver*\"";
                }
                else if (driverInstanceName.Contains("firefox"))
                {

                    cmd = "taskkill /F /FI \"IMAGENAME eq geckodriver*\"";
                }
                else
                {
                    cmd = "taskkill /F /FI \"IMAGENAME eq msedgedriver*\"";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            finally
            {
                Process process = new();

                try
                {
                    ProcessStartInfo processStartInfo = new("CMD.exe", "/C " + cmd);
                    process.StartInfo = processStartInfo;
                    process.Start();
                    process.WaitForExit();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
                finally
                {
                    process.Close();
                }
            }
        }
    }
}
