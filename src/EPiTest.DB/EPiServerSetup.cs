using System;
using System.Configuration;
using EPiServer;
using EPiServer.BaseLibrary;
using EPiServer.ChangeLog;
using EPiServer.Configuration;
using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.DataAccess;
using EPiServer.Framework.Initialization;
using EPiServer.Implementation;
using EPiServer.Web;
using EPiServer.Web.Hosting;
using InitializationModule = EPiServer.Framework.Initialization.InitializationModule;

namespace EPiTest.DB
{
    public class EPiServerSetup
    {
        public static void StartUp()
        {
            if (string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath))
            {
                AppDomain.CurrentDomain.AppendPrivatePath(AppDomain.CurrentDomain.BaseDirectory);
            }

            Global.BaseDirectory = ".";
            Global.InstanceName = "EPiServer Unit Test";

            Settings.InitializeAllSettings(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));

            SiteMappingConfiguration.Instance = new SiteMappingConfiguration();
            Settings.Instance = Settings.MapHostToSettings("*", true);

            ClassFactory.Instance = new DefaultBaseLibraryFactory(String.Empty);
            ClassFactory.RegisterClass(typeof(IRuntimeCache), typeof(DefaultRuntimeCache));
            ClassFactory.RegisterClass(typeof(IChangeLog), typeof(NullChangeLog));
            
            LanguageManager.Instance = new LanguageManager(".");

            GenericHostingEnvironment.Instance = new EPiServerHostingEnvironment();

            DataAccessBase.Initialize(
                ConfigurationManager.ConnectionStrings[Settings.Instance.ConnectionStringName],
                TimeSpan.Zero,
                0,
                TimeSpan.Zero);

            InitializationModule.FrameworkInitialization(HostType.TestFramework);
        }

        public static void ClearCache()
        {
            DataFactoryCache.Clear();
            CacheManager.Clear();
            PermanentLinkMapStore.Clear();
            StoreDefinition.ClearCache();
        }
    }
}