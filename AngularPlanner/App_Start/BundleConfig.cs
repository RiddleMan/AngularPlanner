using System.Web;
using System.Web.Optimization;

namespace AngularPlanner
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-*"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/App/app.js")
                .IncludeDirectory(
                    "~/App", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/compatibility").Include(
                "~/Scripts/es5-shim.js",
                "~/Scripts/json3.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/angular-csp.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
