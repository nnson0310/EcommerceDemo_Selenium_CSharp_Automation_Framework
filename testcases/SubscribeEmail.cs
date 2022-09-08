
using EcommerceDemo.commons;
using EcommerceDemo.helpers;
using NUnit.Framework;

namespace EcommerceDemo.testcases
{
    public class SubscribeEmail : BaseTest
    {
        [TestCase(1, 2, 3)]
        public void TC_01(int a, int b, int c)
        {
            string path = Path.Combine(MethodHelper.GetProjectRootDir() + "browser_drivers", "chromedriver.exe");
            Console.WriteLine(path);
            Assert.That(c, Is.EqualTo(a + b));
        }
    }
}
