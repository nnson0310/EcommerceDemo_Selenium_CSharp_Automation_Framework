using EcommerceDemo.commons;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDemo.page_objects
{
    public class CreateNewAccountPage : BasePage
    {
        private IWebDriver? driver;

        public CreateNewAccountPage(IWebDriver? driver)
        {
            this.driver = driver;
        }
    }
}
