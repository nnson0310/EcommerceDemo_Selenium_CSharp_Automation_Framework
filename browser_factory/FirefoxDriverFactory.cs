﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace EcommerceDemo.browser_factory
{
    internal class FirefoxDriverFactory: IBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            FirefoxOptions firefoxOptions = new()
            {
                AcceptInsecureCertificates = true,
                // disable all browser logs except fatal errors
                LogLevel = FirefoxDriverLogLevel.Fatal
            };
            
            return new FirefoxDriver(firefoxOptions);
        }
    }
}
