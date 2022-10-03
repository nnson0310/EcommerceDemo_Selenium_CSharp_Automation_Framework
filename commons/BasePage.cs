using EcommerceDemo.helpers;
using EcommerceDemo.custom_exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;

namespace EcommerceDemo.commons
{
    abstract public class BasePage
    {
        private WebDriverWait? explicitWait;

        private IJavaScriptExecutor? jsExecutor;

        private int longTimeout = GlobalConstants.longTimeout;

        private int shortTimeout = GlobalConstants.shortTimeout;

        private void OverrideGlobalTimeout(IWebDriver driver, int seconds)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        private string GetDynamicXpath(string locator, params string[] dynamicValues)
        {
            if (locator.StartsWith("Xpath") || locator.StartsWith("xpath=") || locator.StartsWith("XPATH="))
            {
                locator = string.Format(locator, (string[])dynamicValues);
            }
            return locator;
        }

        private string GetLocatorValue(string locator)
        {
            return locator.Substring(locator.IndexOf("=") + 1);
        }

        private By GetByLocator(string locator)
        {
            By? by;

            if (locator.ToLower().StartsWith("id="))
            {
                by = By.Id(GetLocatorValue(locator));
            }
            else if (locator.ToLower().StartsWith("css="))
            {
                by = By.CssSelector(GetLocatorValue(locator));
            }
            else if (locator.ToLower().StartsWith("class="))
            {
                by = By.ClassName(GetLocatorValue(locator));
            }
            else if (locator.ToLower().StartsWith("name="))
            {
                by = By.Name(GetLocatorValue(locator));
            }
            else if (locator.ToLower().StartsWith("xpath="))
            {
                by = By.XPath(GetLocatorValue(locator));
            }
            else
            {
                throw new InvalidLocatorException(locator);
            }
            return by;
        }

        protected void OpenPageUrl(IWebDriver driver, string url)
        {
            driver.Url = url;
        }

        protected string GetPageTitle(IWebDriver driver)
        {
            return driver.Title;
        }

        protected string GetPageSource(IWebDriver driver)
        {
            return driver.PageSource;
        }

        protected string GetPageUrl(IWebDriver driver)
        {
            return driver.Url;
        }

        protected void RedirectBack(IWebDriver driver)
        {
            driver.Navigate().Back();
        }

        protected void RefreshPage(IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        protected void RedirectToPage(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        protected void RedirectForward(IWebDriver driver)
        {
            driver.Navigate().Forward();
        }

        protected IAlert WaitForAlertPresent(IWebDriver driver)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));

            return explicitWait.Until(ExpectedConditions.AlertIsPresent());
        }

        protected void AcceptAlert(IWebDriver driver)
        {
            WaitForAlertPresent(driver).Accept();
        }

        protected void CancelAlert(IWebDriver driver)
        {
            WaitForAlertPresent(driver).Dismiss();
        }

        protected bool IsActiveLink(IWebDriver driver, string link)
        {
            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, link);
            var response = httpClient.Send(request).StatusCode;
            if ((int)response == 200)
            {
                return true;
            }
            return false;
        }

        protected void SendKeyToAlert(IWebDriver driver, string str)
        {
            WaitForAlertPresent(driver).SendKeys(str);
        }

        protected IWebElement WaitForPresentOfElement(IWebDriver driver, string locator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));

            return explicitWait.Until(ExpectedConditions.ElementExists(GetByLocator(locator)));
        }

        protected List<IWebElement> WaitForPresentOfAllElements(IWebDriver driver, string locator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));

            return explicitWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(GetByLocator(locator))).ToList();
        }

        protected void SwitchWindowById(IWebDriver driver, string currentWindowId)
        {
            List<string> allWindowIds = driver.WindowHandles.ToList();

            foreach (string windowId in allWindowIds)
            {
                if (!windowId.Equals(currentWindowId))
                {
                    driver.SwitchTo().Window(windowId);
                    break;
                }
            }
        }

        protected void SwitchWindowByTitle(IWebDriver driver, string pageTitle)
        {
            List<string> allWindowIds = driver.WindowHandles.ToList();

            foreach (string windowId in allWindowIds)
            {
                driver.SwitchTo().Window(windowId);
                if (driver.Title.Equals(pageTitle))
                {
                    break;
                }
            }
        }

        protected string GetCurrentWindowId(IWebDriver driver)
        {
            return driver.CurrentWindowHandle;
        }

        protected void CloseAllExceptParentWindow(IWebDriver driver, string parentWindowId)
        {
            List<string> allWindowIds = driver.WindowHandles.ToList();

            foreach (string windowId in allWindowIds)
            {
                if (!windowId.Equals(parentWindowId))
                {
                    driver.SwitchTo().Window(windowId);
                    driver.Close();
                }
                driver.SwitchTo().Window(parentWindowId);
            }
        }

        protected IWebElement GetElement(IWebDriver driver, string locator)
        {
            return driver.FindElement(GetByLocator(locator));
        }

        protected List<IWebElement> GetElements(IWebDriver driver, string locator)
        {
            return driver.FindElements(GetByLocator(locator)).ToList();
        }

        protected void ClickToElement(IWebDriver driver, string locator)
        {
            GetElement(driver, locator).Click();
        }

        protected void ClickToElement(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            GetElement(driver, GetDynamicXpath(locator, dynamicValues)).Click();
        }

        protected void ClickToElementByAction(IWebDriver driver, string locator)
        {
            Actions action = new(driver);
            action.MoveToElement(GetElement(driver, locator)).Click().Build().Perform();
        }

        protected void ClickToElementByAction(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            Actions action = new(driver);
            action.MoveToElement(GetElement(driver, GetDynamicXpath(locator, dynamicValues))).Click().Build().Perform();
        }

        protected void SendKeyToElement(IWebDriver driver, string locator, string value)
        {
            GetElement(driver, locator).Clear();
            GetElement(driver, locator).SendKeys(value);
        }

        protected void SendKeyToElement(IWebDriver driver, string locator, string value, params string[] dynamicValues)
        {
            GetElement(driver, GetDynamicXpath(locator, dynamicValues)).Clear();
            GetElement(driver, GetDynamicXpath(locator, dynamicValues)).SendKeys(value);
        }

        protected void UploadFileBySendKey(IWebDriver driver, string locator, string value)
        {
            GetElement(driver, locator).SendKeys(value);
        }

        protected void UploadFileBySendKey(IWebDriver driver, string locator, string value, params string[] dynamicValues)
        {
            GetElement(driver, GetDynamicXpath(locator, dynamicValues)).SendKeys(value);
        }

        protected void ClearInputValueByKeyboard(IWebDriver driver, string locator)
        {
            IWebElement input = GetElement(driver, locator);
            input.SendKeys(Keys.Control + "a");
            input.SendKeys(Keys.Delete);
        }

        protected void PressEnterButton(IWebDriver driver)
        {
            Actions actions = new(driver);
            actions.SendKeys(Keys.Enter).Build().Perform();
        }

        protected void PressTabButton(WebDriver driver)
        {
            Actions actions = new(driver);
            actions.SendKeys(Keys.Tab).Build().Perform();
        }

        protected void PressSpaceButton(WebDriver driver)
        {
            Actions actions = new(driver);
            actions.SendKeys(Keys.Space).Perform();
        }

        protected void SelectItemInDropDown(IWebDriver driver, string locator, string text)
        {
            SelectElement select = new(GetElement(driver, locator));
            select.SelectByText(text);
        }

        protected void SelectItemInDropDown(IWebDriver driver, string locator, string text, params string[] dynamicValues)
        {
            SelectElement select = new(GetElement(driver, GetDynamicXpath(locator, dynamicValues)));
            select.SelectByText(text);
        }

        protected IWebElement GetSelectedItemInDropDown(IWebDriver driver, string locator)
        {
            SelectElement select = new(GetElement(driver, locator));

            return select.SelectedOption;
        }

        protected bool IsDropdownMultiple(IWebDriver driver, string locator)
        {
            SelectElement select = new(GetElement(driver, locator));

            return select.IsMultiple;
        }

        protected void SelectItemInCustomDropDown(IWebDriver driver, string parentLocator, string childItemLocator, string expectedItem)
        {
            GetElement(driver, parentLocator).Click();
            MethodHelper.SleepInSeconds(1);

            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            List<IWebElement> elements = explicitWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(GetByLocator(childItemLocator))).ToList();

            foreach (IWebElement element in elements)
            {
                if (element.Text.Trim().Equals(expectedItem))
                {
                    jsExecutor = (IJavaScriptExecutor)driver;
                    jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true)", element);
                    MethodHelper.SleepInSeconds(1);

                    element.Click();
                    MethodHelper.SleepInSeconds(1);
                    break;
                }
            }
        }

        protected String GetAttributeValue(IWebDriver driver, string locator, string attributeName)
        {
            return GetElement(driver, locator).GetAttribute(attributeName).Trim();
        }

        protected String GetAttributeValue(IWebDriver driver, string locator, string attributeName, params string[] dynamicValues)
        {
            return GetElement(driver, GetDynamicXpath(locator, dynamicValues)).GetAttribute(attributeName).Trim();
        }

        protected string GetElementText(IWebDriver driver, string locator)
        {
            return GetElement(driver, locator).Text.Trim();
        }

        protected string GetElementText(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            return GetElement(driver, GetDynamicXpath(locator, dynamicValues)).Text.Trim();
        }

        protected string GetElementProperty(IWebDriver driver, string locator, string propertyName)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            return (string)jsExecutor.ExecuteScript("return arguments[0]." + propertyName, GetElement(driver, locator));
        }

        protected List<string> GetTextOfAllElements(IWebDriver driver, string locator)
        {
            List<IWebElement> elements = GetElements(driver, locator);
            var allTexts = new List<string>();
            foreach (IWebElement element in elements)
            {
                allTexts.Add(element.Text);
            }
            return allTexts;
        }

        protected string GetCssValue(WebDriver driver, string cssAttribute, string locator)
        {
            return GetElement(driver, locator).GetCssValue(cssAttribute);
        }

        protected string GetCssValue(WebDriver driver, string cssAttribute, string locator, params string[] dynamicValues)
        {
            return GetElement(driver, GetDynamicXpath(locator, dynamicValues)).GetCssValue(cssAttribute);
        }

        protected string GetHexColorFromRgbColor(string rgbColor)
        {
            return ColorHelper.RgbToHexColor(rgbColor);
        }

        protected string GetHexColorFromRgbaColor(string rgbaColor)
        {
            return ColorHelper.RgbaToHexColor(rgbaColor);
        }

        protected int GetElementSize(IWebDriver driver, string locator)
        {
            return GetElements(driver, locator).Capacity;
        }

        protected int GetElementSize(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            return GetElements(driver, GetDynamicXpath(locator, dynamicValues)).Capacity;
        }

        protected void UncheckCheckboxOrRadio(IWebDriver driver, string locator)
        {
            List<IWebElement> elements = GetElements(driver, locator);

            foreach (IWebElement element in elements)
            {
                if (element.Selected)
                {
                    element.Click();
                    break;
                }
            }
        }

        protected void UncheckCheckboxOrRadio(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            List<IWebElement> elements = GetElements(driver, GetDynamicXpath(locator, dynamicValues));

            foreach (IWebElement element in elements)
            {
                if (element.Selected)
                {
                    element.Click();
                    break;
                }
            }
        }

        protected void CheckCheckboxOrRadio(IWebDriver driver, string locator)
        {
            List<IWebElement> elements = GetElements(driver, locator);

            foreach (IWebElement element in elements)
            {
                if (!element.Selected)
                {
                    element.Click();
                    break;
                }
            }
        }

        protected void CheckCheckboxOrRadio(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            List<IWebElement> elements = GetElements(driver, GetDynamicXpath(locator, dynamicValues));

            foreach (IWebElement element in elements)
            {
                if (!element.Selected)
                {
                    element.Click();
                    break;
                }
            }
        }

        protected bool IsElementDisplayed(IWebDriver driver, string locator)
        {
            return GetElement(driver, locator).Displayed;
        }

        protected bool IsElementDisplayed(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            return GetElement(driver, GetDynamicXpath(locator, dynamicValues)).Displayed;
        }

        protected bool IsElementUndisplayed(IWebDriver driver, string locator)
        {
            OverrideGlobalTimeout(driver, shortTimeout);
            List<IWebElement> elements = GetElements(driver, locator);
            OverrideGlobalTimeout(driver, longTimeout);
            int numberOfElements = elements.Capacity;

            if (numberOfElements == 0)
            {
                return true;
            }
            else if (numberOfElements > 0 && !elements.First().Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool IsElementEnabled(IWebDriver driver, string locator)
        {
            return GetElement(driver, locator).Enabled;
        }

        protected bool IsElementEnabled(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            return GetElement(driver, GetDynamicXpath(locator, dynamicValues)).Enabled;
        }

        protected bool IsElementSelected(IWebDriver driver, string locator)
        {
            return GetElement(driver, locator).Selected;
        }

        protected bool IsElementSelected(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            return GetElement(driver, GetDynamicXpath(locator, dynamicValues)).Selected;
        }

        protected void SwitchToFrame(IWebDriver driver, string locator)
        {
            driver.SwitchTo().Frame(GetElement(driver, locator));
        }

        protected void SwitchToDefaultContent(IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        protected void HoverToElement(IWebDriver driver, string locator)
        {
            Actions actions = new(driver);
            actions.MoveToElement(GetElement(driver, locator)).Perform();
        }

        protected void HoverToElement(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            Actions actions = new(driver);
            actions.MoveToElement(GetElement(driver, GetDynamicXpath(locator, dynamicValues))).Perform();
        }

        protected string GetInnerTextOfDocument(IWebDriver driver)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            return (string)jsExecutor.ExecuteScript("return document.documentElement.innerText;");
        }

        protected string GetInnerTextOfElement(IWebDriver driver, string locator)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            return (string)jsExecutor.ExecuteScript("return arguments[0].innerText", GetElement(driver, locator));
        }

        protected void ScrollToBottomOfPage(IWebDriver driver)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("window.scrollBy(0,document.body.scrollHeight)");
        }

        protected void HighlightElement(IWebDriver driver, string locator)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            IWebElement element = GetElement(driver, locator);

            string originalStyle = element.GetAttribute("style");
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])", element, "style", "border: 2px solid red; border-style: dashed;");
            MethodHelper.SleepInSeconds(1);
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])", element, "style", originalStyle);
        }

        protected void ClickToElementByJs(IWebDriver driver, string locator)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].click();", GetElement(driver, locator));
        }

        protected void ScrollToElement(IWebDriver driver, string locator)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", GetElement(driver, locator));
        }

        protected void ScrollToElement(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", GetElement(driver, GetDynamicXpath(locator, dynamicValues)));
        }

        protected void RemoveAttributeInDOM(IWebDriver driver, string locator, string attribute)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].removeAttribute('" + attribute + "');", GetElement(driver, locator));
        }

        protected void RemoveAttributeInDOM(IWebDriver driver, string locator, string attribute, params string[] dynamicValues)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].removeAttribute('" + attribute + "');", GetElement(driver, GetDynamicXpath(locator, dynamicValues)));
        }

        protected void ChangeAttribute(IWebDriver driver, string locator, string attributeName, string attributeValue)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute('" + attributeName + "', '" + attributeValue + "')", GetElement(driver, locator));
        }

        protected void WaitUntilPageIsFullyLoaded(IWebDriver driver)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            explicitWait.Until(new Func<IWebDriver, bool>(driver =>
            {
                jsExecutor = (IJavaScriptExecutor)driver;
                return jsExecutor.ExecuteScript("return document.readyState").ToString()!.Equals("complete");
            }));
        }

        protected bool AreJQueryAndJSLoadedSuccess(IWebDriver driver)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            jsExecutor = (IJavaScriptExecutor)driver;

            Func<IWebDriver, bool> jQueryLoad = new((IWebDriver driver) =>
            {
                try
                {
                    return ((long)jsExecutor.ExecuteScript("return jQuery.active") == 0);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return false;
                }
            });

            Func<IWebDriver, bool> jsLoad = new((IWebDriver driver) =>
            {
                string? jsLoadStatus = jsExecutor.ExecuteScript("return document.readyState").ToString();
                if (jsLoadStatus is not null)
                {
                    return jsLoadStatus.Equals("complete");
                }
                return false;
            });

            return explicitWait.Until(jQueryLoad) && explicitWait.Until(jsLoad);
        }

        protected string GetElementValidationMessage(IWebDriver driver, string locator)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            return (string)jsExecutor.ExecuteScript("return arguments[0].validationMessage;", GetElement(driver, locator));
        }

        protected bool IsImageLoaded(IWebDriver driver, string locator)
        {
            jsExecutor = (IJavaScriptExecutor)driver;
            bool status = (bool)jsExecutor.ExecuteScript("return arguments[0].complete && typeof arguments[0].naturalWidth != 'undefined' && arguments[0].naturalWidth > 0", GetElement(driver, locator));
            return status;
        }

        protected bool AreAllImagesUploaded(IWebDriver driver, string locator)
        {
            List<IWebElement> elements = GetElements(driver, locator);
            jsExecutor = (IJavaScriptExecutor)driver;
            bool status = true;
            foreach (IWebElement element in elements)
            {
                status = (bool)jsExecutor.ExecuteScript("return arguments[0].complete && typeof arguments[0].naturalWidth != 'undefined' && arguments[0].naturalWidth > 0", element);
            }
            return status;
        }

        protected void WaitForElementVisible(IWebDriver driver, string locator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            explicitWait.Until(ExpectedConditions.ElementIsVisible(GetByLocator(locator)));
        }

        protected void WaitForElementVisible(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            By dynamicLocator = GetByLocator(GetDynamicXpath(locator, dynamicValues));
            explicitWait.Until(ExpectedConditions.ElementIsVisible(dynamicLocator));
        }

        protected void WaitForAllElementVisible(IWebDriver driver, string locator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            explicitWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(GetByLocator(locator)));
        }

        protected void WaitForElementClickable(IWebDriver driver, string locator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));

            explicitWait.Until(ExpectedConditions.ElementToBeClickable(GetByLocator(locator)));
        }

        protected void WaitForElementClickable(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            By dynamicLocator = GetByLocator(GetDynamicXpath(locator, dynamicValues));
            explicitWait.Until(ExpectedConditions.ElementToBeClickable(dynamicLocator));
        }

        protected void WaitForElementInvisible(IWebDriver driver, string locator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            OverrideGlobalTimeout(driver, shortTimeout);
            explicitWait.Until(ExpectedConditions.InvisibilityOfElementLocated(GetByLocator(locator)));
            OverrideGlobalTimeout(driver, longTimeout);
        }

        protected void SubmitElement(IWebDriver driver, string locator)
        {
            GetElement(driver, locator).Submit();
        }

        protected void SubmitElement(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            GetElement(driver, GetDynamicXpath(locator, dynamicValues)).Submit();
        }

        protected void WaitForElementInvisible(IWebDriver driver, string locator, params string[] dynamicValues)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            OverrideGlobalTimeout(driver, shortTimeout);

            By dynamicLocator = GetByLocator(GetDynamicXpath(locator, dynamicValues));
            explicitWait.Until(ExpectedConditions.InvisibilityOfElementLocated(dynamicLocator));
            OverrideGlobalTimeout(driver, longTimeout);
        }

        /// <summary>
        /// Send key to iframe element
        /// </summary>
        /// <param name="driver">Webdriver instance</param>
        /// <param name="value">string value will be sent to input</param>
        /// <param name="locator">locator of web element (css, xpath...)</param>
        protected void SendKeyToIframe(IWebDriver driver, string value, string locator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longTimeout));
            explicitWait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));

            IWebElement editable = driver.SwitchTo().ActiveElement();
            editable.SendKeys(value);
            driver.SwitchTo().DefaultContent();
        }

    }
}
