using System.Configuration;
using System.Reflection;

namespace EcommerceDemo.helpers
{
    sealed internal class MethodHelper
    {
        public static void SleepInSeconds(int seconds)
        {
            Thread.Sleep(seconds);
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
    }
}
