
using EcommerceDemo.commons;
using EcommerceDemo.extents;
using EcommerceDemo.page_objects;
using OpenQA.Selenium;

namespace EcommerceDemo.testcases.smokeTest_pre_condition
{
    public class CreateNewAccountTest : BaseTest
    {
        public static void NavigateToCreateNewAccountPage<T>(IWebDriver driver, string linkText, T page) where T : CommonPage
        {
            ReportLog.Info("Pre_condition: In HomePage, navigate to 'Create New Account' page by clicking 'Create An Account' header link");
            page.ClickToCreateAnAccountLink(driver, linkText);
        }
    }
}
