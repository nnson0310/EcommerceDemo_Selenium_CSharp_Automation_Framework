﻿
using AventStack.ExtentReports;
using EcommerceDemo.commons;
using EcommerceDemo.env_factory;
using EcommerceDemo.extents;
using EcommerceDemo.helpers;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EcommerceDemo.utils
{
    public class DriverHelper
    {
        private IWebDriver? driver;

        public static string? environmentName = MethodHelper.GetEnvironmentParams("env");
        public static string? browser = MethodHelper.GetEnvironmentParams("browser");
        public static string? browserVersion = MethodHelper.GetEnvironmentParams("browser_version");
        public static string? ipAddress = MethodHelper.GetEnvironmentParams("ip_address");
        public static string? port = MethodHelper.GetEnvironmentParams("port");
        public static string? os = MethodHelper.GetEnvironmentParams("os");
        public static string? osVersion = MethodHelper.GetEnvironmentParams("os_version");

        public IWebDriver GetDriver()
        {
            try
            {
                driver = InitDriver();
                ReportLog.Pass("Init browser driver = " + driver.ToString() + " successfully.");
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
            if (environmentName == "cloud")
            {
                cap = ((RemoteWebDriver)driver).Capabilities;
            }
            browser = cap.GetCapability("browserName").ToString();
            browserVersion = cap.GetCapability("browserVersion").ToString();

            return driver;
        }

        private IWebDriver InitDriver()
        {
            // assign default value if null
            browser ??= "firefox";
            browserVersion ??= "latest";
            environmentName ??= "local";
            ipAddress ??= "localhost";
            port ??= "4444";
            os ??= "Windows";
            osVersion ??= "10";

            driver = environmentName.ToLower() switch
            {
                "local" => new LocalEnvFactory(browser).InitDriver(),
                "grid" => new GridEnvFactory(browser, ipAddress, port).InitDriver(),
                _ => new CloudEnvFactory(browser, browserVersion, os, osVersion).InitDriver(),
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

        /// <summary>
        /// Close browser and kill all process though cmd
        /// Check os is "Windows" or "Mac" to run corresponding cli
        /// </summary>
        public void CloseBrowserAndKillProcess()
        {
            string cmd = "";
            try
            {
                string osName = Environment.OSVersion.ToString().ToLower();
                string driverInstanceName = driver!.ToString()!.ToLower();

                if (driverInstanceName.Contains("chrome"))
                {
                    if (osName.Contains("window"))
                    {
                        cmd = "taskkill /F /FI \"IMAGENAME eq chromedriver*\"";
                    }
                    else
                    {
                        cmd = "pkill chromedriver";
                    }
                }
                else if (driverInstanceName.Contains("firefox"))
                {
                    if (osName.Contains("windows"))
                    {
                        cmd = "taskkill /F /FI \"IMAGENAME eq geckodriver*\"";
                    }
                    else
                    {
                        cmd = "pkill geckodriver";
                    }
                }
                else if (driverInstanceName.Contains("edge"))
                {
                    if (osName.Contains("window"))
                    {
                        cmd = "taskkill /F /FI \"IMAGENAME eq msedgedriver*\"";
                    }
                    else
                    {
                        cmd = "pkill msedgedriver";
                    }
                }
                else if (driverInstanceName.Contains("safari"))
                {
                    if (osName.Contains("mac"))
                    {
                        cmd = "pkill safaridriver";
                    }
                }

                if (driver is not null)
                {
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Quit();
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
