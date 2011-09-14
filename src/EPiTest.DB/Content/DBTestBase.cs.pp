using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace $rootnamespace$
{
    public class DbTestBase
    {
        Establish context = () =>
        {
            EPiServerSetup.StartUp();
        };

        Cleanup after = () =>
        {
            Database.RestoreFromSnapshot();
            EPiServerSetup.ClearCache();
        };
    }
}
