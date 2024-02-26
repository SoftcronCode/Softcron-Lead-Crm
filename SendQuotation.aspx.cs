using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class SendQuotation : System.Web.UI.Page
    {
        compress compressobj = new compress();  // Unzip Method Class Object.
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        CommonMethods commonMethods = new CommonMethods();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllLeadRecords();
                GetAllCustomerRecords();
                discountAmount.Visible = false;
                string refParam = Request.QueryString["ref"];
                string quotationMasterId = Request.QueryString["qmid"];
                if (!string.IsNullOrEmpty(quotationMasterId))
                {
                    string errorMessage = await GetQuotationDetailsByID(quotationMasterId);
                    if (errorMessage != null)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject(errorMessage)})</script>", false);
                    }
                }
            }
        }


        #region  Get Lead And Customer Data

        // Method is use to get all Lead Records and store the data to session.
        protected async void GetAllLeadRecords()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Lead/LeadReport";
                var data = new
                {

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
                    Session["Lead_Records"] = dt;
                }
                else
                {
                    Session["Lead_Records"] = null;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }

        // Method is use to get all customer Records and store the data to session.
        protected async void GetAllCustomerRecords()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Customer/CustomerReporting";
                var data = new
                {
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
                    Session["Customer_Records"] = dt;
                }
                else
                {
                    Session["Customer_Records"] = null;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ErrorMessage)})</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }

        #endregion


        #region  Search Lead OR Customer Method

        // method use to search the lead data in session by name, phonr or email.
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtSearch.Value.Trim();
                if (string.IsNullOrEmpty(searchValue))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning", "<script>warning('Warning: Please Enter a valid search input.')</script>", false);
                    return;
                }

                int type = int.Parse(DropDown_type.SelectedValue);
                if (type == 1)
                {
                    if (Session["Lead_Records"] != null)
                    {
                        DataTable dt = (DataTable)Session["Lead_Records"];
                        if (dt.Rows.Count > 0)
                        {
                            DataRow[] matchingRows = dt.Select($"first_name LIKE '%{searchValue}%' OR " +
                                                                  $"phone_no LIKE '%{searchValue}%' OR " +
                                                                  $"email_id LIKE '%{searchValue}%'");

                            if (matchingRows.Length == 1)
                            {
                                Textbox_CustomerName.Text = matchingRows[0]["first_name"].ToString();
                                Textbox_CustomerEmail.Text = matchingRows[0]["email_id"].ToString();
                                client_data.Attributes["style"] = "display: none;";
                            }
                            else if (matchingRows.Length > 1)
                            {
                                gridview.DataSource = matchingRows;
                                gridview.DataBind();
                                client_data.Attributes["style"] = "display: block;";
                            }
                            else
                            {
                                gridview.DataSource = null;
                                gridview.DataBind();
                                client_data.Attributes["style"] = "display: none;";
                            }
                        }
                    }
                }
                else if (type == 2)
                {
                    if (Session["Customer_Records"] != null)
                    {
                        DataTable dt = (DataTable)Session["Customer_Records"];
                        if (dt.Rows.Count > 0)
                        {
                            DataRow[] matchingRows = dt.Select($"first_name LIKE '%{searchValue}%' OR " +
                                                                  $"phone_no LIKE '%{searchValue}%' OR " +
                                                                  $"email_id LIKE '%{searchValue}%'");

                            if (matchingRows.Length == 1)
                            {
                                Textbox_CustomerName.Text = matchingRows[0]["first_name"].ToString();
                                Textbox_CustomerEmail.Text = matchingRows[0]["email_id"].ToString();
                                client_data.Attributes["style"] = "display: none;";
                            }
                            else if (matchingRows.Length > 1)
                            {
                                gridview.DataSource = matchingRows;
                                gridview.DataBind();
                                client_data.Attributes["style"] = "display: block;";
                            }
                            else
                            {
                                gridview.DataSource = null;
                                gridview.DataBind();
                                client_data.Attributes["style"] = "display: none;";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
        }


        // method use to set the lead data to textboxes on select click.
        protected void OnCustomerSelectClick(object sender, EventArgs e)
        {
            try
            {
                GridViewRow selectedRow = (GridViewRow)(((LinkButton)sender).NamingContainer);

                string applicantName = selectedRow.Cells[2].Text;
                string applicantEmail = selectedRow.Cells[3].Text;

                Textbox_CustomerName.Text = applicantName;
                Textbox_CustomerEmail.Text = applicantEmail;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
        }

        #endregion




        #region Send Quotation Methods

        // method to bind the quotation details by id.
        protected async Task<string> GetQuotationDetailsByID(string id)
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Lead/GetAllQuotationData";
                var data = new
                {
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
                    DataRow filteredRow = dt.AsEnumerable().FirstOrDefault(row => Convert.ToInt32(row["quotation_masterid"]) == Convert.ToInt32(id));
                    if (filteredRow != null)
                    {
                        DataTable filteredDt = dt.Clone();            // Create a new DataTable with the same structure as the original DataTable                       
                        filteredDt.ImportRow(filteredRow);
                        if (filteredDt.Rows.Count > 0)
                        {
                            lbl_serviceName.Text = filteredDt.Rows[0]["service_name"].ToString();
                            lbl_validity.Text = filteredDt.Rows[0]["validity"].ToString();
                            lbl_price.Text = filteredDt.Rows[0]["price"].ToString();
                            lbl_quotationSubject.Text = filteredDt.Rows[0]["quotation_head"].ToString();
                            lbl_quotationText.Text = filteredDt.Rows[0]["quotation_text"].ToString();
                            Textbox_SellingPrice.Text = filteredDt.Rows[0]["price"].ToString();

                            Session["quotation_text"] = filteredDt.Rows[0]["quotation_text"].ToString();
                            Session["quotation_head"] = filteredDt.Rows[0]["quotation_head"].ToString();
                            return null;
                        }
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
            return null;
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/send-quotation");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
        }

        protected void ButtonSendQuotation_Click(object sender, EventArgs e)
        {
            string name = Textbox_CustomerName.Text;
            string email = Textbox_CustomerEmail.Text;
            string price = Textbox_SellingPrice.Text;
            string mailBody = Session["quotation_text"].ToString();
            string mailSubject = Session["quotation_head"].ToString();

            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("softcrontechnology@gmail.com", "liii lvmw yosm cgbo");
                    smtpClient.EnableSsl = true;

                    // Create the email message
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage
                    {
                        From = new MailAddress("softcrontechnology@gmail.com", "Softcron Technology"),
                        Subject = mailSubject,
                        Body = mailBody.Replace("{name}", name).Replace("{price}", price),
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(email);
                    try
                    {
                        // Send the email
                        smtpClient.Send(mailMessage);
                        Session.Remove("new_quotation_head");
                        Session.Remove("new_quotation_text");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>success('Quotation Send Successfulyy !')</script>", false);
                        Response.Redirect("/dashboard", false);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
            }
        }


        // hide or show the discount textbox.
        protected void ddl_DiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddl_DiscountType.SelectedValue;

            // Check the selected value and perform actions accordingly
            if (selectedValue == "1" || selectedValue == "2")
            {
                discountAmount.Visible = true;
            }

        }

        #endregion




    }
}

