using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace EPiTest.DB
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
