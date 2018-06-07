using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumFramework.Core;
using OpenQA.Selenium.Support;
using SeleniumExtras.WaitHelpers;

namespace SeleniumFramework.Pages
{
    public class BasePage
    {
        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "alert_ok")]
        public IWebElement AlertOkButton { get; set; }

        public By AlertAreaSelector = By.Id("alert_dv");

        protected IWebDriver Driver { get; set; }

        protected void GoToPage(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void WaitForAlert()
        {
            WaitUntilElementVisible(AlertAreaSelector);
        }

        public void ClickAlertOk()
        {
            ClickOnLink(AlertOkButton);
        }

        protected void ClickOnLink(IWebElement element)
        {
            element.Click();
            WaitForPageLoad();
        }

        protected void ClickOnRandomElement(IList<IWebElement> elementsList)
        {
            //Get random element
            IWebElement randElement = GetRandomElementFromList(elementsList);

            ClickOnLink(randElement);
        }


        public IWebElement GetRandomElementFromList(IList<IWebElement> elementsliList)
        {
            Random random = new Random();
            //Get random element
            return elementsliList[random.Next(0, elementsliList.Count - 1)];
        }

        protected bool CheckIfListIsEmpty(List<object> objectsList)
        {
            if (objectsList == null || objectsList.Count < 1)
            {
                return true;
            }

            return false;
        }

        protected void WaitForPageLoad()
        {
            int maxWaitTimeInSeconds = 3;
            string state = string.Empty;
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(maxWaitTimeInSeconds));

                wait.Until(d =>
                {
                    try
                    {
                        state = ((IJavaScriptExecutor) Driver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignore
                    }
                    catch (NoSuchWindowException)
                    {
                        //when popup is closed, switch to last windows
                        Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                    }

                    return state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) ||
                           state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase);
                });
            }
            catch (TimeoutException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw;
                }
            }
            catch (NullReferenceException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw;
                }
            }
            catch (WebDriverException)
            {
                if (Driver.WindowHandles.Count == 1)
                {
                    Driver.SwitchTo().Window(Driver.WindowHandles[0]);
                }

                state = ((IJavaScriptExecutor) Driver).ExecuteScript(@"return document.readyState").ToString();
                if (!(state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) ||
                      state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw;
                }
            }
        }


        protected void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor) Driver).ExecuteScript("arguments[0].scrollIntoView();", element);
        }

        public IWebElement WaitUntilElementVisible(By locator)
        {
            Driver.TurnOnImplicitlyWait();
            WebDriverWait wait = new WebDriverWait(Driver, Settings.TimeOutInSeconds);

            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            Driver.TurnOffImplicitlyWait();

            return element;
        }
    }
}