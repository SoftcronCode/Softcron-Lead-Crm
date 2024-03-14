using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class AssignFormToGroup : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGroupDropdown();
                BindFormDropdown();
                BindDataTable();
            }
        }

        // Bind Group DropDown
        protected async void BindGroupDropdown()
        {
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string TableName = "group_master";
            string ColumnName = "group_name";

            var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl_groupname.DataSource = dt;
                ddl_groupname.DataTextField = dt.Columns[0].ColumnName;
                ddl_groupname.DataValueField = dt.Columns[1].ColumnName;
                ddl_groupname.DataBind();
                ddl_groupname.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
            }
        }

        // Bind Form DropDown
        protected async void BindFormDropdown()
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
                    filterID2 = "ExtensionTableID", //Column Name 
                    filterID3 = "AliasName", // column name
                    searchCriteria = "table_master_extension_config", // Table Name
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
                                ddlTagList.DataSource = dt;
                                ddlTagList.DataValueField = dt.Columns[0].ColumnName;
                                ddlTagList.DataTextField = dt.Columns[1].ColumnName;
                                ddlTagList.DataBind();
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

        // Method for Submit Buttom
        protected async void ButtonSubmit_Click(object sender, EventArgs e)
        {
            string totalFormId = null;

            if (ddlTagList.Items.Count > 0)
            {
                string FormId = null;
                for (int i = 0; i < ddlTagList.Items.Count; i++)
                {
                    if (ddlTagList.Items[i].Selected)
                    {
                        string selectedItemId = ddlTagList.Items[i].Value + ",";
                        FormId = FormId + selectedItemId;
                    }
                }

                if (!string.IsNullOrEmpty(FormId))
                {
                    totalFormId = FormId.TrimEnd(',');
                }
            }


            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string action = "INSERT";
            string tablename = "form_assign_to_group";
            int id = 0;

            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
               new ColumnData { columnValue = ddl_groupname.SelectedValue, columnName = "group_masterid", columnDataType = "105002" },
               new ColumnData { columnValue = totalFormId, columnName = "form_id", columnDataType = "105001" }
            };

            var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
            if (Success)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: " + ErrorMessage)})</script>", false);
                ButtonSubmit.Visible = true;
                ButtonUpdate.Visible = false;
                ddl_groupname.SelectedIndex = 0;
                ddlTagList.ClearSelection();
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

            if (ddlTagList.Items.Count > 0)
            {
                string FormId = null;
                for (int i = 0; i < ddlTagList.Items.Count; i++)
                {
                    if (ddlTagList.Items[i].Selected)
                    {
                        string selectedItemId = ddlTagList.Items[i].Value + ",";
                        FormId = FormId + selectedItemId;
                    }
                }

                if (!string.IsNullOrEmpty(FormId))
                {
                    totalFormId = FormId.TrimEnd(',');
                }
            }


            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string action = "UPDATE";
            string tablename = "form_assign_to_group";
            int id = int.Parse(HiddenFieldID.Value);

            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
               new ColumnData { columnValue = ddl_groupname.SelectedValue, columnName = "group_masterid", columnDataType = "105002" },
               new ColumnData { columnValue = totalFormId, columnName = "form_id", columnDataType = "105001" }
            };

            var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
            if (Success)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: " + ErrorMessage)})</script>", false);
                ButtonUpdate.Visible = false;
                ButtonSubmit.Visible = true;
                ddl_groupname.SelectedIndex = 0;
                ddlTagList.ClearSelection();
                BindDataTable();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            ddl_groupname.SelectedIndex = 0;
            ddlTagList.ClearSelection();
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
                    action = "GroupFormBind", //Action Name
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
                string TableName = "form_assign_to_group";
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/OLDExecute";
                var data = new
                {
                    tableName = TableName,
                    action = action,
                    id = id,
                    primaryColumn = "form_assign_to_groupID",   
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
                                string formids = dt.Rows[0]["form_id"].ToString();
                                string[] formId = formids.Split(',');

                                foreach (string fid in formId)
                                {
                                    ListItem listItem = ddlTagList.Items.FindByValue(fid);
                                    if (listItem != null)
                                    {
                                        listItem.Selected = true;
                                    }
                                }
                                ddl_groupname.SelectedValue = dt.Rows[0]["group_masterid"].ToString();
                                HiddenFieldID.Value = dt.Rows[0]["form_assign_to_groupID"].ToString();
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