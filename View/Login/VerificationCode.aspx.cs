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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI.View.Login
{
    public partial class VereficationCode : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.

        protected void Page_Load(object sender, EventArgs e)
        {
            string EmailID = Request.Cookies["EmailId"]?.Value;
            string sessionOTP = Request.Cookies["Password_otp"]?.Value;
            if(!string.IsNullOrEmpty(EmailID) && !string.IsNullOrEmpty(sessionOTP))
            {
                string maskedEmail = MaskEmail(EmailID);
                lbl_emailid.Text = maskedEmail;
            }
            else
            {
                Response.Redirect("/View/Login/Login");
            }
           

        }

        // when button Submit OTP Click.
        protected void submitOTP_Click(object sender, EventArgs e)
        {
            try
            {
                string userEnteredOTP = $"{first.Text}{second.Text}{third.Text}{fourth.Text}{fifth.Text}{sixth.Text}";
                string sessionOTP = Request.Cookies["Password_otp"]?.Value;

                if (!string.IsNullOrEmpty(sessionOTP))
                {
                    if (userEnteredOTP == sessionOTP)
                    {
                        Response.Redirect("/View/Login/ResetPassword");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning", "<script>warning('Warning: Wrong Otp')</script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning", "<script>warning('Warning: OTP is not Valid. Try Resend Code Again')</script>", false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        // Function to mask the email address
        private string MaskEmail(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    int atIndex = email.IndexOf('@');
                    if (atIndex > 0)
                    {
                        string maskedPart = new string('*', atIndex);
                        string visiblePart = email.Substring(atIndex);
                        return maskedPart + visiblePart;
                    }
                }
                return email; // Return the original email if it cannot be masked
            }
            catch (Exception)
            {

                throw;
            }
        }


        // method call when resend OTP Button Click.
        protected async void resendOTP_Click(object sender, EventArgs e)
        {
            string sessionEmailID = Session["EmailId"] as string;
            if (!string.IsNullOrEmpty(sessionEmailID))
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        string UserID = Request.Cookies["userid"]?.Value;
                        string ipAddress = Request.UserHostAddress;
                        var apiUrl = Url + "ERP/Login/SendPasswordResetEmail";
                        var data = new
                        {
                            email = sessionEmailID,
                            objCommon = new
                            {
                                insertedUserID = UserID,
                                insertedIPAddress = ipAddress,
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
                                        Response.Redirect(Request.Url.ToString(), true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: Try Again!')</script>", false);
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
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                Response.Redirect("/View/Login/forgotpassword", false);
            }

        }



    }
}