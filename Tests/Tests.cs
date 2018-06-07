using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumFramework.Core;
using SeleniumFramework.Pages;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private SsComPage _sscompage;
        private IWebDriver _driver;
        private AdsListPage _adListPage;
        private MemoPage _memoPage;

        [SetUp]
        public void BeforeTest()
        {
            _driver = LocalWebdriver.CreateFirefoxDriver();
            _sscompage = new SsComPage(_driver);
            _adListPage = new AdsListPage(_driver);
            _memoPage = new MemoPage(_driver);
            _sscompage.GotoSsComPage();
        }

        [TearDown]
        public void AfterTest()
        {
            _driver.Quit();
        }

        [Test]
        public void AddAdvertisementToFavourites()
        {
            _sscompage.OpenAnyCategoryPage();
            _sscompage.OpenAnySubcategory();
            Assert.IsTrue(_adListPage.AdsTablePage.CheckIfAdsListPageDisplayed(),
                "Expected ads list page to be visible");
            _adListPage.AdsTablePage.ClickOnRandomPagingButtonIfExist();
            Assert.IsTrue(_adListPage.AdsTablePage.VerifyAdsExists(), "Ads list page was not present");
            _adListPage.AdsTablePage.OpenAnyAd(out string adId, out string addShortenedTex);
            _adListPage.ClickAddToFavorites();
            _sscompage.GotoMemoPage();
            _memoPage.AdsTablePage.AssertAddExist(adId, addShortenedTex);
        }
    }
}