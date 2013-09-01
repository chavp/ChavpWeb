using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BootstrapConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-1.8.2.js",
                "~/Scripts/jquery-ui-1.8.24.js",
                "~/Content/bootstrap-3.0.0/dist/js/bootstrap.js",
                "~/Scripts/knockout-2.3.0.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                "~/Content/themes/base/jquery.ui.all.css",
                "~/Content/bootstrap-3.0.0/dist/css/bootstrap.css",
                "~/Content/bootstrap-3.0.0/dist/css/bootstrap-theme.css"));
        }
    }
}