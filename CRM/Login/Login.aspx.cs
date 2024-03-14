using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class Login : System.Web.UI.Page
    {
        compress compressobj = new compress();          // Unzip Method Class Object
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();


        // Login Button Click 
        protected async void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string ipAddress = Request.UserHostAddress;      // Find IP Address 
                    var apiUrl = Url + "ERP/Setup/ClientERPLogin";   // Api Url
                    var data = new
                    {
                        loginUID = Text_username.Text.Trim(),
                        loginPWD = Text_password.Text.Trim()
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
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);       // Unzip the Response.
                                Response.Cookies["ResponseData"].Value = unzippedResponse;                     // Add Response To Cookies.
                                Response.Cookies["LoginMessage"].Value = "Login Successfull!";
                                Response.Redirect("/dashboard");
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