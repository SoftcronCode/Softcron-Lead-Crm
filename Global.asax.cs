using DSERP_Client_UI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace DSERP_Client_UI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            RouteTable.Routes.MapPageRoute("default", "page/{name}", "~/Default.aspx");
            RouteTable.Routes.MapPageRoute("dashboard", "dashboard", "~/Dashboard.aspx");
            RouteTable.Routes.MapPageRoute("form-grouping", "form-grouping", "~/AssignFormToGroup.aspx");
            RouteTable.Routes.MapPageRoute("assign-group", "assign-group", "~/AssignGroupToUser.aspx");
            RouteTable.Routes.MapPageRoute("new-lead", "new-lead", "~/AddNewLead.aspx");
            RouteTable.Routes.MapPageRoute("AddNewCustomer", "add-new-customer", "~/AddNewCustomer.aspx");
            RouteTable.Routes.MapPageRoute("QuotationMaster", "quotation-master", "~/QuotationMaster.aspx");
            RouteTable.Routes.MapPageRoute("SelectQuotation", "select-quotation", "~/SelectQuotation.aspx");
            RouteTable.Routes.MapPageRoute("SendQuotation", "send-quotation", "~/SendQuotation.aspx");
            RouteTable.Routes.MapPageRoute("SalesPage", "sales", "~/Sales.aspx");
            RouteTable.Routes.MapPageRoute("SalesReport", "sales-reporting", "~/SalesReporting.aspx");
            RouteTable.Routes.MapPageRoute("AddPayment", "add-payment", "~/AddPayment.aspx");
            RouteTable.Routes.MapPageRoute("PaymentReport", "payment-reporting", "~/PaymentReporting.aspx");
            RouteTable.Routes.MapPageRoute("AllFollowupNotification", "all-notification", "~/Followup_Reporting.aspx");


            RouteTable.Routes.MapPageRoute("loginPage", "view/login/login", "~/View/Login/Login.aspx");
            RouteTable.Routes.MapPageRoute("ForgotPassword", "view/login/forgotpassword", "~/View/Login/ForgotPassword.aspx");
            RouteTable.Routes.MapPageRoute("VerificationCode", "view/login/verificationcode", "~/View/Login/VerificationCode.aspx");
            RouteTable.Routes.MapPageRoute("ResetPassword", "view/login/resetpassword", "~/View/Login/ResetPassword.aspx");
            RouteTable.Routes.MapPageRoute("CreateUser", "create-user", "~/CreateUser.aspx");
            RouteTable.Routes.MapPageRoute("Invoice", "invoice", "~/Invoice.aspx");


        }

    }
}