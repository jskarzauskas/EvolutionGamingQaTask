using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace SeleniumFramework.Pages
{
    public class SsComPage : TopMenuPage
    {
        private readonly string SSpageUrl = "https://www.ss.com/";
        private readonly By _categoriesHeadersSelector = By.CssSelector("div.main_head2");
        private readonly By _subCategoriesSelector = By.CssSelector("a.a_category");

        public SsComPage(IWebDriver driver) : base(driver)
        {
        }


        public void GotoSsComPage()
        {
            GoToPage(SSpageUrl);
        }

        public void OpenAnyCategoryPage()
        {
            ReadOnlyCollection<IWebElement> categories = GetCategoriesList();

            //Get random category
            ClickOnRandomElement(categories);
        }

        public void OpenAnySubcategory()
        {
            ReadOnlyCollection<IWebElement> subCategoriesList = GetSubCategoriesList();

            //Get random category
            ClickOnRandomElement(subCategoriesList);

            if (IsSubCategoryPageDisplayed())
            {
                OpenAnySubcategory();
            }
        }


        private bool IsSubCategoryPageDisplayed()
        {
            return !CheckIfListIsEmpty(GetSubCategoriesList().ToList<object>());
        }

        private ReadOnlyCollection<IWebElement> GetSubCategoriesList()
        {
            ReadOnlyCollection<IWebElement> subcategories = Driver.FindElements(_subCategoriesSelector);

            return subcategories;
        }

        private ReadOnlyCollection<IWebElement> GetCategoriesList()
        {
            ReadOnlyCollection<IWebElement> categories = Driver.FindElements(_categoriesHeadersSelector);
            return categories;
        }
    }
}