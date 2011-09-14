using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using EPiServer;
using EPiServer.Web.Hosting;

namespace $rootnamespace$
{
    public class EPiServerHostingEnvironment : IHostingEnvironment
    {
        VirtualPathProvider _provider = null;

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
