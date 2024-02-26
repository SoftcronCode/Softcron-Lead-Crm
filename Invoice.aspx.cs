using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class Invoice : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                lbl_date.Text = currentDate;
                string customerID = Request.QueryString["cid"].ToString();
                string salesID = Request.QueryString["sid"].ToString();
                if (!string.IsNullOrEmpty(customerID))
                {
                    GetCustomerRecordById(customerID);
                }
                if (!string.IsNullOrEmpty(salesID))
                {
                    GetSalesData(salesID);
                }
            }
        }


        protected async void GetCustomerRecordById(string customerID)
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string tableName = "customer_details";
                int id = int.Parse(customerID);

                var (ErrorMessage, dt) = await commonMethods.GetRecordByID(UserID, ipAddress, tableName, id);

                if (dt != null)
                {
                    string firstname = dt.Rows[0]["first_name"].ToString();
                    string lastname = dt.Rows[0]["last_name"].ToString();
                    string fullname = $"{firstname} {lastname}";
                    lbl_customerName.Text = fullname;
                    lbl_customerAddress.Text = dt.Rows[0]["address"].ToString();
                    lbl_custEmail.Text = dt.Rows[0]["email_id"].ToString();
                    lbl_custPhone.Text = dt.Rows[0]["phone_no"].ToString();
                    string gstNumber = dt.Rows[0]["gst_number"].ToString();
                    lbl_custGst.Text = gstNumber;
                    string stateCode = gstNumber.Length >= 2 ? gstNumber.Substring(0, 2) : gstNumber;
                    Session["StateCode"] = stateCode;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
        }


        protected async void GetSalesData(string salesID)
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Invoice/GetSalesDataForInvoice";
                var data = new
                {
                    id = salesID,
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
                    Repeater.DataSource = dt;
                    Repeater.DataBind();

                    lbl_subTotal.Text = Convert.ToDecimal(dt.Rows[0]["sub_total"]).ToString("0.00");
                    lbl_discountAmt.Text = Convert.ToDecimal(dt.Rows[0]["discount_amount"]).ToString("0.00");

                    lbl_totalAmt.Text = Convert.ToDecimal(dt.Rows[0]["final_total"]).ToString("0.00");
                    string gstPercentage = Convert.ToString(dt.Rows[0]["gst_percentage"]);
                    string gstAmount = Convert.ToDecimal(dt.Rows[0]["gst_amount"]).ToString("0.00");

                    //lbl_gstVal.Text = gstPercentage;
                    //lbl_gstAmt.Text = gstAmount;

                    string stateCode = Session["StateCode"] as string;
                    if (stateCode == "06")
                    {
                        decimal numericGstAmount = Convert.ToDecimal(gstAmount);
                        decimal numericGstPercentage = Convert.ToDecimal(gstPercentage);

                        // Calculate half GST values
                        decimal halfGstAmount = numericGstAmount / 2;
                        decimal halfGstPercentage = numericGstPercentage / 2;

                        cgst.Attributes.Add("style", "display:block");
                        sgst.Attributes.Add("style", "display:block");
                        lbl_cgst.Text = halfGstPercentage.ToString("0.00");
                        lbl_sgst.Text = halfGstPercentage.ToString("0.00");
                        lbl_cgstAmt.Text = halfGstAmount.ToString("0.00");
                        lbl_sgstAmt.Text = halfGstAmount.ToString("0.00");
                    }
                    else
                    {
                        gst.Attributes.Add("style", "display:block");
                        lbl_gstVal.Text = gstPercentage;
                        lbl_gstAmt.Text = gstAmount;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + ErrorMessage)})</script>", false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
        }
    }

}