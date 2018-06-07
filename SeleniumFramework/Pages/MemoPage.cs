using OpenQA.Selenium;

namespace SeleniumFramework.Pages
{
    public class MemoPage : TopMenuPage
    {
        public MemoPage(IWebDriver driver) : base(driver)
        {
            AdsTablePage = new AdsListTablePage(driver);
        }

        public AdsListTablePage AdsTablePage { get; set; }
    }
}