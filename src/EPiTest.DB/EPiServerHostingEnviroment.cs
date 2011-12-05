using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using EPiServer;
using EPiServer.Web.Hosting;

namespace EPiTest.DB
{
    public class EPiServerHostingEnvironment : IHostingEnvironment
    {
        VirtualPathProvider _provider = null;

        public EPiServerHostingEnvironment()
        {
            //We need the first provider to be one that doesn't delegates to its parent.
            //The default hosting environment uses a MapPathBasedVirtualPathProvider but that's internal
            //so we use our custom dummy provider.
            _provider = new DummyVirtualPathProvider();
        }

        public void RegisterVirtualPathProvider(VirtualPathProvider virtualPathProvider)
        {
            // Sets up the provider chain
            typeof(VirtualPathProvider)
                .GetField("_previous", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(virtualPathProvider, _provider);

            _provider = virtualPathProvider;
        }

        public VirtualPathProvider VirtualPathProvider
        {
            get { return _provider; }
        }

        public string MapPath(string virtualPath)
        {
            return Path.Combine(ApplicationPhysicalPath, VirtualPathUtility.ToAbsolute(virtualPath, ApplicationVirtualPath).Replace('/', '\\'));
        }

        public string ApplicationID
        {
            get { return String.Empty; }
        }

        public string ApplicationPhysicalPath
        {
            get { return Global.BaseDirectory; }
        }

        public string ApplicationVirtualPath
        {
            get { return "/"; }
        }
    }
}
