using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace DSERP_Client_UI
{
    public partial class Sales : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();


        protected async void Page_Load(object sender, EventArgs e)
        {
            customer_details.Attributes["style"] = "display: none;";

            try
            {
                if (!IsPostBack)
                {
                    Session["Customer_Details"] = null;
                    await LoadCustomerData();
                    BindServiceNameDropDown();
                    BindServiceValidityDropdown();
                    await BindInvoiceStatusDropdown();


                    // Method to Get the Customer Detail For Add sales and this method call when id pass through the customer Page.
                    if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
                    {
                        // Retrieve the new_leadid from the query string
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }



        #region All DropDown Bind Methods

        // method to get all product Name Data
        protected async void BindServiceNameDropDown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "service_master";

                var (ErrorMessage, dt) = await commonMethods.SelectAllData(TableName, UserID, ipAddress);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string dtJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                    Session["Service_Name_Data"] = dtJson;
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

        // method to get the product validity dropdown data
        protected async void BindServiceValidityDropdown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "service_validity";
                string ColumnName = "validity";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string dtJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                    Session["Service_Validity_Data"] = dtJson;
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

        // method to load all customer Data
        protected async Task LoadCustomerData()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "customer_details";

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

        // Method to bind Invoice status DropDown
        protected async Task BindInvoiceStatusDropdown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "invoice_status";
                string ColumnName = "status";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_invoicestatus.DataSource = dt;
                    ddl_invoicestatus.DataTextField = dt.Columns[0].ColumnName;
                    ddl_invoicestatus.DataValueField = dt.Columns[1].ColumnName;
                    ddl_invoicestatus.DataBind();
                    ddl_invoicestatus.Items.Insert(0, new ListItem("--Select--", "0"));
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



        #region Search Customer Methods


        // search button click  
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
                    if (Session["Customer_Details"] != null && Session["Customer_Details"] is DataTable)
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


        protected async Task<(string ErrorMessage, DataTable dt)> GetCustomerSalesRecordByID(String id)
        {
            try
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

                var (ErrorMessage, dt) = await commonMethods.CommonMethod(apiUrl, data);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return (null, dt);
                }
                else
                {
                    return (ErrorMessage, null);
                }
            }
            catch (Exception ex)
            {
                return(ex.Message, null);
            }
            
        }

        #endregion



    }
}