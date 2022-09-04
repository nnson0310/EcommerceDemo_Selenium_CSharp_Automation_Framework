using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EcommerceDemo.browser_factory
{
    internal class CocCocDriverFactory : IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            ChromeOptions chromeOptions = new()
            {
                AcceptInsecureCertificates = true
            };

            // set binary path location for CocCoc browser
            // binary path = path to .exe file (it depends on where you install application)
            // because CocCoc is a 64-bit program so it will be always installed
            // in Program Files other than Program Files (x86) folder. If you install
            // it in another driver (ex: D:), specify the absolute path for it
            chromeOptions.BinaryLocation = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + 
                "\\CocCoc\\Browser\\Application\\browser.exe";

            return new ChromeDriver(chromeOptions);
        }
    }
}
