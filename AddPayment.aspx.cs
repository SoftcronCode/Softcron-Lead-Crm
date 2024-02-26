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
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class AddPayment : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPaymentModeDropDown();
                BindBankNameDropDown();
                await LoadCustomerData();


                // Method to Get the Customer Detail For Add sales and this method call when id pass through the customer Page.
                if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
                {
                    // Retrieve the customer id from the query string
                    string customer_id = Request.QueryString["cid"];
                    var (ErrorMessage, dt) = await GetCustomerSalesRecordByID(customer_id);
                    if (dt == null)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
                    }
                    else
                    {
                        SetCustomerData(dt);
                    }
                }
            }
        }



        #region  All Methods to Bind Data On Page Load.


        // method to load all customer Data
        protected async Task LoadCustomerData()
        {
            try
            {
                string TableName = "customer_details";
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;

                var (ErrorMessage, dt) = await commonMethods.SelectAllData(TableName, UserID, ipAddress);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Session["Customer_Details"] = dt;
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


        // Method To Bind Payment Mode DropDown
        protected async void BindPaymentModeDropDown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "pay_mode_master";
                string ColumnName = "payment_mode";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_paymentMode.DataSource = dt;
                    ddl_paymentMode.DataTextField = dt.Columns[0].ColumnName;
                    ddl_paymentMode.DataValueField = dt.Columns[1].ColumnName;
                    ddl_paymentMode.DataBind();
                    ddl_paymentMode.Items.Insert(0, new ListItem("--Select--", "0"));
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


        // Method To Bind Bank Name DropDown
        protected async void BindBankNameDropDown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "bank_master";
                string ColumnName = "bank_name";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_bankname.DataSource = dt;
                    ddl_bankname.DataTextField = dt.Columns[0].ColumnName;
                    ddl_bankname.DataValueField = dt.Columns[1].ColumnName;
                    ddl_bankname.DataBind();
                    ddl_bankname.Items.Insert(0, new ListItem("--Select--", "0"));
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





        #region Customer Search Button Methods.

        // Method call when search button is clicked.
        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            customer_details.Attributes["style"] = "display: none;";

            try
            {
                string searchValue = txtSearch.Value.Trim();
                if (string.IsNullOrEmpty(searchValue))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning", "<script>warning('Warning: Please Enter a valid search input.')</script>", false);
                }
                else
                {
                    if (Session["Customer_Details"] != null)
                    {
                        DataTable dt = (DataTable)Session["Customer_Details"];
                        if (dt.Rows.Count > 0)
                        {
                            DataRow[] matchingRows = dt.Select($"first_name LIKE '%{searchValue}%' OR " +
                                                             $"phone_no LIKE '%{searchValue}%' OR " +
                                                             $"email_id LIKE '%{searchValue}%'");

                            if (matchingRows.Length == 1)
                            {
                                string customerID = matchingRows[0]["customer_detailsID"].ToString();
                                var (ErrorMessage, mathingRowDt) = await GetCustomerSalesRecordByID(customerID);
                                if (mathingRowDt != null)
                                {
                                    SetCustomerData(mathingRowDt);
                                    txtSearch.Value = string.Empty;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
                                }
                            }
                            else if (matchingRows.Length > 1)
                            {
                                DataTable filteredDt = matchingRows.CopyToDataTable();
                                gridview.DataSource = filteredDt;
                                gridview.DataBind();
                                Session["filteredCusDt"] = filteredDt;
                                txtSearch.Value = string.Empty;
                                customer_data.Attributes["style"] = "display: block;";
                            }
                            else
                            {
                                gridview.DataSource = null;
                                gridview.DataBind();
                                customer_data.Attributes["style"] = "display: block;";
                            }
                        }
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }

        // Method call when user click on the select button in Datatable and datatable is show when search button click. 
        protected async void OnCustomerSelectClick(object sender, EventArgs e)
        {
            try
            {
                customer_data.Attributes["style"] = "display: none;";
                LinkButton name_click = (LinkButton)sender;
                string customerID = name_click.CommandArgument;
                //  string customerID = e.CommandArgument.ToString();
                var (ErrorMessage, dt) = await GetCustomerSalesRecordByID(customerID);
                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
                }
                else
                {
                    SetCustomerData(dt);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }

        // Method to set Customer Details to label to show.
        protected void SetCustomerData(DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    customer_details.Attributes["style"] = "display: block;";

                    hiddenField_customerid.Value = dt.Rows[0]["customer_detailsID"].ToString();
                    lbl_customerName.Text = dt.Rows[0]["customer_name"].ToString();
                    lbl_customerEmail.Text = dt.Rows[0]["email_id"].ToString();
                    lbl_customerPhone.Text = dt.Rows[0]["phone_no"].ToString();
                    lbl_country.Text = dt.Rows[0]["country"].ToString();
                    string totalAmount = dt.Rows[0]["total_amount"].ToString();
                    string totalQty = dt.Rows[0]["total_qty"].ToString();
                    string totalPayment = dt.Rows[0]["total_payment"].ToString();
                    if (string.IsNullOrEmpty(totalPayment))
                    {
                        totalPayment = "0.00";
                    }

                    decimal totalAmountNumeric = decimal.Parse(totalAmount);
                    decimal totalPaymentNumeric = decimal.Parse(totalPayment);

                    lbl_salesQty.Text = totalQty;
                    lbl_totalSalesAmount.Text = totalAmountNumeric.ToString("F2");
                    lbl_totalAmountReceived.Text = totalPaymentNumeric.ToString("F2");
                    decimal totalPending = totalAmountNumeric - totalPaymentNumeric;
                    string totalPendingString = Math.Abs(totalPending).ToString("F2");
                    if (totalPending > 0)
                    {
                        lbl_totalAmountPending.Text = totalPendingString;
                        lbl_grossAmount.Text = "0.00";
                    }
                    else
                    {
                        lbl_totalAmountPending.Text = "0.00";
                        lbl_grossAmount.Text = "+ " + totalPendingString;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: No Record Found!")})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }

        // Method to get customer Sales Record by ID.
        protected async Task<(string ErrorMessage, DataTable dt)> GetCustomerSalesRecordByID(String id)
        {
            using (var httpClient = new HttpClient())
            {

                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Customer/CustomerSalesRecord";
                var data = new
                {
                    customer_id = id,
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
                                return (null, dt);
                            }
                            else
                            {
                                return ("No Record Found", null);
                            }
                        }
                        else
                        {
                            return (responseObject.responseMessage, null);
                        }
                    }
                    else
                    {
                        return ($"Request failed with status code: {response.StatusCode}", null);
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message, null);
                }
            }
        }


        #endregion






        #region Methods to add Payment

        // Method call when Button Add Payment Button Click.
        protected async void ButtonAddPayment_Click(object sender, EventArgs e)
        {
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string action = "INSERT";
            string tablename = "transition_table";
            int id = 0;

            string customer_id = hiddenField_customerid.Value;
            if (string.IsNullOrEmpty(customer_id) || customer_id == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Please Select a customer First')</script>", false);
                return;
            }

            // Parse the date using the specified format "yyyy-MM-dd"
            DateTime dateValue = DateTime.ParseExact(TextBox_paymentDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            // Convert the date back to the desired string format "dd-MM-yyyy"
            string formattedDate = dateValue.ToString("dd-MM-yyyy");

            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
               new ColumnData { columnValue = customer_id, columnName = "customer_detailsID", columnDataType = "105002" },
               new ColumnData { columnValue = TextBox_amount.Text, columnName = "payment_amount", columnDataType = "105005" },
               new ColumnData { columnValue = formattedDate, columnName = "payment_date", columnDataType = "105003" },
               new ColumnData { columnValue = ddl_paymentMode.SelectedValue, columnName = "payment_mode_masterid", columnDataType = "105002" },
               new ColumnData { columnValue = ddl_bankname.SelectedValue, columnName = "bank_masterid", columnDataType = "105002" },
               new ColumnData { columnValue = TextBox_referenceid.Text, columnName = "reference_id", columnDataType = "105001" },
               new ColumnData { columnValue = TextBox_remark.Text, columnName = "remark", columnDataType = "105001" },
               new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" },
            };

            var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
            if (Success)
            {
                ClearInputFields();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $"<script>success({JsonConvert.SerializeObject("Success: " + ErrorMessage)})</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
            }
            customer_details.Attributes["style"] = "display: none;";
        }

        // Method to save Data.


        // Method to clear all input fields.
        protected void ClearInputFields()
        {
            TextBox[] textFields = { TextBox_amount, TextBox_paymentDate, TextBox_referenceid, TextBox_remark };

            foreach (var textField in textFields)
            {
                textField.Text = string.Empty;
            }

            DropDownList[] dropdowns = { ddl_paymentMode, ddl_bankname };

            foreach (var dropdown in dropdowns)
            {
                dropdown.SelectedValue = "0";
            }

            hiddenField_customerid.Value = string.Empty;
            lbl_customerName.Text = "";
            lbl_customerEmail.Text = "";
            lbl_customerPhone.Text = "";
            lbl_country.Text = "";
            lbl_salesQty.Text = "";
            lbl_totalSalesAmount.Text = "";
            lbl_totalAmountReceived.Text = "";
            lbl_totalAmountPending.Text = "";
            lbl_grossAmount.Text = "";
        }

        #endregion
    }
}