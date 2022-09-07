using AventStack.ExtentReports;
using System.Runtime.CompilerServices;

namespace EcommerceDemo.extents
{
    public class ExtentTestManager
    {
        [ThreadStatic]
        private static ExtentTest? parentTest;

        [ThreadStatic]
        private static ExtentTest? childTest;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, string? testDesc = null)
        {
            parentTest = ExtentReportHelper.GetExtentReports().CreateTest(testName, testDesc);
            return parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateChildTest(string testName, string? testDesc = null)
        {
            childTest = parentTest!.CreateNode(testName, testDesc);
            return childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return childTest!;
        }
    }
}
