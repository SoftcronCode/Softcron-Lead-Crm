using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class Followup_Reporting : System.Web.UI.Page
    {
        compress compressobj = new compress();  // Unzip Method Class Object.
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindNotifications();
        }


        #region Methods
        // Method To Bind All Notifications.
        protected async void BindNotifications()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // string tableName = "lead_followup";
                    string UserID = Request.Cookies["userid"]?.Value;
                    string ipAddress = Request.UserHostAddress;
                    var apiUrl = Url + "ERP/Lead/Followup_notification";
                    var data = new
                    {
                        // tableName = tableName,
                        action = "SELECTALL",
                        id = 0,
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
                                if (responseObject.responseDynamic == null)
                                {

                                }
                                else
                                {
                                    var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                    if (dt.Rows.Count > 0)
                                    {
                                        //var filterdata = dt.AsEnumerable().Where(row => Convert.ToInt32(row["is_display"]) == 1).CopyToDataTable();
                                        NotificationRepeater.DataSource = dt;
                                        NotificationRepeater.DataBind();

                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + responseObject.responseMessage)})</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + response.StatusCode)})</script>", false);
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }

        }


        // Method to Calculate Time Difference
        protected string CalculateTimeDifference(object followupDate)
        {
            try
            {
                if (followupDate != null && followupDate != DBNull.Value)
                {
                    string dateStr = followupDate.ToString();
                    string[] dateFormats = { "MM/dd/yyyy HH:mm:ss", "dd-MMM-yyyy" };

                    if (DateTime.TryParse(dateStr, out DateTime originalDate))
                    {
                        string formattedDate = originalDate.ToString("yyyy-MM-dd HH:mm:ss");

                        if (DateTime.TryParseExact(formattedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime upcomingFollowUpDate))
                        {
                            TimeSpan timeDifference = DateTime.Now - upcomingFollowUpDate;
                            string formattedTime = timeDifference.Humanize();
                            return $"{formattedTime} ago";
                        }
                        else
                        {
                            return "Invalid date format in the database.";
                        }
                    }
                    else
                    {
                        return "Invalid date format in the database.";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
            return string.Empty;
        }

        #endregion


    }
}