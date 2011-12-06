using System;
using System.Configuration;
using System.Reflection;
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
        public static void StartUp(Func<string> hostNameResolver = null)
        {
            if (string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath))
            {
                AppDomain.CurrentDomain.AppendPrivatePath(AppDomain.CurrentDomain.BaseDirectory);
            }

            if (hostNameResolver == null)
            {
                hostNameResolver = () => "*";
            }
            SiteMappingConfiguration.CurrentHostNameResolver = hostNameResolver;

            if (SiteMappingConfiguration.Instance != null)
            {
                SiteMappingConfiguration.Instance.SiteId =
                    SiteMappingConfiguration.Instance.SiteIdForHost(hostNameResolver());
                typeof(PageReference).GetField("_start", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, null);
            }

            if (!(GenericHostingEnvironment.Instance is EPiServerHostingEnvironment))
            {
                GenericHostingEnvironment.Instance = new EPiServerHostingEnvironment();
            }

            Global.BaseDirectory = ".";
            Global.InstanceName = "EPiServer Unit Test";

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
            CacheManager.Clear(); 
            PermanentLinkMapStore.Clear();
            StoreDefinition.ClearCache();
        }
    }
}