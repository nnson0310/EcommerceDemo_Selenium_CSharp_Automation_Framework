using NUnit.Framework;
using System.Configuration;
using System.Reflection;

namespace EcommerceDemo.helpers
{
    sealed public class MethodHelper
    {
        public static void SleepInSeconds(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public static string GetProjectRootDir()
        {
            return Directory.GetCurrentDirectory().Split("bin")[0];
        }

        public static string? GetProjectName()
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }

        public static string? GetEnvironmentParams(string paramName)
        {
            return ConfigurationManager.AppSettings[paramName];
        }

        public static IEnumerable<string> GetDriverOptionArguments()
        {
            yield return "--disable-gpu";
            yield return "--disable-notifications";
            yield return "--no-sandbox";
            yield return "--disable-extensions";
            yield return "--dns-prefetch-disable";
            yield return "--ignore-certificate-errors";
            yield return "--disable-infobars";
            yield return "--disable-popup-blocking";
            yield return "--disable-sync";
            yield return "--disable-translate";
            yield return "--enable-automation";
        }

        public static string GetBrowserDriverDir()
        {
            return GetProjectRootDir() + "browser_drivers";
        }

        public static string CleanString(string str)
        {
            return str.Trim().Replace("\r\n", "");
        }

        public static string? GetTestMethodName()
        {
            return TestContext.CurrentContext.Test.MethodName;
        }
    }
}
