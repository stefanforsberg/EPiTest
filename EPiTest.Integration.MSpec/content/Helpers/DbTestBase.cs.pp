using EPiServer;
using Machine.Specifications;

namespace $rootnamespace$.Helpers
{
    public class DbTestBase
    {
        Establish context = () =>
            {
                EPiServerSetup.StartUp();
            };

        Cleanup after = () =>
        {
            DatabaseSetup.RestoreFromSnapshot();
            EPiServerSetup.ClearCache();
        };        
    }
}
