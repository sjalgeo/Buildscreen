using System.Web.Optimization;

namespace ParkSquare.BuildScreen
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}