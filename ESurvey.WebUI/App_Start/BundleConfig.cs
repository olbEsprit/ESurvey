using System.Web.Optimization;

namespace IdentitySample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/xeditable.min.css"
                      ));
           
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-sanitize.min.js",
                "~/Scripts/angular-resource-min.js",
                "~/Scripts/xeditable.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angularUi").Include(
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                "~/Scripts/angular-ui/ui-bootstrap.min.js",
                "~/Scripts/angular-ui/angular-modal-service.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                "~/Scripts/angapps/adminapp.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/editorApp").Include(
            "~/scripts/angapps/editorApp.js",
            "~/scripts/angapps/surveyDetailsEditor.js",
            "~/scripts/angapps/questionListController.js",
            "~/scripts/angapps/questionCreatorController.js",
            "~/scripts/angapps/questionEditorController.js"
            ));
        }
    }
}
