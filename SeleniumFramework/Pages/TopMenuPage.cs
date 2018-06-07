using OpenQA.Selenium;

namespace SeleniumFramework.Pages
{
    public class TopMenuPage : BasePage
    {
        protected TopMenuPage(IWebDriver driver) : base(driver)
        {
        }

        public void GotoMemoPage()
        {
            Driver.Navigate().GoToUrl("https://www.ss.com/en/favorites/");
        }
    }
}