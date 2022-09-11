
using EcommerceDemo.commons;
using EcommerceDemo.extents;
using EcommerceDemo.page_objects;
using OpenQA.Selenium;

namespace EcommerceDemo.testcases.smokeTest_pre_condition
{
    public class CreateNewAccountTest: BaseTest
    {
        public static void NavigateToCreateNewAccountPage(IWebDriver driver, string linkText)
        {
            HomePage homePage = PageInitManager.GetPageInitManager().GetHomePage(driver);

            ReportLog.Info("Pre_condition: In HomePage, navigate to 'Create New Account' page by clicking 'Create An Account' header link");
            homePage.ClickToCreateAnAccountLink(driver, linkText);
        }
    }
}
