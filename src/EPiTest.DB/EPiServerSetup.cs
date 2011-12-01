using System;
using System.Configuration;
using EPiServer;
using EPiServer.BaseLibrary;
using EPiServer.ChangeLog;
using EPiServer.Configuration;
using EPiServer.Core;
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

            GenericHostingEnvironment.Instance = new EPiServerHostingEnvironment();

            Global.BaseDirectory = ".";
            Global.InstanceName = "EPiServer Unit Test";

            SiteMappingConfiguration.CurrentHostNameResolver = () => "*";
            InitializationModule.FrameworkInitialization(HostType.WebApplication);

            Settings.InitializeAllSettings(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));

            ClassFactory.Instance = new DefaultBaseLibraryFactory(String.Empty);
            ClassFactory.RegisterClass(typeof(IRuntimeCache), typeof(DefaultRuntimeCache));
            ClassFactory.RegisterClass(typeof(IChangeLog), typeof(NullChangeLog));
            
            LanguageManager.Instance = new LanguageManager(".");

            DataAccessBase.Initialize(
                ConfigurationManager.ConnectionStrings[Settings.Instance.ConnectionStringName],
                TimeSpan.Zero,
                0,
                TimeSpan.Zero);
        }

        public static void ClearCache()
        {
            DataFactoryCache.Clear();
            CacheManager.Clear();PermanentLinkMapStore.Clear();
        }
    }
}