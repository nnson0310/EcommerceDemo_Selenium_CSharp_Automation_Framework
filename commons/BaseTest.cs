using EcommerceDemo.env_factory;
using OpenQA.Selenium;
using System.Diagnostics;

namespace EcommerceDemo.commons
{
    internal class BaseTest
    {
        private static IWebDriver? driver;

        protected IWebDriver GetBrowserDriver(
            string url,
            string environmentName,
            string browserName,
            string browserVersion,
            string ipAddress,
            string port,
            string os,
            string osVersion
    )
        {
            browserName ??= "firefox";
            driver = environmentName.ToLower() switch
            {
                "local" => new LocalEnvFactory(browserName).InitBrowserDriver(),
                "grid" => new GridEnvFactory(browserName, ipAddress, port).InitBrowserDriver(),
                _ => new CloudEnvFactory(browserName, browserVersion, os, osVersion).InitBrowserDriver(),
            };

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(GlobalConstants.longTimeout);
            driver.Manage().Window.Maximize();
            driver.Url = url;

            return driver;
        }

        public static IWebDriver? GetWebDriver()
        {
            return driver;
        }

        protected void CloseBrowserAndKillProcess()
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
