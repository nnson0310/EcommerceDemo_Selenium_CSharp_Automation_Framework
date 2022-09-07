﻿
using EcommerceDemo.commons;
using EcommerceDemo.env_factory;
using EcommerceDemo.extents;
using EcommerceDemo.helpers;
using OpenQA.Selenium;
using System.Diagnostics;

namespace EcommerceDemo.utils
{
    public class DriverHelper
    {
        private IWebDriver? driver;

        private string? environmentName = MethodHelper.GetEnvironmentParams("env");
        private string? browser = MethodHelper.GetEnvironmentParams("browser");
        private string? browserVersion = MethodHelper.GetEnvironmentParams("browser_version");
        private string? ipAddress = MethodHelper.GetEnvironmentParams("ip_address");
        private string? port = MethodHelper.GetEnvironmentParams("port");
        private string? os = MethodHelper.GetEnvironmentParams("os");
        private string? osVersion = MethodHelper.GetEnvironmentParams("os_version");

        public IWebDriver? GetDriver()
        {
            try
            {
                driver = InitDriver();
                ReportLog.Pass("Init browser driver successfully.");
            } catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                ReportLog.Fail("Fail to init browser driver.");
            }
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

                if (driver != null)
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
