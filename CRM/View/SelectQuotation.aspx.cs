using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class SelectQuotation : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();
            }
        }

        protected async void BindGridview()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Lead/GetAllQuotationData";

                var (ErrorMessage, dt) = await commonMethods.BindDataTable(apiUrl, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                    Session["quotationRecord"] = dt;
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }



        protected void btn_Send_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Send")
            {               
                int quotationMasterId = Convert.ToInt32(e.CommandArgument);
                string redirectedUrl = $"/send-quotation?ref=send&qmid={quotationMasterId}";
                Response.Redirect(redirectedUrl, false);
            }
        }


    }
}