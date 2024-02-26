using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace DSERP_Client_UI
{
    public partial class CreateUser : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();


        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await BindDataTable();
            }
        }



        #region AlL INSERT, UPDATE, DELETE Methods

        // Method when Submit Button Click
        protected async void ButtonSubmitClick_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedRole = "";

                if (RadioButton1.Checked)
                {
                    selectedRole = "Admin";
                }
                else if (RadioButton2.Checked)
                {
                    selectedRole = "User";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Error: Please Select Role First. ")})</script>", false);
                    return;
                }

                int clientMasterID = int.Parse(Request.Cookies["clientid"]?.Value);
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                using (var httpClient = new HttpClient())
                {

                    var apiUrl = Url + "ERP/Setup/AddClientUser";

                    var data = new
                    {
                        clientMasterID = clientMasterID,
                        userDisplayName = TextBox_displayName.Text,
                        appAccessUserName = TextBox_userName.Text,
                        userEmailID = TextBox_email.Text,
                        appAccessPWD = Text_password.Text,
                        userRole = selectedRole,
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
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    await BindDataTable();
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "clearInputFields", "$(document).ready(function() { clearFields(); });", true);
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>success('" + responseObject.responseMessage + "')</script>", false);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + responseObject.responseMessage)})</script>", false);
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

        // Method when Update Button Click
        protected void ButtonUpdatClick_Click(object sender, EventArgs e)
        {
            
        }




        #endregion



        #region DataTable Methods

        // Bind DataTable
        protected async Task BindDataTable()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "lead_status_master";

                var (ErrorMessage, dt) = await commonMethods.SelectAllData(TableName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }



        // Method to change the status.
        protected async void btnChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string action = "UPDATE";
                string tablename = "client_user_details";

                Button btn = sender as Button;
                GridViewRow row = btn.NamingContainer as GridViewRow;
                int id = Convert.ToInt32(gridview.DataKeys[row.RowIndex].Value);
                string StatusValue;
                if (btn.Text == "Active")
                {
                    StatusValue = "NO";
                }
                else
                {
                    StatusValue = "YES";
                }

                List<ColumnData> textBoxDataList = new List<ColumnData>
            {
                new ColumnData { columnValue = StatusValue, columnName = "ActiveStatus", columnDataType = "105001" },
            };

                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    await BindDataTable();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }


        //  method for the delete 
        protected async void gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gridview.DataKeys[e.RowIndex].Value);
                int rowIndex = Convert.ToInt32(e.RowIndex);
                string tableName = "customer_user_details";
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;

                var (Success, ErrorMessage) = await commonMethods.DeleteById(id, tableName, UserID, ipAddress);
                if (Success)
                {
                    await BindDataTable();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ex.Message)})</script>", false);
            }
        }


        // GridView RowUpdate  gridview button 
        protected async void Gridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gridview.DataKeys[e.RowIndex].Value);
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string tablename = "client_user_details";

            var (ErrorMessage, dt) = await commonMethods.GetRecordByID(UserID, ipAddress, tablename, id);

            if (dt != null && dt.Rows.Count > 0)
            {
                hiddenfield_userid.Value = dt.Rows[0]["UserID"].ToString();
                TextBox_userName.Text = dt.Rows[0]["AppAccessUserName"].ToString();
                TextBox_displayName.Text = dt.Rows[0]["UserDisplayName"].ToString();
                TextBox_email.Text = dt.Rows[0]["UserEmailID"].ToString();
                Text_password.Text = dt.Rows[0]["AppAccessPWD"].ToString();
                string UserRole = dt.Rows[0]["UserRole"].ToString();
                if (UserRole == "Admin")
                {
                    RadioButton1.Checked = true;
                }
                else
                {
                    RadioButton2.Checked = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
            }
        }


        #endregion




    }
}