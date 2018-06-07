using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;

namespace SeleniumFramework.Pages
{
    public class AdsListTablePage : BasePage
    {
        private readonly string _adShortenedTextSelector = "a#dm_{0}";

        private readonly By _paginButtonsListSelector = By.CssSelector("form#filter_frm div.td2 a");

        public AdsListTablePage(IWebDriver driver) : base(driver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "form div.td2")]
        private IWebElement PageingArea { get; set; }

        [FindsBy(How = How.XPath, Using = "//form//table//tr[./td[@class='msga2 pp0']]")]
        private IList<IWebElement> AdsList { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form#filter_frm")]
        private IWebElement AdsListArea { get; set; }

        public void OpenAnyAd(out string adId, out string adShortenedTex)
        {
            ClickOnRandomPagingButtonIfExist();
            IWebElement adElement = GetRandomElementFromList(AdsList);
            OpenAd(adElement, out adId, out adShortenedTex);
            adShortenedTex = string.Empty;
        }

        public void OpenAd(IWebElement element, out string adId, out string adShortenedTex)
        {
            adId = element.GetAttribute("id").Replace("tr_", "");
            adShortenedTex = GetShortenedAdText(element);

            ScrollToElement(element);
            Actions action = new Actions(Driver);
            action.MoveToElement(element).Click().Build().Perform();
            WaitForPageLoad();
        }

        public void ClickOnRandomPagingButtonIfExist()
        {
            List<IWebElement> buttonList = GetPagingButtonsList().ToList();
            if (buttonList.Count.Equals(0))
            {
                return;
            }

            //Remove "Previuos" and "Last" buttons
            buttonList.RemoveAt(0);
            buttonList.RemoveAt(buttonList.Count - 1);

            Random rand = new Random();
            ClickOnLink(buttonList[rand.Next(0, buttonList.Count - 1)]);
        }

        public bool CheckIfAdsListPageDisplayed()
        {
            try
            {
                return AdsListArea.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AssertAddExist(string adId, string shortenedText)
        {
            IWebElement element;
            By selector = By.CssSelector(string.Format(_adShortenedTextSelector, adId));
            try
            {
                element = Driver.FindElement(selector);

                if (element.Text.Contains(shortenedText))
                {
                    return;
                }

                throw new AssertionException("Add is found, but short text do not match.");
            }
            catch (Exception)
            {
                throw new Exception($"Failed to find element with by selector {selector}");
            }
        }

        private ReadOnlyCollection<IWebElement> GetPagingButtonsList()
        {
            return Driver.FindElements(_paginButtonsListSelector);
        }

        private string GetShortenedAdText(IWebElement adTdRow)
        {
            return adTdRow.FindElement(By.CssSelector("div.d1 a")).Text;
        }

        public bool VerifyAdsExists()
        {
            return !CheckIfListIsEmpty(AdsList.ToList<object>());
        }
    }
}