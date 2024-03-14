using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI.View.Login
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.


        protected void Page_Load(object sender, EventArgs e)
        {
            string EmailID = Request.Cookies["EmailId"]?.Value;
            if (string.IsNullOrEmpty(EmailID))
            {
                Response.Redirect("/View/Login/Login");
            }
        }

        protected async void resetPasswordBtn_Click(object sender, EventArgs e)
        {
            string EmailID = Request.Cookies["EmailId"]?.Value;
            try
            {   
                using (var httpClient = new HttpClient())
                {
                    string ipAddress = Request.UserHostAddress;      // Find IP Address 
                    var apiUrl = Url + "ERP/Setup/ChangePassword";   // Api Url
                    var data = new
                    {
                        newPassword = textbox_password.Text.Trim(),
                        userEmail = EmailID
                    };

                    try
                    {
                        var jsondata = JsonConvert.SerializeObject(data);
                        var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var responseObject = JsonConvert.DeserializeObject<ResponseClass>(responseContent);
                            if (responseObject.responseCode == 1)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>success('Error: " + responseObject.responseMessage + "')</script>", false);
                                Response.Cookies["Password_otp"].Value = string.Empty;
                                Response.Cookies["EmailId"].Value = string.Empty;
                                Response.Redirect("/View/Login/login");
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + responseObject.responseMessage)})</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Request failed with status code: " + response.StatusCode)})</script>", false);
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }
    }
}