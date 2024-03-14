using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI.View.Login
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void PasswordResetEmailBtn_Click(object sender, EventArgs e)
        {
            string email_id = txt_email.Text.Trim();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string UserID = Request.Cookies["userid"]?.Value;
                    string ipAddress = Request.UserHostAddress;
                    var apiUrl = Url + "ERP/Login/SendPasswordResetEmail";
                    var data = new
                    {
                        email = email_id,
                        objCommon = new
                        {
                            insertedUserID = "string",
                            insertedIPAddress = "string",
                            dateShort = "dd-MM-yyyy",
                            dateLong = "dd-MM-yyyy- HH:mm:ss"
                        }
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
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);

                                Match match = Regex.Match(unzippedResponse, @"\d+");
                                if (match.Success)
                                {
                                    string otpValue = match.Value;
                                    Response.Cookies["Password_otp"].Value = otpValue;
                                    Response.Cookies["EmailId"].Value = email_id;
                                    txt_email.Text = string.Empty;
                                    Response.Redirect("/View/Login/VerificationCode", false);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + responseObject.responseMessage)})</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Request failed with status code : " + response.StatusCode)})</script>", false);
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
