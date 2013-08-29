using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Web.Models;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IDictionary<string, User> Members { get; set; }
        public static IDictionary<string, string[]> UserRoleDic { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapConfig.RegisterBundles(BundleTable.Bundles);

            Members = new Dictionary<string, User>();
            var chavp = new User
            {
                Name = "#:P",
                Email = "my.parinya@gmail.com",
                Password = "123456789".GetHashCode().ToString(),
            };

            Members.Add(chavp.Name, chavp);
            UserRoleDic = new Dictionary<string, string[]>();

            UserRoleDic.Add(chavp.Name, new string[]{"admin"});
        }

        
    }
}