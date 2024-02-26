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
    public partial class AssignGroupToUser : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGroupListBox();
                BindUserDropdown();
                BindDataTable();
            }
        }

        protected async void BindGroupListBox()
        {
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string TableName = "group_master";
            string ColumnName = "group_name";

            var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl_grouplist.DataSource = dt;
                ddl_grouplist.DataValueField = dt.Columns[1].ColumnName;
                ddl_grouplist.DataTextField = dt.Columns[0].ColumnName;
                ddl_grouplist.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
            }
        }

        protected async void BindUserDropdown()

        {
            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                var data = new
                {
                    action = "DropdownByPK",   //action name
                    searchText = "",
                    filterID = "0",
                    filterID1 = "0",
                    filterID2 = "UserID", //Column Name 
                    filterID3 = "UserDisplayName", // column name
                    searchCriteria = "client_user_details", // Table Name
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
                                ddl_username.DataSource = dt;
                                ddl_username.DataValueField = dt.Columns[0].ColumnName;
                                ddl_username.DataTextField = dt.Columns[1].ColumnName;
                                ddl_username.DataBind();
                                ddl_username.Items.Insert(0, new ListItem("--Select--", "0"));
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

        protected async void ButtonSubmit_Click(object sender, EventArgs e)
        {
            string totalGroupId = null;

            if (ddl_grouplist.Items.Count > 0)
            {
                string GroupId = null;
                for (int i = 0; i < ddl_grouplist.Items.Count; i++)
                {
                    if (ddl_grouplist.Items[i].Selected)
                    {
                        string selectedItemId = ddl_grouplist.Items[i].Value + ",";
                        GroupId = GroupId + selectedItemId;
                    }
                }

                if (!string.IsNullOrEmpty(GroupId))
                {
                    totalGroupId = GroupId.TrimEnd(',');
                }
            }

            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string action = "INSERT";
            string tablename = "user_group_assign";
            int id = 0;

            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
               new ColumnData { columnValue = ddl_username.SelectedValue, columnName = "userid", columnDataType = "105002" },
               new ColumnData { columnValue = totalGroupId, columnName = "group_masterid", columnDataType = "105001" }
            };

            var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
            if (Success)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: " + ErrorMessage)})</script>", false);
                ButtonSubmit.Visible = true;
                ButtonUpdate.Visible = false;
                ddl_username.SelectedIndex = 0;
                ddl_grouplist.ClearSelection();
                BindDataTable();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
            }
        }

        protected async void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string totalFormId = null;

            if (ddl_grouplist.Items.Count > 0)
            {
                string GroupId = null;
                for (int i = 0; i < ddl_grouplist.Items.Count; i++)
                {
                    if (ddl_grouplist.Items[i].Selected)
                    {
                        string selectedItemId = ddl_grouplist.Items[i].Value + ",";
                        GroupId = GroupId + selectedItemId;
                    }
                }

                if (!string.IsNullOrEmpty(GroupId))
                {
                    totalFormId = GroupId.TrimEnd(',');
                }
            }

            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string action = "UPDATE";
            string tablename = "user_group_assign";
            int id = int.Parse(HiddenFieldID.Value);

            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
               new ColumnData { columnValue = ddl_username.SelectedValue, columnName = "userid", columnDataType = "105002" },
               new ColumnData { columnValue = totalFormId, columnName = "group_masterid", columnDataType = "105001" }
            };

            var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
            if (Success)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: " + ErrorMessage)})</script>", false);
                ButtonUpdate.Visible = false;
                ButtonSubmit.Visible = true;
                ddl_username.SelectedIndex = 0;
                ddl_grouplist.ClearSelection();
                BindDataTable();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            ddl_username.SelectedIndex = 0;
            ddl_grouplist.ClearSelection();
        }




        // Bind Form DropDown
        protected async void BindDataTable()
        {
            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/ManageUser";
                var data = new
                {
                    action = "UserGroupBind", //Action Name
                    userid = 0,
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
                                gridview.DataSource = dt;
                                gridview.DataBind();
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


        // Method Call when GridView Update Button Click.
        protected async void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gridview.DataKeys[e.RowIndex].Value);
            string action = "SELECTBYID";
            using (var httpClient = new HttpClient())
            {
                string TableName = "user_group_assign";
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/OLDExecute";
                var data = new
                {
                    tableName = TableName,
                    action = action,
                    id = id,
                    primaryColumn = "user_group_assignid",
                    primarydatatype = "string",
                    primaryColumnValue = 0,
                    columns = new[]
                    {
                        new
                        {
                           columnName = "string",
                           columnValue = "string",
                           columnDataType = 0
                        }
                    },
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
                                string Groupids = dt.Rows[0]["group_masterid"].ToString();
                                string[] GroupId = Groupids.Split(',');

                                foreach (string Gid in GroupId)
                                {
                                    ListItem listItem = ddl_grouplist.Items.FindByValue(Gid);
                                    if (listItem != null)
                                    {
                                        listItem.Selected = true;
                                    }
                                }
                                ddl_username.SelectedValue = dt.Rows[0]["userid"].ToString();
                                HiddenFieldID.Value = dt.Rows[0]["user_group_assignID"].ToString();
                                ButtonSubmit.Visible = false;
                                ButtonUpdate.Visible = true;
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