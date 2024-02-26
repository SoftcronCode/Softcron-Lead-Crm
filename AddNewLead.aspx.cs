using Humanizer;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class NewLeadCommanForm : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                await BindLeadStatusDropDown();
                BindSourceDropdown();
                BindAssignDropDown();
                BindServiceDropDown();
                BindServiceType();
                await BindDataTable();
            }
        }

        #region All Methods For Bind DropDown

        // Bind Lead Status DropDown
        protected async Task BindLeadStatusDropDown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "lead_status_master";

                var (ErrorMessage, dt) = await commonMethods.SelectAllData(TableName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_leadstatus.DataSource = dt;
                    ddl_leadstatus.DataTextField = "status";  // Use the correct column name
                    ddl_leadstatus.DataValueField = "Lead_status_masterID";  // Use the correct column name
                    ddl_leadstatus.DataBind();
                    ddl_leadstatus.Items.Insert(0, new ListItem("--Select--", "0"));

                    Session["status_data"] = dt;
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

        // Bind Source DropDown
        protected async void BindSourceDropdown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "source_master";
                string ColumnName = "source";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_leadsource.DataSource = dt;
                    ddl_leadsource.DataTextField = dt.Columns[0].ColumnName;
                    ddl_leadsource.DataValueField = dt.Columns[1].ColumnName;
                    ddl_leadsource.DataBind();
                    ddl_leadsource.Items.Insert(0, new ListItem("--Select--", "0"));
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

        // Bind Assign DropDown
        protected async void BindAssignDropDown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "client_user_details";

                var (ErrorMessage, dt) = await commonMethods.SelectAllData(TableName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Add("CombinedField", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string userDisplayName = row["userdisplayname"].ToString();
                        string role = row["userrole"].ToString().ToLower();
                        // Create a custom combined field with userdisplayname and role
                        row["CombinedField"] = $"{userDisplayName} ({role})";
                    }

                    ddl_assign.DataSource = dt;
                    ddl_assign.DataTextField = "CombinedField";  // Use the correct column name
                    ddl_assign.DataValueField = "userid";  // Use the correct column name
                    ddl_assign.DataBind();
                    ddl_assign.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddl_assign.SelectedValue = UserID;

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

        // Bind Service DropDown
        protected async void BindServiceDropDown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "service_master";
                string ColumnName = "service_name";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_service.DataSource = dt;
                    ddl_service.DataTextField = dt.Columns[0].ColumnName;
                    ddl_service.DataValueField = dt.Columns[1].ColumnName;
                    ddl_service.DataBind();
                    ddl_service.Items.Insert(0, new ListItem("--Select--", "0"));
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

        // Bind CustomerType DropDown
        protected async void BindServiceType()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "service_type_master";
                string ColumnName = "service_type";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_serviceType.DataSource = dt;
                    ddl_serviceType.DataTextField = dt.Columns[0].ColumnName;
                    ddl_serviceType.DataValueField = dt.Columns[1].ColumnName;
                    ddl_serviceType.DataBind();
                    ddl_serviceType.Items.Insert(0, new ListItem("--Select--", "0"));
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

        #endregion






        #region All Methods For Add, Update Button And Save data 

        // Method call when Button save Click to insert the lead data.
        protected void ButtonSubmitLead_Click(object sender, EventArgs e)
        {
            string ButtonName = ButtonSubmitLead.Text.ToString();
            GetAllInputFieldsData(ButtonName);
        }

        // method is call when Convert To Customer Button Click on Add New Lead Form.
        protected void ButtonConvertToCustomer_Click(object sender, EventArgs e)
        {
            string ButtonName = ButtonConvertToCustomer.Text.ToString();
            GetAllInputFieldsData(ButtonName);
        }

        // method is use to Get All Input Field Data OF Add New Lead Form.
        protected void GetAllInputFieldsData(string ButtonName)
        {
            string UserID = Request.Cookies["userid"]?.Value;
            string action = "INSERT";
            string tablename = "new_lead";
            int id = 0;
            string Country = HiddenField_country.Value;
            string referedByName = TextBox_referedBy.Text;
            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
              new ColumnData { columnValue = ddl_leadstatus.SelectedValue, columnName = "lead_status_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_leadsource.SelectedValue, columnName = "source_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_assign.SelectedValue, columnName = "assign_user_id", columnDataType= "105002" },
              new ColumnData { columnValue = TextBox_fname.Text, columnName = "first_name", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_lname.Text, columnName = "last_name", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_email.Text, columnName = "email_id", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_phone.Text, columnName = "phone_no", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_companyname.Text, columnName = "company_name", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_website.Text, columnName = "website_url", columnDataType = "105001" },
              new ColumnData { columnValue = ddl_service.SelectedValue, columnName = "service_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_serviceType.SelectedValue, columnName = "service_type_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = Country, columnName = "country", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_desc.Text, columnName = "description", columnDataType = "105001" },
              new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" },
            };
            if (!string.IsNullOrEmpty(referedByName))
            {
                textBoxDataList.Add(new ColumnData { columnValue = referedByName, columnName = "refered_by_name", columnDataType = "105001" });
            }
            SaveData(action, id, textBoxDataList, tablename, ButtonName);
        }

        // Method call when Button Update Click to update the record.
        protected void ButtonUpdateLead_Click(object sender, EventArgs e)
        {
            string ButtonName = ButtonUpdateLead.Text.ToString();
            string UserID = Request.Cookies["userid"]?.Value;

            string action = "UPDATE";
            string tablename = "new_lead";
            int id = int.Parse(hiddenfield_leadid.Value);
            string Country = HiddenField_country.Value;
            string referedByName = TextBox_referedBy.Text;
            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
              new ColumnData { columnValue = ddl_leadstatus.SelectedValue, columnName = "lead_status_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_leadsource.SelectedValue, columnName = "source_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_assign.SelectedValue, columnName = "assign_user_id", columnDataType= "105002" },
              new ColumnData { columnValue = TextBox_fname.Text, columnName = "first_name", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_lname.Text, columnName = "last_name", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_email.Text, columnName = "email_id", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_phone.Text, columnName = "phone_no", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_companyname.Text, columnName = "company_name", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_website.Text, columnName = "website_url", columnDataType = "105001" },
              new ColumnData { columnValue = ddl_service.SelectedValue, columnName = "service_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_serviceType.SelectedValue, columnName = "service_type_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = Country, columnName = "country", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_desc.Text, columnName = "description", columnDataType = "105001" },
              new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" },
            };
            if (!string.IsNullOrEmpty(referedByName))
            {
                textBoxDataList.Add(new ColumnData { columnValue = referedByName, columnName = "refered_by_name", columnDataType = "105001" });
            }
            SaveData(action, id, textBoxDataList, tablename, ButtonName);
            ButtonUpdateLead.Attributes["style"] = "display: none;";
            ButtonSubmitLead.Attributes["style"] = "display: block;";
            ButtonConvertToCustomer.Attributes["style"] = "display: block;";

        }

        // save lead data to database
        protected async void SaveData(string Action, int ID, List<ColumnData> textBoxDataList, string tablename, string ButtonName)
        {
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            using (var httpClient = new HttpClient())
            {

                var apiUrl = Url + "ERP/Setup/OLDExecute";

                var data = new
                {
                    tableName = tablename,
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
                                if (ButtonName != "Convert To Customer")
                                {
                                    await BindDataTable();
                                    ClearInputFields();
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>success('" + responseObject.responseMessage + "')</script>", false);
                                }
                                else
                                {
                                    int leadid = responseObject.responseObject[0].lastInsertedID;
                                    string redirectedUrl = $"/add-new-customer?lid={leadid}";
                                    Response.Redirect(redirectedUrl, false);
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

        #endregion








        #region All Methods For DataTable

        // Bind DataTable
        protected async Task BindDataTable()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Lead/LeadReport";

                var (ErrorMessage, dt) = await commonMethods.BindDataTable(apiUrl, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                    Session["LeadRecord"] = null;
                    Session["LeadRecord"] = dt;
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ex.Message)})</script>", false);
            }
        }

        protected async void gridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the DropDownList in the row
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddl_status_option");

                if (ddlStatus != null)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    if (drv != null)
                    {
                        string leadStatusValue = drv["lead_status_masterid"].ToString();

                        DataTable statusOptions = Session["status_data"] as DataTable;
                        if (statusOptions != null)
                        {
                            ddlStatus.DataSource = statusOptions;
                            ddlStatus.DataTextField = "status";
                            ddlStatus.DataValueField = "Lead_status_masterID";
                            ddlStatus.DataBind();

                            ListItem selectedItemValue = ddlStatus.Items.FindByValue(leadStatusValue);
                            if (selectedItemValue != null)
                            {
                                selectedItemValue.Selected = true;

                                if (selectedItemValue.Value == "5")
                                {
                                    ddlStatus.Enabled = false;
                                    ddlStatus.CssClass = "form-control";
                                }
                            }
                        }
                        else
                        {
                            await BindLeadStatusDropDown();
                            await BindDataTable();
                        }

                    }
                }
            }
        }

        // method call when dropdown ddlStatusOption SelectedIndexChanged
        protected void ddlStatusOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected value from the dropdown
                DropDownList ddlStatus = (DropDownList)sender;
                string selectedValue = ddlStatus.SelectedValue.ToString();

                // Get the GridView row where the dropdown is located
                GridViewRow gridViewRow = (GridViewRow)ddlStatus.NamingContainer;

                // Get the row index and data key value
                int rowIndex = gridViewRow.RowIndex;
                int LeadID = int.Parse(gridview.DataKeys[rowIndex].Value.ToString());

                if (selectedValue == "5")
                {
                    string redirectedUrl = $"/add-new-customer?lid={LeadID}";
                    Response.Redirect(redirectedUrl, false);
                }
                else
                {
                    UpdateGridviewDropDown(selectedValue, LeadID);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ex.Message)})</script>", false);
            }
        }

        // Method to update gridview dropdown value 
        protected void UpdateGridviewDropDown(string selectedValue, int LeadID)
        {
            string action = "UPDATE";
            int id = LeadID;
            string tablename = "new_lead";
            string ButtonName = "gridviewdropdown";

            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
                   new ColumnData { columnValue = selectedValue, columnName = "lead_status_masterid", columnDataType = "105002" },
            };

            SaveData(action, id, textBoxDataList, tablename, ButtonName);
        }

        // GridView RowUpdate Buttom
        protected void Gridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gridview.DataKeys[e.RowIndex].Value);
            SetDataToModalForEdit(id);
        }

        // GridView Row Delete Button
        protected async void Gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gridview.DataKeys[e.RowIndex].Value);
            string tableName = "new_lead";
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

        // Method to set the Data of lead to Add New Lead Form for update the data and open the popup Modal.
        protected async void SetDataToModalForEdit(int id)
        {
            TextBox_companyname.Text = string.Empty;
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string TableName = "new_lead";
            int LeadID = id;

            var (ErrorMessage, dt) = await commonMethods.GetRecordByID(UserID, ipAddress, TableName, LeadID);
            if (dt != null && dt.Rows.Count > 0)
            {
                hiddenfield_leadid.Value = dt.Rows[0]["new_leadid"].ToString();
                ddl_leadstatus.SelectedValue = dt.Rows[0]["lead_status_masterid"].ToString();
                string LeadSource = dt.Rows[0]["source_masterid"].ToString();
                ddl_leadsource.SelectedValue = LeadSource;
                if (!string.IsNullOrEmpty(LeadSource) && LeadSource == "10")
                {
                    TextBox_referedBy.Text = dt.Rows[0]["refered_by_name"].ToString();
                    refered_by.Attributes["style"] = "display: block;";
                }
                else
                {
                    refered_by.Attributes["style"] = "display: none;";
                }

                ddl_assign.SelectedValue = dt.Rows[0]["assign_user_id"].ToString();
                TextBox_fname.Text = dt.Rows[0]["first_name"].ToString();
                TextBox_lname.Text = dt.Rows[0]["last_name"].ToString();
                TextBox_email.Text = dt.Rows[0]["email_id"].ToString();
                TextBox_phone.Text = dt.Rows[0]["phone_no"].ToString();
                TextBox_companyname.Text = dt.Rows[0]["company_name"].ToString();
                TextBox_website.Text = dt.Rows[0]["website_url"].ToString();
                ddl_service.SelectedValue = dt.Rows[0]["service_masterid"].ToString();
                ddl_serviceType.SelectedValue = dt.Rows[0]["service_type_masterid"].ToString();
                string countryValue = dt.Rows[0]["country"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "BindCountry", "$(document).ready(function() { BindCountryDropDown('" + countryValue + "'); });", true);
                TextBox_desc.Text = dt.Rows[0]["description"].ToString();
                ButtonUpdateLead.Attributes["style"] = "display: block;";
                ButtonSubmitLead.Attributes["style"] = "display: none;";
                ButtonConvertToCustomer.Attributes["style"] = "display: none;";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAddNewLeadModal", "$(document).ready(function() { showAddNewLeadModal(); });", true);
            }
        }

        #endregion



        #region  FollowUp Methods

        // GridView Command to View Lead Details and to View FollowUps Records.
        protected async void Button_GridViewCommand(object sender, GridViewCommandEventArgs e)
        {
            // when FollowUp Lead Button is clicked.
            if (e.CommandName == "Followup")
            {
                int leadId = Convert.ToInt32(e.CommandArgument);
                Session["lead_id"] = leadId;
                bindFollowUp(leadId);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showFollowUpModal", "$(document).ready(function() { showFollowUpModal(); });", true);
            }

            // when View Lead Button is clicked.
            if (e.CommandName == "ViewLead")
            {
                try
                {
                    int leadId = Convert.ToInt32(e.CommandArgument);
                    DataTable dt = (DataTable)Session["LeadRecord"];     // Retrieve DataTable from session
                    if (dt != null)
                    {
                        DataRow filteredRow = dt.AsEnumerable().FirstOrDefault(row => Convert.ToInt32(row["new_leadid"]) == leadId);
                        if (filteredRow != null)
                        {
                            DataTable filteredDt = dt.Clone();            // Create a new DataTable with the same structure as the original DataTable                       
                            filteredDt.ImportRow(filteredRow);           // Import the filtered row into the new DataTable
                            if (filteredDt.Rows.Count > 0)
                            {
                                string LeadID = filteredDt.Rows[0]["new_leadid"].ToString();
                                HiddenField_lead_ID.Value = LeadID;
                                lbl_header_name.Text = filteredDt.Rows[0]["first_name"].ToString();
                                lbl_firstName.Text = filteredDt.Rows[0]["first_name"].ToString();
                                lbl_lastName.Text = filteredDt.Rows[0]["last_name"].ToString();
                                lbl_email.Text = filteredDt.Rows[0]["email_id"].ToString();
                                lbl_phone.Text = filteredDt.Rows[0]["phone_no"].ToString();
                                string CompanyName = filteredDt.Rows[0]["company_name"].ToString();
                                if (!string.IsNullOrEmpty(CompanyName))
                                {
                                    lbl_companyName.Text = CompanyName;
                                }
                                else
                                {
                                    lbl_companyName.Text = "N/A";
                                }
                                string website = filteredDt.Rows[0]["website_url"].ToString();
                                if (!string.IsNullOrEmpty(website))
                                {
                                    lbl_website.Text = website;
                                }
                                else
                                {
                                    lbl_website.Text = "N/A";
                                }
                                lbl_serviceName.Text = filteredDt.Rows[0]["service_name"].ToString();
                                lbl_serviceType.Text = filteredDt.Rows[0]["service_type"].ToString();
                                lbl_country.Text = filteredDt.Rows[0]["country"].ToString();
                                lbl_remark.Text = filteredDt.Rows[0]["description"].ToString();
                                lbl_source.Text = filteredDt.Rows[0]["source"].ToString();
                                string sourceMasterID = filteredDt.Rows[0]["source_masterid"].ToString();
                                if (sourceMasterID == "10")
                                {
                                    lbl_referedByName.Text = filteredDt.Rows[0]["refered_by_name"].ToString();
                                }
                                else
                                {
                                    lbl_referedByName.Text = "N/A";
                                }

                                lbl_status.Text = filteredDt.Rows[0]["status"].ToString();
                                lbl_assignName.Text = filteredDt.Rows[0]["assign_to"].ToString();
                                string createdDate = filteredDt.Rows[0]["created_datetime"].ToString();
                                lbl_createdDate.Text = CalculateTimeDifference(createdDate);
                                lbl_createdDate.Attributes["title"] = createdDate;
                                string followupDate = filteredDt.Rows[0]["followup_date"].ToString();
                                if (!string.IsNullOrEmpty(followupDate))
                                {
                                    lbl_followupDate.Text = followupDate;
                                }
                                else
                                {
                                    lbl_followupDate.Text = "No FollowUp Found";
                                }
                                lbl_createdBy.Text = filteredDt.Rows[0]["created_by"].ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showViewLeadModal", "$(document).ready(function() { showViewLeadModal(); });", true);
                                await BindNotesRepeater(LeadID);
                                await BindDocsRepeater(LeadID);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                }
            }
        }

        // Method To Bind FollowUp Data.
        public async void bindFollowUp(int leadid)
        {
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;

            using (var httpClient = new HttpClient())
            {

                var apiUrl = Url + "ERP/Setup/OLDExecute";
                var data = new
                {
                    tableName = "lead_followup",//TABLE NAME 
                    action = "SELECTBYID",  //ACTION
                    id = leadid,
                    primaryColumn = "lead_id",//COLUMN NAME
                    primarydatatype = "string",
                    primaryColumnValue = 0,
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
                                dt.DefaultView.Sort = "followup_date DESC";
                                ChatRepeater.DataSource = dt.DefaultView.ToTable();
                                ChatRepeater.DataBind();
                                NoRecord.Visible = false;
                                followUp_container.Visible = true;
                            }
                            else
                            {
                                dt.Rows.Clear();
                                ChatRepeater.DataSource = dt;
                                ChatRepeater.DataBind();
                                NoRecord.Visible = true;
                                followUp_container.Visible = false;
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


        // Method To Add New FollowUp
        public async void Button_AddFollowUp_Click(object sender, EventArgs e)
        {
            DateTime parsedDateTime = DateTime.ParseExact(followUpDateTime.Text, "yyyy-MM-ddTHH:mm", null);
            // Format the DateTime in the desired format
            string formattedDateTime = parsedDateTime.ToString("dd-MM-yyyy- HH:mm:ss");
            string leadId = Session["lead_id"].ToString();
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string followupText = Remark_txt.Text;
            //string followupDate = followUpDateTime.Text;

            List<ColumnData> textBoxDataList = new List<ColumnData>
                 {
               new ColumnData { columnValue = leadId, columnName = "lead_id", columnDataType = "105002" },
               new ColumnData { columnValue = followupText, columnName = "followup_text", columnDataType = "105001" },
               new ColumnData { columnValue = formattedDateTime, columnName = "followup_date", columnDataType = "105004" },
                new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" }
                 };
            using (var httpClient = new HttpClient())
            {

                var apiUrl = Url + "ERP/Setup/OLDExecute";

                var data = new
                {
                    tableName = "lead_followup",
                    action = "INSERT",
                    id = 0,
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
                                ChatRepeater.DataSource = dt;
                                ChatRepeater.DataBind();
                                Session.Remove("lead_id");
                                Remark_txt.Text = string.Empty;
                                followUpDateTime.Text = string.Empty;
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
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Request failed with status code : " + response.StatusCode)})</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                }
            }
        }

        #endregion







        #region  Extra Methods


        // Method Call when View Lead Popup Modal Edit Button Click
        protected void ButtonEditLead_Click(object sender, EventArgs e)
        {
            int id = int.Parse(HiddenField_lead_ID.Value);
            SetDataToModalForEdit(id);
        }

        // Method call when View Lead PopUp Convert to Customer Button IS Click.
        protected void ButtonCustomer_Click(object sender, EventArgs e)
        {
            int leadid = int.Parse(HiddenField_lead_ID.Value);
            string redirectedUrl = $"/add-new-customer?lid={leadid}";
            Response.Redirect(redirectedUrl, false);
        }

        // Method to Clear All Input Fields.
        protected void ClearInputFields()
        {
            TextBox[] textFields = { TextBox_fname, TextBox_email, TextBox_phone, TextBox_companyname, TextBox_desc };

            foreach (var textField in textFields)
            {
                textField.Text = string.Empty;
            }

            DropDownList[] dropdowns = { ddl_leadsource };

            foreach (var dropdown in dropdowns)
            {
                dropdown.SelectedValue = "0";
            }
        }

        // Method To Call Time Difference
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

                return string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
            return "Unexcepted Error Occured.";
        }


        #endregion





        #region All Notes Mehods

        protected async Task BindNotesRepeater(string id)
        {
            int LeadID = int.Parse(id);
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            var apiUrl = Url + "ERP/Lead/GetNotesByID";
            var data = new
            {
                customerID = 0,
                leadID = LeadID,
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
                using (var httpClient = new HttpClient())
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    lbl_notesCount.Text = dt.Rows.Count.ToString();
                                    NotesRepeater.DataSource = dt;
                                    NotesRepeater.DataBind();
                                    Session["AllLeadnotes_data"] = dt;
                                }
                            }
                            else
                            {
                                lbl_notesCount.Text = "0";
                                NotesRepeater.DataSource = null;
                                NotesRepeater.DataBind();
                                Session["AllLeadnotes_data"] = null;
                            }
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
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
        }

        protected async void Button_addNote_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from form fields
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string action = "INSERT";
                string tablename = "notes_table";
                int id = 0;
                string LeadID = HiddenField_lead_ID.Value;

                List<ColumnData> textBoxDataList = new List<ColumnData>
                 {
                new ColumnData { columnValue = LeadID, columnName = "new_leadid", columnDataType = "105002" },
                new ColumnData { columnValue = TextBox_note.Text, columnName = "note_text", columnDataType = "105001" },
                new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" }
                 };

                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: " + ErrorMessage)})</script>", false);
                    TextBox_note.Text = string.Empty;
                    await BindNotesRepeater(LeadID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showNotesModal", "$(document).ready(function() { showNotesModal(); });", true);
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

        protected async void Button_UpdateNote_Click(object sender, EventArgs e)
        {
            try
            {
                string LeadID = HiddenField_lead_ID.Value;
                int noteID = int.Parse(HiddenField_noteid.Value);
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string action = "UPDATE";
                string tablename = "notes_table";
                int id = noteID;

                List<ColumnData> textBoxDataList = new List<ColumnData>
                 {
                new ColumnData { columnValue = TextBox_note.Text, columnName = "note_text", columnDataType = "105001" },
                new ColumnData { columnValue = UserID, columnName = "modified_by", columnDataType = "105002" }
                 };

                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success : " + ErrorMessage)})</script>", false);
                    TextBox_note.Text = string.Empty;
                    Button_UpdateNote.Visible = false;
                    Button_addNote.Visible = true;
                    await BindNotesRepeater(LeadID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showNotesModal", "$(document).ready(function() { showNotesModal(); });", true);
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

        protected async void NotesRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int noteId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    DataTable dt = (DataTable)Session["AllLeadnotes_data"];     // Retrieve DataTable from session
                    if (dt != null)
                    {
                        DataRow filteredRow = dt.AsEnumerable().FirstOrDefault(row => Convert.ToInt32(row["notes_tableid"]) == noteId);
                        if (filteredRow != null)
                        {
                            DataTable filteredDt = dt.Clone();            // Create a new DataTable with the same structure as the original DataTable                       
                            filteredDt.ImportRow(filteredRow);           // Import the filtered row into the new DataTable
                            if (filteredDt.Rows.Count > 0)
                            {
                                HiddenField_noteid.Value = filteredDt.Rows[0]["notes_tableid"].ToString();
                                TextBox_note.Text = filteredDt.Rows[0]["note_text"].ToString();
                                Button_UpdateNote.Visible = true;
                                Button_addNote.Visible = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showNotesModal", "$(document).ready(function() { showNotesModal(); });", true);
            }

            else if (e.CommandName == "Delete")
            {
                int noteId = Convert.ToInt32(e.CommandArgument);
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string tablename = "notes_table";
                var (Success, ErrorMessage) = await commonMethods.DeleteById(noteId, tablename, UserID, ipAddress);
                if (Success)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success : " + ErrorMessage)})</script>", false);
                    string LeadID = HiddenField_lead_ID.Value;
                    await BindNotesRepeater(LeadID);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showNotesModal", "$(document).ready(function() { showNotesModal(); });", true);
            }
        }

        #endregion



        #region  File Upload Methods

        protected async Task BindDocsRepeater(string id)
        {
            int LeadID = int.Parse(id);
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            var apiUrl = Url + "ERP/Lead/GetDocsByID";
            var data = new
            {
                customerID = LeadID,
                leadID = 0,
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
                using (var httpClient = new HttpClient())
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    lbl_docsCount.Text = dt.Rows.Count.ToString();
                                    DocsRepeater.DataSource = dt;
                                    DocsRepeater.DataBind();
                                }
                            }
                            else
                            {
                                lbl_docsCount.Text = "0";
                                DocsRepeater.DataSource = null;
                                DocsRepeater.DataBind();
                            }
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
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
        }

        protected async void UploadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload.HasFile)
                {
                    string filename = Path.GetFileName(FileUpload.PostedFile.FileName);
                    string contentType = FileUpload.PostedFile.ContentType;
                    string fileExtension = Path.GetExtension(FileUpload.FileName.ToString());

                    string[] allowedExtensions = { ".png", ".jpeg", ".xlsx", ".docx", ".pdf" };
                    if (Array.IndexOf(allowedExtensions, fileExtension) != -1)
                    {
                        string targetDirectory = Server.MapPath("~/assets/docs/lead");
                        string uniqueFilename = GenerateUniqueFilename(targetDirectory, Path.GetFileNameWithoutExtension(filename), fileExtension);


                        // Retrieve values from form fields
                        string UserID = Request.Cookies["userid"]?.Value;
                        string ipAddress = Request.UserHostAddress;
                        string action = "INSERT";
                        int id = 0;
                        string tablename = "docs_table";
                        string LeadID = HiddenField_lead_ID.Value;

                        List<ColumnData> textBoxDataList = new List<ColumnData>
                        {
                           new ColumnData { columnValue = LeadID, columnName = "customer_detailsid", columnDataType = "105002" },
                           new ColumnData { columnValue = uniqueFilename, columnName = "filename", columnDataType = "105001" },
                           new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" }
                        };

                        var (success, errorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                        if (success)
                        {
                            try
                            {
                                string filePath = Path.Combine(targetDirectory, uniqueFilename);
                                FileUpload.SaveAs(filePath);
                                await BindDocsRepeater(LeadID);
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: File Upload Successfully!")})</script>", false);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDocsModal", "$(document).ready(function() { showDocsModal(); });", true);
                            }
                            catch (Exception ex)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDocsModal", "$(document).ready(function() { showDocsModal(); });", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + errorMessage)})</script>", false);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showDocsModal", "$(document).ready(function() { showDocsModal(); });", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", $"<script>info({JsonConvert.SerializeObject("Info: Invalid file type. Allowed types are: .png, .jpeg, .xlsx, .docx, .pdf")})</script>", false);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showDocsModal", "$(document).ready(function() { showDocsModal(); });", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", $"<script>info({JsonConvert.SerializeObject("Info: Please select a file to upload.")})</script>", false);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showDocsModal", "$(document).ready(function() { showDocsModal(); });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDocsModal", "$(document).ready(function() { showDocsModal(); });", true);
            }
        }

        private string GenerateUniqueFilename(string targetDirectory, string baseFilename, string fileExtension)
        {
            string uniqueFilename = $"{baseFilename}{fileExtension}";
            int counter = 0;

            while (File.Exists(Path.Combine(targetDirectory, uniqueFilename)))
            {
                counter++;
                uniqueFilename = $"{baseFilename}({counter:D1}){fileExtension}";
            }

            return uniqueFilename;
        }

        protected async void DocsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                int docsId = Convert.ToInt32(e.CommandArgument);
                string tablename = "docs_table";
                var (success, ErrorMessage) = await commonMethods.DeleteById(docsId, tablename, UserID, ipAddress);
                if (success)
                {
                    string LeadID = HiddenField_lead_ID.Value;
                    await BindDocsRepeater(LeadID);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success : " + ErrorMessage)})</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDocsModal", "$(document).ready(function() { showDocsModal(); });", true);
            }
        }






        #endregion

    }

    public class ProductClass
    {
        public string Product_class_masterID { get; set; }
        public string is_active { get; set; }
        public string Product_class { get; set; }
        public string Product_name_masterid { get; set; }
        public string Product_name { get; set; }
    }
}
