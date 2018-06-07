using OpenQA.Selenium;

namespace SeleniumFramework.Pages
{
    public class AdsListPage : TopMenuPage
    {
        private readonly By _addFavoritesLinkSelector = By.Id("a_fav");

        public AdsListPage(IWebDriver driver) : base(driver)
        {
            AdsTablePage = new AdsListTablePage(driver);
        }

        public AdsListTablePage AdsTablePage { get;}

        public void ClickAddToFavorites()
        {
            IWebElement link = WaitUntilElementVisible(_addFavoritesLinkSelector);
            ClickOnLink(link);
            WaitForAlert();
            ClickAlertOk();
        }
    }
}