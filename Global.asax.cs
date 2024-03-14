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


            // CRM Routing
            RouteTable.Routes.MapPageRoute("AddNewCustomer", "add-new-customer", "~/CRM/View/AddNewCustomer.aspx");
            RouteTable.Routes.MapPageRoute("new-lead", "new-lead", "~/CRM/View/AddNewLead.aspx");
            RouteTable.Routes.MapPageRoute("AddPayment", "add-payment", "~/CRM/View/AddPayment.aspx");
            RouteTable.Routes.MapPageRoute("dashboard", "dashboard", "~/CRM/View/Dashboard.aspx");
            RouteTable.Routes.MapPageRoute("default", "page/{name}", "~/CRM/View/Default.aspx");
            RouteTable.Routes.MapPageRoute("SalesPage", "sales", "~/CRM/View/Sales.aspx");
            RouteTable.Routes.MapPageRoute("SelectQuotation", "select-quotation", "~/SelectQuotation.aspx");
            RouteTable.Routes.MapPageRoute("SendQuotation", "send-quotation", "~/SendQuotation.aspx");

            RouteTable.Routes.MapPageRoute("form-grouping", "form-grouping", "~/CRM/UserManagment/AssignFormToGroup.aspx");
            RouteTable.Routes.MapPageRoute("assign-group", "assign-group", "~/CRM/UserManagment/AssignGroupToUser.aspx");
            RouteTable.Routes.MapPageRoute("CreateUser", "create-user", "~/CRM/UserManagment/CreateUser.aspx");

            RouteTable.Routes.MapPageRoute("QuotationMaster", "quotation-master", "~/CRM/Template/QuotationMaster.aspx");
            RouteTable.Routes.MapPageRoute("Invoice", "invoice", "~/CRM/Template/Invoice.aspx");

            RouteTable.Routes.MapPageRoute("SalesReport", "sales-reporting", "~/CRM/Report/SalesReporting.aspx");
            RouteTable.Routes.MapPageRoute("PaymentReport", "payment-reporting", "~/CRM/Report/PaymentReporting.aspx");
            RouteTable.Routes.MapPageRoute("AllFollowupNotification", "all-notification", "~/CRM/Report/Followup_Reporting.aspx");

            RouteTable.Routes.MapPageRoute("loginPage", "view/login/login", "~/CRM/Login/Login.aspx");
            RouteTable.Routes.MapPageRoute("ForgotPassword", "view/login/forgotpassword", "~/CRM/Login/ForgotPassword.aspx");
            RouteTable.Routes.MapPageRoute("VerificationCode", "view/login/verificationcode", "~/CRM/Login/VerificationCode.aspx");
            RouteTable.Routes.MapPageRoute("ResetPassword", "view/login/resetpassword", "~/CRM/Login/ResetPassword.aspx");

        }

    }
}