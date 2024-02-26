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
    public partial class RoomMaster : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoomTypeDropdown();
                BindRoomCategoryDropdown();
                BindRoomCapacityDropdown();
                BindRoomSizeDropdown();
                BindRoomStatusDropdown();
                BindDatatable();
            }
               
            
        }

        //method on submit of form
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            string action = "INSERT";
            int id = 0;
            SaveData(action, id);
            ButtonSubmit.Visible = true;

        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {

        }

        //method to bind room type
        protected async void BindRoomTypeDropdown()
        {


            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                var data = new
                {
                    action = "DROPDOWNBIND",   //action name
                    searchText = "",
                    filterID = "0",
                    filterID1 = "0",
                    filterID2 = "room_type_master", //table name 
                    filterID3 = "room_type", // column name
                    searchCriteria = "",
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
                                DropDownList_Room_Type.DataSource = dt;
                                DropDownList_Room_Type.DataTextField = dt.Columns[0].ColumnName;
                                DropDownList_Room_Type.DataValueField = dt.Columns[1].ColumnName;
                                DropDownList_Room_Type.DataBind();
                                DropDownList_Room_Type.Items.Insert(0, new ListItem("--Select--", "0"));
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Request failed with status code: " + response.StatusCode + "')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('An error occurred: " + ex.Message + "')</script>", false);
                }
            }
        }


        //method to bind room category 
        protected async void BindRoomCategoryDropdown()
        {


            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                var data = new
                {
                    action = "DROPDOWNBIND",   //action name
                    searchText = "",
                    filterID = "0",
                    filterID1 = "0",
                    filterID2 = "room_category_master", //table name 
                    filterID3 = "room_category", // column name
                    searchCriteria = "",
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
                                DropDownList_Room_Category.DataSource = dt;
                                DropDownList_Room_Category.DataTextField = dt.Columns[0].ColumnName;
                                DropDownList_Room_Category.DataValueField = dt.Columns[1].ColumnName;
                                DropDownList_Room_Category.DataBind();
                                DropDownList_Room_Category.Items.Insert(0, new ListItem("--Select--", "0"));
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Request failed with status code: " + response.StatusCode + "')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('An error occurred: " + ex.Message + "')</script>", false);
                }
            }
        }

        //method to bind room capacity 
        protected async void BindRoomCapacityDropdown()
        {


            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                var data = new
                {
                    action = "DROPDOWNBIND",   //action name
                    searchText = "",
                    filterID = "0",
                    filterID1 = "0",
                    filterID2 = "room_capacity_master", //table name 
                    filterID3 = "room_capacity", // column name
                    searchCriteria = "",
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
                                DropDownList_Room_Capacity.DataSource = dt;
                                DropDownList_Room_Capacity.DataTextField = dt.Columns[0].ColumnName;
                                DropDownList_Room_Capacity.DataValueField = dt.Columns[1].ColumnName;
                                DropDownList_Room_Capacity.DataBind();
                                DropDownList_Room_Capacity.Items.Insert(0, new ListItem("--Select--", "0"));
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Request failed with status code: " + response.StatusCode + "')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('An error occurred: " + ex.Message + "')</script>", false);
                }
            }
        }


        //method to bind room size
        protected async void BindRoomSizeDropdown()
        {


            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                var data = new
                {
                    action = "DROPDOWNBIND",   //action name
                    searchText = "",
                    filterID = "0",
                    filterID1 = "0",
                    filterID2 = "room_size_master", //table name 
                    filterID3 = "room_size", // column name
                    searchCriteria = "",
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
                                DropDownList_Room_Size.DataSource = dt;
                                DropDownList_Room_Size.DataTextField = dt.Columns[0].ColumnName;
                                DropDownList_Room_Size.DataValueField = dt.Columns[1].ColumnName;
                                DropDownList_Room_Size.DataBind();
                                DropDownList_Room_Size.Items.Insert(0, new ListItem("--Select--", "0"));
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Request failed with status code: " + response.StatusCode + "')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('An error occurred: " + ex.Message + "')</script>", false);
                }
            }
        }


        //meethod to bind room status
        protected async void BindRoomStatusDropdown()
        {


            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                var data = new
                {
                    action = "DROPDOWNBIND",   //action name
                    searchText = "",
                    filterID = "0",
                    filterID1 = "0",
                    filterID2 = "room_status_master", //table name 
                    filterID3 = "room_status", // column name
                    searchCriteria = "",
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
                                DropDownList_Room_Status.DataSource = dt;
                                DropDownList_Room_Status.DataTextField = dt.Columns[0].ColumnName;
                                DropDownList_Room_Status.DataValueField = dt.Columns[1].ColumnName;
                                DropDownList_Room_Status.DataBind();
                                DropDownList_Room_Status.Items.Insert(0, new ListItem("--Select--", "0"));
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Request failed with status code: " + response.StatusCode + "')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('An error occurred: " + ex.Message + "')</script>", false);
                }
            }
        }

      
        protected async void SaveData(string Action, int ID)
        {
            //DateTime ararriveDate = DateTime.ParseExact(TextBox_arrivedate.Text, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            //string formararriveDate = ararriveDate.ToString("dd-MM-yyyy");

            //DateTime departdate = DateTime.ParseExact(TextBox_departdate.Text, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            //string formatdepartdate = departdate.ToString("dd-MM-yyyy");

            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
               new ColumnData { columnValue = TextBox_Room_No.Text, columnName = "room_no", columnDataType = "105002" },
              new ColumnData { columnValue = DropDownList_Room_Type.SelectedValue, columnName = "room_type", columnDataType= "105002" },
              new ColumnData { columnValue = DropDownList_Room_Category.SelectedValue, columnName = "room_category", columnDataType = "105002" },
              new ColumnData { columnValue = TextBox_Room_Price.Text, columnName = "room_price", columnDataType = "105002" },
              //new ColumnData { columnValue = formararriveDate, columnName = "arrive_date", columnDataType = "105003" },
              //new ColumnData { columnValue = formatdepartdate, columnName = "depart_date", columnDataType = "105003" },
              new ColumnData { columnValue = DropDownList_Room_Capacity.SelectedValue, columnName = "room_capacity", columnDataType = "105002" },
              new ColumnData { columnValue = DropDownList_Room_Size.SelectedValue, columnName = "room_size", columnDataType = "105002" },
               new ColumnData { columnValue = DropDownList_Room_Status.SelectedValue, columnName = "room_status", columnDataType = "105002" },
              new ColumnData { columnValue = TextBox_Floor_Number.Text, columnName = "floor", columnDataType = "105001" },
            };
            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/OLDExecute";

                var data = new
                {
                    tableName = "room_master_table",
                    action = Action,
                    id = ID,
                    primaryColumn = "string",
                    primarydatatype = "string",
                    primaryColumnValue = 0,
                    columns = textBoxDataList,
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
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>success('" + responseObject.responseMessage + "')</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Request failed with status code: " + response.StatusCode + "')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('An error occurred: " + ex.Message + "')</script>", false);
                }
            }
        }


        protected async void BindDatatable()
        {
            string Action = "SELECT";
            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/OLDExecute";
                var data = new
                {
                    tableName = "room_master_table_copy", //table name 
                    action = Action,
                    id = 0,
                    primaryColumn = "string",
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
                                gridview.DataSource = dt;
                                gridview.DataBind();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Request failed with status code: " + response.StatusCode + "')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('An error occurred: " + ex.Message + "')</script>", false);
                }
            }

        }

        protected void btnChangeStatus_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int id = Convert.ToInt32(gridview.DataKeys[row.RowIndex].Value);
            int StatusValue;
            String action = "UPDATE_STATUS";
            if (btn.Text == "Active")
            {
                StatusValue = 1;
                BtnStatusChange(action,StatusValue,id);
            }
            else
            {
                StatusValue = 0;
                BtnStatusChange(action, StatusValue, id);
            }
        }

        protected async void BtnStatusChange(string action, int StatusValue, int id)
        {
            using (var httpClient = new HttpClient())
            {
                string tableName = Session["tableName"] as string;
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/OLDExecute";
                var data = new
                {
                    tableName = tableName,
                    action = action,
                    id = id,
                    primaryColumn = "string",
                    primarydatatype = "string",
                    primaryColumnValue = StatusValue,
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
                                //BindDataToDataTable(dt);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Request failed with status code: " + response.StatusCode + "')</script>", false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('An error occurred: " + ex.Message + "')</script>", false);
                }
            }
        }
    }
}