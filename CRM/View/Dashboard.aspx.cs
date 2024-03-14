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
    public partial class Dashboard : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoginMessage = Request.Cookies["LoginMessage"]?.Value;
            if (!string.IsNullOrEmpty(LoginMessage))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success('{LoginMessage}')</script>", false);
                Response.Cookies["LoginMessage"].Expires = DateTime.Now.AddDays(-1);
            }
        }

    }
}