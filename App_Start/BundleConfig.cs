using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DSERP_Client_UI.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include
            (
            // "~/assets/js/jquery-3.7.0.js",
            "~/assets/js/toastr/toastr-v2.1.3.min.js",
            "~/assets/js/toastr/toastr-msg.js",
            "~/assets/js/chosen.jquery-v1.8.7.min.js",
            "~/assets/js/popper.min.js",
            "~/assets/js/simplebar.min.js",
            "~/assets/js/bootstrap.min.js",
            "~/assets/js/feather.min.js",
            "~/assets/js/custom-font.js",
            "~/assets/js/pcoded.js",
            "~/assets/js/datatable/jquery.dataTables-1.13.5.min.js",
            "~/assets/js/datatable/dataTables.buttons-v2.4.1.min.js",
            "~/assets/js/datatable/dataTables.searchBuilder-v1.6.0.min.js",
            "~/assets/js/datatable/dataTables.dateTime-v1.5.1.min.js",
            "~/assets/js/datatable/jszip-v3.10.1.min.js",
            "~/assets/js/datatable/pdfmake-v0.1.53.min.js",
            "~/assets/js/datatable/vfs_fonts.js",
            "~/assets/js/datatable/buttons.html5-v2.4.1.min.js",
            "~/assets/js/datatable/buttons.print-v2.4.1.min.js"
            ));

            //wrapup all css in a bundle  
            bundles.Add(new StyleBundle("~/bundles/WebFormsCss").Include
            (
            "~/assets/css/inter.css",
           // "~/assets/css/font-awesome/all.min.css",
            "~/assets/css/toastr/toastr.min.css",
            "~/assets/css/style.css",
            "~/assets/css/style-preset.css",
            "~/assets/css/datatable/jquery.dataTables-v1.13.5.min.css",
            "~/assets/css/datatable/buttons.dataTables-v2.4.1.min.css",
            "~/assets/css/datatable/searchBuilder.dataTables-v1.6.0.min.cs",
            "~/assets/css/datatable/dataTables.dateTime-v1.5.1.min.css",
            "~/assets/css/chosen-v1.8.7.min.css"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}