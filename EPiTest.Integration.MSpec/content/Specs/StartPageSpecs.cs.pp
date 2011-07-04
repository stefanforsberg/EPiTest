using $rootnamespace$.Helpers;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using Machine.Specifications;

namespace $rootnamespace$.Specs
{
    public class StartPageSpecs : DbTestBase
    {
        public static PageData GetStartPage()
        {
            return DataFactory.Instance.GetPage(PageReference.StartPage);
        }
    }

    public class when_loading_the_startpage : StartPageSpecs
    {
        protected static PageData StartPage;

        Because of = () =>
            {
                StartPage = GetStartPage();
            };

        It should_have_a_page_link_that_indicate_that_it_is_the_start_page = () =>
            {
                StartPage.PageLink.ShouldEqual(PageReference.StartPage);
            };
    }

    public class when_changing_the_name_of_the_start_page : StartPageSpecs
    {
        static string _pageNameAfterSave;
        static string _newPageName = "www";

        Establish context = () =>
            {
                var startPage = GetStartPage().CreateWritableClone();
                startPage.PageName = _newPageName;
                DataFactory.Instance.Save(startPage, SaveAction.Publish | SaveAction.ForceCurrentVersion, AccessLevel.NoAccess);
            };

        Because of = () =>
        {
            _pageNameAfterSave = GetStartPage().PageName;
        };

        It should_have_the_new_name_when_it_is_loaded_again = () =>
            {
                _pageNameAfterSave.ShouldEqual(_newPageName);
            };

    }
}
