
using EcommerceDemo.commons;
using NUnit.Framework;

namespace EcommerceDemo.testcases
{
    public class SubscribeEmail : BaseTest
    {
        [TestCase(1, 2, 3)]
        public void TC_01(int a, int b, int c)
        {
            Assert.That(c, Is.EqualTo(a + b));
        }
    }
}
