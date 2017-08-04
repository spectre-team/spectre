using System.Web;
using System.Web.Optimization;

namespace Spectre
{
    /// <summary>
    /// Configures bundle
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(bundle: new ScriptBundle(virtualPath: "~/bundles/jquery").Include(
                virtualPath: "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(bundle: new ScriptBundle(virtualPath: "~/bundles/modernizr").Include(
                virtualPath: "~/Scripts/modernizr-*"));

            bundles.Add(bundle: new ScriptBundle(virtualPath: "~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(bundle: new StyleBundle(virtualPath: "~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}
