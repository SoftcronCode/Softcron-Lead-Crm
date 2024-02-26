using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using Humanizer;
using static Humanizer.On;
using System.Net;


namespace DSERP_Client_UI
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                string leadFollowupID = Request.Form["__EVENTARGUMENT"];
                if (!string.IsNullOrEmpty(leadFollowupID))
                {
                    // Handle the Clear button click here
                    //  ClearNotification(leadFollowupID);
                }

                string responseData = Request.Cookies["ResponseData"]?.Value;

                if (!string.IsNullOrEmpty(responseData))
                {
                    var userdata = JsonConvert.DeserializeObject<ClientUserData>(responseData);

                    if (userdata != null)
                    {
                        string ipAddress = Request.UserHostAddress;
                        Session["ipAddress"] = ipAddress;
                        // User Details
                        string username = userdata.User.UserDisplayName;
                        string userID = userdata.User.UserID.ToString();
                        string ClientMasterID = userdata.User.ClientMasterID.ToString();
                        string GroupMasterID = userdata.User.group_masterid.ToString();
                        string UserRole = userdata.User.userrole.ToString();
                        Label_username.Text = Label_uname.Text = username;
                        Response.Cookies["userid"].Value = userID;
                        Response.Cookies["clientid"].Value = ClientMasterID;
                        BindMenuGroupRepeater();

                        if (!string.IsNullOrEmpty(UserRole) && UserRole == "Admin")
                        {
                            manage_user.Attributes.Add("style", "display:block");
                        }
                        else
                        {
                            manage_user.Attributes.Add("style", "display:none");
                        }
                    }
                }
                else
                {
                    string script = "alert('Please Login First !'); window.location.href = '/View/Login/login';";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", script, true);
                }
                BindNotification();
            }

        }



        #region Side Menu Bind Methods

        // method to bind side menu
        protected void BindMenuGroupRepeater()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string UserID = Request.Cookies["userid"]?.Value;
                    string ipAddress = Request.UserHostAddress;
                    var apiUrl = Url + "ERP/Setup/ManageUser";
                    var data = new
                    {
                        action = "BindUserGroup", //Action Name
                        userid = UserID,
                        groupmasterid = 0,
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
                        var response = httpClient.PostAsync(apiUrl, content).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = response.Content.ReadAsStringAsync().Result;
                            var responseObject = JsonConvert.DeserializeObject<ResponseClass>(responseContent);
                            if (responseObject.responseCode == 1)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    OuterRepeater.DataSource = dt;
                                    OuterRepeater.DataBind();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + responseObject.responseMessage)})</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Request failed with status code : " + response.StatusCode)})</script>", false);
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // method to bind the form names of groups.
        protected void OuterRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Repeater InnerRepeater = (Repeater)e.Item.FindControl("InnerRepeater");

                    if (InnerRepeater != null) // Check if the control was found
                    {
                        String GroupID = DataBinder.Eval(e.Item.DataItem, "group_masterid").ToString();

                        using (var httpClient = new HttpClient())
                        {
                            string UserID = Request.Cookies["userid"]?.Value;
                            string ipAddress = Request.UserHostAddress;
                            var apiUrl = Url + "ERP/Setup/ManageUser";
                            var data = new
                            {
                                action = "BindUserForm", //Action Name
                                userid = 0,
                                groupmasterid = GroupID,
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
                                var response = httpClient.PostAsync(apiUrl, content).Result;

                                if (response.IsSuccessStatusCode)
                                {
                                    var responseContent = response.Content.ReadAsStringAsync().Result;
                                    var responseObject = JsonConvert.DeserializeObject<ResponseClass>(responseContent);
                                    if (responseObject.responseCode == 1)
                                    {
                                        var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                        if (dt.Rows.Count > 0)
                                        {
                                            InnerRepeater.DataSource = dt;
                                            InnerRepeater.DataBind();
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + responseObject.responseMessage)})</script>", false);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Request failed with status code : " + response.StatusCode)})</script>", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // This Method is Execute when user click on the list Item of Side Menu.
        protected void btnTable_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnTable = (LinkButton)sender;
                string commandArgument = btnTable.CommandArgument;

                // Split the commandArgument string by the delimiter to retrieve the individual values
                string[] values = commandArgument.Split('|');
                Session["tableName"] = values[0];
                Session["clientMasterID"] = values[1];
                Session["tableCode"] = values[2];
                Session["extensionTableId"] = values[3];
                string Url = values[4];
                string Default = values[5];
                Session["TableAliasName"] = values[6];
                if (Default == "0")
                {
                    Response.Redirect("/" + Url, false);
                }
                else
                {
                    Response.Redirect("/page/" + Url, false);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        // method call when user click on the side menu bar form.
        protected void btnMenu_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnTable = (LinkButton)sender;
                string commandArgument = btnTable.CommandArgument;
                string[] values = commandArgument.Split(',');
                Session["tableName"] = values[0];
                Session["clientMasterID"] = values[1];
                Session["tableCode"] = values[2];
                Session["extensionTableId"] = values[3];
                string Url = values[4];
                string Default = values[5];
                Session["TableAliasName"] = values[6];
                if (Default == "0")
                {
                    Response.Redirect("/" + Url, false);
                }
                else
                {
                    Response.Redirect("/page/" + Url, false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Method For Button Logout Click.
        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear All Session Value.
                Session.Abandon();

                // Clear all cookies
                if (Request.Cookies != null)
                {
                    foreach (string cookie in Request.Cookies.AllKeys)
                    {
                        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                    }
                }
                Response.Redirect("~/view/login/login", false);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion


        #region Notification Methods

        // Method call on page load
        protected void BindNotification()
        {
            string action = "SELECT";
            int id = 0;
            Followup_notification_Actions(action, id);
        }


        // method to bind the notification on the menu bar.
        protected void Followup_notification_Actions(string Action, int ID)
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
                        action = Action,
                        id = ID,
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
                        var response = httpClient.PostAsync(apiUrl, content).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = response.Content.ReadAsStringAsync().Result;
                            var responseObject = JsonConvert.DeserializeObject<ResponseClass>(responseContent);
                            if (responseObject.responseCode == 1)
                            {
                                if (responseObject.responseDynamic == null)
                                {
                                    notification_badge.Attributes["style"] = "display: none;";
                                    NotificationRepeater.DataSource = null;
                                    NotificationRepeater.DataBind();
                                }
                                else
                                {
                                    var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                    if (dt.Rows.Count > 0)
                                    {
                                        notification_badge.Attributes["style"] = "display: block;";
                                        notification_count.Text = dt.Rows.Count.ToString();
                                        NotificationRepeater.DataSource = dt;
                                        NotificationRepeater.DataBind();
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + responseObject.responseMessage)})</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Request failed with status code : " + response.StatusCode)})</script>", false);
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        // Method to Call When Clear Button Click on the Notification.
        protected void clear_notification_btn_Click(object sender, EventArgs e)
        {
            LinkButton clearBtn = (LinkButton)sender;
            int leadFollowupID = Convert.ToInt32(clearBtn.CommandArgument);
            string action = "CLEAR";
            int id = leadFollowupID;
            Followup_notification_Actions(action, id);
        }


        // Method Call when Mark All Read Notification Button Click.
        protected void mark_all_read_btn_Click(object sender, EventArgs e)
        {
            string action = "CLEARALL";
            int id = 0;
            Followup_notification_Actions(action, id);
        }

        // Method to Calculate Time Difference
        protected string CalculateTimeDifference(object followupDate)
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

            return string.Empty;

        }


        #endregion 


    }


    public class ClientUserData
    {
        public User User { get; set; }
        public List<Table> Tables { get; set; }
    }

    public class User
    {
        public string UserDisplayName { get; set; }
        public int UserID { get; set; }
        public int AppAccessTypeID { get; set; }
        public int ClientMasterID { get; set; }
        public string group_masterid { get; set; }
        public string userrole { get; set; }
    }

    public class Table
    {
        public int TableCode { get; set; }
        public string TableName { get; set; }
        public int ClientMasterID { get; set; }
        public int ExtensionTableID { get; set; }
        public string AliasName { get; set; }
        public string Url { get; set; }
        public int Is_Default { get; set; }
    }

}
