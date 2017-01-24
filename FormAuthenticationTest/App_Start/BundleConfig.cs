using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace FormAuthenticationTest
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Script").Include(
                "~/Scripts/jquery-2.0.3.js",
                "~/Scripts/ajaxfileupload.js",
                "~/Scripts/UploadFile.js"));

            bundles.Add(new StyleBundle("~/bundle/css").Include("~/Content/css/Fieldset.css"));

            bundles.Add(new StyleBundle("~/Content/css/bundle").Include("~/Content/css/MenuBar.css",
                "~/Content/css/Layout.css"));
            bundles.Add(new StyleBundle("~/Content/themes/default/bundle").Include("~/Content/themes/default/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/themes/bundle").Include("~/Content/themes/icon.css"));
            bundles.Add(new ScriptBundle("~/Scripts/bundle").Include("~/Scripts/jquery-2.0.3.js", 
                "~/Scripts/jquery.easyui-1.4.2.js"));

            
        }
    }
}