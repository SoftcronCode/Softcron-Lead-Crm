using Antlr.Runtime.Misc;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace DSERP_Client_UI
{
    public partial class AddNewCustomer : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["openModal"] == "true")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAddNewLeadModal", "$(document).ready(function() { showAddNewLeadModal(); });", true);
            }

            if (Session["SuccessMessage"] != null)
            {
                string successMessage = Session["SuccessMessage"].ToString();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success('{successMessage}')</script>", false);
                Session.Remove("SuccessMessage");
            }

            if (!IsPostBack)
            {
                await BindGenderDropdown();
                await BindDataTable();


                //convert lead to customer 
                if (!string.IsNullOrEmpty(Request.QueryString["lid"]))
                {
                    // Retrieve the new_leadid from the query string
                    int new_leadid = int.Parse(Request.QueryString["lid"]);
                    string UserID = Request.Cookies["userid"]?.Value;
                    string ipAddress = Request.UserHostAddress;
                    string TableName = "new_lead";

                    var (ErrorMessage, dt) = await commonMethods.GetRecordByID(UserID, ipAddress, TableName, new_leadid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Found a matching row
                        DataRow matchingRow = dt.Rows[0];

                        hiddenField_leadid.Value = matchingRow["new_leadid"].ToString();
                        string countryValue = matchingRow["country"].ToString();
                        string stateValue = "";
                        string cityValue = "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "BindCountry", "$(document).ready(function() { BindCountryDropDown('" + countryValue + "', '" + stateValue + "', '" + cityValue + "'); });", true);
                        TextBox_firstName.Text = matchingRow["first_name"].ToString();
                        TextBox_lastName.Text = matchingRow["last_name"].ToString();
                        TextBox_email.Text = matchingRow["email_id"].ToString();
                        TextBox_phone.Text = matchingRow["phone_no"].ToString();
                        TextBox_CompanyName.Text = matchingRow["company_name"].ToString();
                        TextBox_websiteUrl.Text = matchingRow["website_url"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showAddNewLeadModal", "$(document).ready(function() { showAddNewLeadModal(); });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
                    }
                }
            }
        }



        #region All Method To Bind DropDown

        //bind gender
        protected async Task BindGenderDropdown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "gender_master";
                string ColumnName = "gender";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_gender.DataSource = dt;
                    ddl_gender.DataTextField = "gender";
                    ddl_gender.DataValueField = "Gender_masterID";
                    ddl_gender.DataBind();
                    ddl_gender.Items.Insert(0, new ListItem("--Select--", "0"));
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







        #region Add New Customer PoPUp Methods

        // submit button
        protected async void ButtonSubmitCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the date using the specified format "yyyy-MM-dd"
                DateTime dateValue = DateTime.ParseExact(TextBox_dob.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                // Convert the date back to the desired string format "dd-MM-yyyy"
                string formattedDateOfBirth = dateValue.ToString("dd-MM-yyyy");


                // Retrieve values from form fields
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string action = "INSERT";
                string tablename = "customer_details";
                int id = 0;
                string Country = HiddenField_country.Value;
                string State = HiddenField_state.Value;
                string City = HiddenField_city.Value;
                string leadid = hiddenField_leadid.Value;

                List<ColumnData> textBoxDataList = new List<ColumnData>
            {
                new ColumnData { columnValue = TextBox_firstName.Text, columnName = "first_name", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_lastName.Text, columnName = "last_name", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_email.Text, columnName = "email_id", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_phone.Text, columnName = "phone_no", columnDataType = "105001" },
                new ColumnData { columnValue = ddl_gender.SelectedValue, columnName = "gender_masterid", columnDataType = "105002" },
                new ColumnData { columnValue = formattedDateOfBirth, columnName = "date_of_birth", columnDataType = "105003" },
                new ColumnData { columnValue = TextBox_CompanyName.Text, columnName = "company_name", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_websiteUrl.Text, columnName = "website_url", columnDataType = "105001" },
                new ColumnData { columnValue = Country, columnName = "country", columnDataType = "105001" },
                new ColumnData { columnValue = State, columnName = "state", columnDataType = "105001" },
                new ColumnData { columnValue = City, columnName = "city", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_pin.Text, columnName = "pin_code", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_address.Text, columnName = "address", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_gst.Text, columnName = "gst_number", columnDataType = "105001" },
                new ColumnData { columnValue = ddl_currency.SelectedValue, columnName = "currency", columnDataType = "105001" },
                new ColumnData { columnValue = Text_password.Text, columnName = "password", columnDataType = "105001" },
                new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" }
            };
                if (!string.IsNullOrEmpty(leadid))
                {
                    textBoxDataList.Add(new ColumnData { columnValue = leadid, columnName = "new_leadid", columnDataType = "105002" });
                }

                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "clearInputFields", "$(document).ready(function() { clearInputFields(); });", true);

                    string new_leadid = Request.QueryString["lid"];
                    if (!string.IsNullOrEmpty(new_leadid))
                    {
                        string Button = "Convert";
                        UpdateLeadStatus(new_leadid, Button);
                    }
                    else
                    {
                        Session["SuccessMessage"] = ErrorMessage;
                        Response.Redirect("/add-new-customer");
                    }
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

        //update customer data button
        protected async void ButtonUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string action = "UPDATE";
                string tablename = "customer_details";
                int id = int.Parse(hiddenField_customer_id.Value);
                // Parse the date using the specified format "yyyy-MM-dd"
                DateTime dateValue = DateTime.ParseExact(TextBox_dob.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Convert the date back to the desired string format "dd-MM-yyyy"
                string formattedDateOfBirth = dateValue.ToString("dd-MM-yyyy");

                string Country = HiddenField_country.Value;
                string State = HiddenField_state.Value;
                string City = HiddenField_city.Value;


                List<ColumnData> textBoxDataList = new List<ColumnData>
            {
                new ColumnData { columnValue = TextBox_firstName.Text, columnName = "first_name", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_lastName.Text, columnName = "last_name", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_email.Text, columnName = "email_id", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_phone.Text, columnName = "phone_no", columnDataType = "105001" },
                new ColumnData { columnValue = ddl_gender.SelectedValue, columnName = "gender_masterid", columnDataType = "105002" },
                new ColumnData { columnValue = formattedDateOfBirth, columnName = "date_of_birth", columnDataType = "105003" },
                new ColumnData { columnValue = TextBox_CompanyName.Text, columnName = "company_name", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_websiteUrl.Text, columnName = "website_url", columnDataType = "105001" },
                new ColumnData { columnValue = Country, columnName = "country", columnDataType = "105001" },
                new ColumnData { columnValue = State, columnName = "state", columnDataType = "105001" },
                new ColumnData { columnValue = City, columnName = "city", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_pin.Text, columnName = "pin_code", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_address.Text, columnName = "address", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_gst.Text, columnName = "gst_number", columnDataType = "105001" },
                new ColumnData { columnValue = ddl_currency.SelectedValue, columnName = "currency", columnDataType = "105001" },
                new ColumnData { columnValue = Text_password.Text, columnName = "password", columnDataType = "105001" },
                new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" }

            };
                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "clearInputFields", "$(document).ready(function() { clearInputFields(); });", true);
                    ButtonUpdateCustomer.Attributes["style"] = "display: none;";
                    ButtonSubmitCustomer.Attributes["style"] = "display: block;";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: " + ErrorMessage)})</script>", false);
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

        private async void UpdateLeadStatus(string leadId, string button)
        {

            //to handel if new customer is added via lead form directly and here to change the lead status to converted
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string action = "UPDATE";
                string tablename = "new_lead";
                int id = int.Parse(leadId);
                string setoption = "0";

                // Check the button text and update setoption accordingly
                if (button.Equals("delete", StringComparison.OrdinalIgnoreCase))
                {
                    setoption = "6";
                }
                else if (button.Equals("convert", StringComparison.OrdinalIgnoreCase))
                {
                    setoption = "5";
                }

                List<ColumnData> textBoxDataList = new List<ColumnData>
                 {
                   new ColumnData { columnValue = setoption, columnName = "lead_status_masterid", columnDataType = "105002" },
                 };

                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject(ErrorMessage)})</script>", false);
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




        #region DataTable Operations Method

        // Bind DataTable
        protected async Task BindDataTable()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Customer/CustomerReporting";

                var (ErrorMessage, dt) = await commonMethods.BindDataTable(apiUrl, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                    Session.Remove("customerRecord");
                    Session["customerRecord"] = dt;
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

        // GridView RowUpdate  gridview button 
        protected async void Gridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gridview.DataKeys[e.RowIndex].Value);
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string tablename = "customer_details";

            var (ErrorMessage, dt) = await commonMethods.GetRecordByID(UserID, ipAddress, tablename, id);


            if (dt != null && dt.Rows.Count > 0)
            {
                hiddenField_customer_id.Value = dt.Rows[0]["customer_detailsID"].ToString();
                TextBox_firstName.Text = dt.Rows[0]["first_name"].ToString();
                TextBox_lastName.Text = dt.Rows[0]["last_name"].ToString();
                TextBox_email.Text = dt.Rows[0]["email_id"].ToString();
                TextBox_phone.Text = dt.Rows[0]["phone_no"].ToString();
                ddl_gender.SelectedValue = dt.Rows[0]["gender_masterID"].ToString();
                var dateOfBirth = (DateTime)dt.Rows[0]["date_of_birth"];
                DateTime desiredDate = new DateTime(dateOfBirth.Year, dateOfBirth.Month, dateOfBirth.Day);
                TextBox_dob.Text = desiredDate.ToString("yyyy-MM-dd");
                TextBox_CompanyName.Text = dt.Rows[0]["company_name"].ToString();
                TextBox_websiteUrl.Text = dt.Rows[0]["website_url"].ToString();
                string countryValue = dt.Rows[0]["country"].ToString();
                string stateValue = dt.Rows[0]["state"].ToString();
                string cityValue = dt.Rows[0]["city"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "BindCountry", "$(document).ready(function() { BindCountryDropDown('" + countryValue + "', '" + stateValue + "', '" + cityValue + "'); });", true);
                TextBox_pin.Text = dt.Rows[0]["pin_code"].ToString();
                TextBox_address.Text = dt.Rows[0]["address"].ToString();
                TextBox_gst.Text = dt.Rows[0]["gst_number"].ToString();
                Text_password.Text = dt.Rows[0]["password"].ToString();
                ddl_currency.SelectedValue = dt.Rows[0]["currency"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAddNewLeadModal", "$(document).ready(function() { showAddNewLeadModal(); });", true);
                ButtonUpdateCustomer.Attributes["style"] = "display: block;";
                ButtonSubmitCustomer.Attributes["style"] = "display: none;";

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
            }

        }

        protected async void gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gridview.DataKeys[e.RowIndex].Value);
                int rowIndex = Convert.ToInt32(e.RowIndex);
                string new_leadid = gridview.DataKeys[rowIndex]["new_leadid"].ToString();
                string tableName = "customer_details";
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;

                var (Success, ErrorMessage) = await commonMethods.DeleteById(id, tableName, UserID, ipAddress);
                if (Success)
                {
                    if (!string.IsNullOrEmpty(new_leadid))
                    {
                        string ButtonName = "delete";
                        UpdateLeadStatus(new_leadid, ButtonName);
                        await BindDataTable();
                    }
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

        //view customer details
        protected async void Button_GridViewCommand(object sender, GridViewCommandEventArgs e)
        {
            // when View Lead Button is clicked.
            if (e.CommandName == "ViewCustomer")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    DataTable dt = (DataTable)Session["customerRecord"];     // Retrieve DataTable from session
                    if (dt != null)
                    {
                        DataRow filteredRow = dt.AsEnumerable().FirstOrDefault(row => Convert.ToInt32(row["customer_detailsID"]) == id);
                        if (filteredRow != null)
                        {
                            DataTable filteredDt = dt.Clone();            // Create a new DataTable with the same structure as the original DataTable                       
                            filteredDt.ImportRow(filteredRow);           // Import the filtered row into the new DataTable
                            if (filteredDt.Rows.Count > 0)
                            {
                                string customerID = filteredDt.Rows[0]["customer_detailsID"].ToString();
                                HiddenField_customerID.Value = customerID;
                                lbl_header_name.Text = filteredDt.Rows[0]["first_name"].ToString();
                                lbl_firstName.Text = filteredDt.Rows[0]["first_name"].ToString();
                                lbl_lastName.Text = filteredDt.Rows[0]["last_name"].ToString();
                                lbl_emailID.Text = filteredDt.Rows[0]["email_id"].ToString();
                                lbl_phoneNo.Text = filteredDt.Rows[0]["phone_no"].ToString();
                                lbl_gender.Text = filteredDt.Rows[0]["gender"].ToString();
                                lbl_dateOfBirth.Text = filteredDt.Rows[0]["date_of_birth"].ToString();
                                lbl_companyName.Text = filteredDt.Rows[0]["company_name"].ToString();
                                lbl_Website.Text = filteredDt.Rows[0]["website_url"].ToString();
                                lbl_country.Text = filteredDt.Rows[0]["country"].ToString();
                                lbl_state.Text = filteredDt.Rows[0]["state"].ToString();
                                lbl_city.Text = filteredDt.Rows[0]["city"].ToString();
                                lbl_pinCode.Text = filteredDt.Rows[0]["pin_code"].ToString();
                                lbl_address.Text = filteredDt.Rows[0]["address"].ToString();
                                lbl_gst.Text = filteredDt.Rows[0]["gst_number"].ToString();
                                lbl_currency.Text = filteredDt.Rows[0]["currency"].ToString();
                                lbl_password.Text = filteredDt.Rows[0]["password"].ToString();
                                string createdDate = filteredDt.Rows[0]["created_date"].ToString();
                                lbl_created_date.Text = CalculateTimeDifference(createdDate);
                                lbl_created_date.Attributes["title"] = createdDate;
                                lbl_created_by.Text = filteredDt.Rows[0]["created_by"].ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showViewLeadModal", "$(document).ready(function() { showViewLeadModal(); });", true);
                                await BindNotesRepeater(customerID);
                                await BindDocsRepeater(customerID);
                            }
                            else
                            {

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                }
            }

            //to redirect to the sales page 
            if (e.CommandName == "AddSales")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"/sales?cid={id}");
            }
        }

        #endregion






        #region Other Methods

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
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
            return string.Empty;
        }


        #endregion





        #region All Notes Mehods

        protected async Task BindNotesRepeater(string id)
        {
            try
            {
                int CustomerID = int.Parse(id);
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Lead/GetNotesByID";
                var data = new
                {
                    customerID = CustomerID,
                    leadID = 0,
                    objCommon = new
                    {
                        insertedUserID = UserID,
                        insertedIPAddress = ipAddress,
                        dateShort = "dd-MM-yyyy",
                        dateLong = "dd-MM-yyyy- HH:mm:ss"
                    }
                };

                var (ErrorMessage, dt) = await commonMethods.CommonMethod(apiUrl, data);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lbl_notesCount.Text = dt.Rows.Count.ToString();
                    NotesRepeater.DataSource = dt;
                    NotesRepeater.DataBind();
                    Session["Allnotes_data"] = dt;
                }
                else
                {
                    lbl_notesCount.Text = "0";
                    NotesRepeater.DataSource = null;
                    NotesRepeater.DataBind();
                    Session["Allnotes_data"] = null;
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
                string customerID = HiddenField_customerID.Value;

                List<ColumnData> textBoxDataList = new List<ColumnData>
                 {
                new ColumnData { columnValue = customerID, columnName = "customer_detailsid", columnDataType = "105002" },
                new ColumnData { columnValue = TextBox_note.Text, columnName = "note_text", columnDataType = "105001" },
                new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" }
                 };

                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: " + ErrorMessage)})</script>", false);
                    TextBox_note.Text = string.Empty;
                    await BindNotesRepeater(customerID);
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
                string customerID = HiddenField_customerID.Value;
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
                    await BindNotesRepeater(customerID);
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
                    DataTable dt = (DataTable)Session["Allnotes_data"];     // Retrieve DataTable from session
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
                    string customerID = HiddenField_customerID.Value;
                    await BindNotesRepeater(customerID);
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
            try
            {
                int CustomerID = int.Parse(id);
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Lead/GetDocsByID";
                var data = new
                {
                    customerID = CustomerID,
                    leadID = 0,
                    objCommon = new
                    {
                        insertedUserID = UserID,
                        insertedIPAddress = ipAddress,
                        dateShort = "dd-MM-yyyy",
                        dateLong = "dd-MM-yyyy- HH:mm:ss"
                    }
                };
                var (ErrorMessage, dt) = await commonMethods.CommonMethod(apiUrl, data);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lbl_docsCount.Text = dt.Rows.Count.ToString();
                    DocsRepeater.DataSource = dt;
                    DocsRepeater.DataBind();
                }
                else
                {
                    lbl_docsCount.Text = "0";
                    DocsRepeater.DataSource = null;
                    DocsRepeater.DataBind();
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
                        string targetDirectory = Server.MapPath("~/assets/docs/customer");
                        string uniqueFilename = GenerateUniqueFilename(targetDirectory, Path.GetFileNameWithoutExtension(filename), fileExtension);


                        // Retrieve values from form fields
                        string UserID = Request.Cookies["userid"]?.Value;
                        string ipAddress = Request.UserHostAddress;
                        string action = "INSERT";
                        int id = 0;
                        string tablename = "docs_table";
                        string customerID = HiddenField_customerID.Value;

                        List<ColumnData> textBoxDataList = new List<ColumnData>
                        {
                           new ColumnData { columnValue = customerID, columnName = "customer_detailsid", columnDataType = "105002" },
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
                                await BindDocsRepeater(customerID);
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
                    string customerID = HiddenField_customerID.Value;
                    await BindDocsRepeater(customerID);
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
}


