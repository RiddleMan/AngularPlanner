﻿using System.Web;
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
                        "~/Scripts/nprogress.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/highcharts.js",
                        "~/Scripts/highcharts-more.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-*",
                        "~/Scripts/highcharts-ng.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory(
                    "~/App", "*.js", true)
                .Include("~/App/app.js"));

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
