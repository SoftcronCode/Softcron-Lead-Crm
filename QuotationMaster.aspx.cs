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
using static Humanizer.In;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using System.Threading.Tasks;
using Humanizer;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data.Common;
using System.Web.UI.WebControls.WebParts;

namespace DSERP_Client_UI
{
    public partial class QuotationMaster : System.Web.UI.Page
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.
        CommonMethods commonMethods = new CommonMethods();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();
                quotaionForm.Visible = false;
                viewPopUp.Visible = false;
                buttonUpdate.Visible = false;
                BindServiceDropdown();
                BindServiceValidityDropdown();
            }

        }


        #region DropDown Bind Methods

        // method to Bind Service Name DropDown.
        protected async void BindServiceDropdown()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string TableName = "service_master";
                string ColumnName = "Service_name";

                var (ErrorMessage, dt) = await commonMethods.BindDropDown(TableName, ColumnName, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddl_serviceName.DataSource = dt;
                    ddl_serviceName.DataTextField = "Service_name";
                    ddl_serviceName.DataValueField = "Service_masterID";
                    ddl_serviceName.DataBind();
                    ddl_serviceName.Items.Insert(0, new ListItem("--Select--", "0"));
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

        // Method to bind Service Validity DropDown.
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
                    ddl_serviceValidity.DataSource = dt;
                    ddl_serviceValidity.DataTextField = dt.Columns[0].ColumnName;
                    ddl_serviceValidity.DataValueField = dt.Columns[1].ColumnName;
                    ddl_serviceValidity.DataBind();
                    ddl_serviceValidity.Items.Insert(0, new ListItem("--Select--", "0"));
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







        #region All Method of PopUp MOdal LIke Submit Update Button Click

        //method to submit record
        protected async void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                string action = "INSERT";
                string tablename = "quotation_master";
                int id = 0;

                List<ColumnData> textBoxDataList = new List<ColumnData>
               {
                new ColumnData { columnValue = ddl_serviceName.SelectedValue, columnName = "service_masterid", columnDataType = "105002" },
                new ColumnData { columnValue = ddl_serviceValidity.SelectedValue, columnName = "service_validityid", columnDataType = "105002" },
                new ColumnData { columnValue = TextBox_price.Text, columnName = "price", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_quotationHead.Text, columnName = "quotation_head", columnDataType = "105001" },
                new ColumnData { columnValue = TextBox_quotationText.Text, columnName = "quotation_text", columnDataType = "105001" },
                new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" },
               };

                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    Buttonclear();
                    quotaionForm.Visible = false;
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

        //method to update record
        protected async void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                int id = int.Parse(HiddenField_quotationID.Value);
                string action = "UPDATE";
                string tablename = "quotation_master";

                List<ColumnData> textBoxDataList = new List<ColumnData>
            {
              new ColumnData { columnValue = ddl_serviceName.SelectedValue, columnName = "service_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_serviceValidity.SelectedValue, columnName = "service_validityid", columnDataType = "105002" },
               new ColumnData { columnValue = TextBox_price.Text, columnName = "price", columnDataType = "105001" },
               new ColumnData { columnValue = TextBox_quotationHead.Text, columnName = "quotation_head", columnDataType = "105001" },
              new ColumnData { columnValue = TextBox_quotationText.Text, columnName = "quotation_text", columnDataType = "105001" },
               new ColumnData { columnValue = UserID, columnName = "created_by", columnDataType = "105002" },
            };

                var (Success, ErrorMessage) = await commonMethods.SaveData(UserID, ipAddress, action, id, tablename, textBoxDataList);
                if (Success)
                {
                    Session.Remove("quotation_id");
                    Buttonclear();
                    quotaionForm.Visible = false;
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


        //method to open Quotation form
        protected void addquotbutton_Click(object sender, EventArgs e)
        {
            if (quotaionForm.Visible == false)
            {
                quotaionForm.Visible = true;
            }
            else
            {
                quotaionForm.Visible = false;
            }
        }

        //quotation view div (close, fresh, renew)
        protected void closeViewPopUp(object sender, EventArgs e)
        {

            //code is not comming here but we have made a jugad where when clicked on cross it will close the div this is because
            //we have set visible to false on page relode outside postback 
            if (viewPopUp.Visible == false)
            {
                viewPopUp.Visible = true;
            }
            else
            {
                viewPopUp.Visible = false;
            }
        }


        #endregion



        #region All Methods of GridView

        //method to bind gridview 
        protected async void BindGridview()
        {
            try
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Lead/GetAllQuotationData";

                var (ErrorMessage, dt) = await commonMethods.BindDataTable(apiUrl, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                    Session["quotationRecord"] = dt;
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


        //method to  open the form to update the record
        protected async void gridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = int.Parse(gridview.DataKeys[e.RowIndex]["quotation_masterid"].ToString());
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string tablename = "quotation_master";

            var (ErrorMessage, dt) = await commonMethods.GetRecordByID(UserID, ipAddress, tablename, id);


            if (dt != null && dt.Rows.Count > 0)
            {
                HiddenField_quotationID.Value = dt.Rows[0]["quotation_masterid"].ToString();
                ddl_serviceName.SelectedValue = dt.Rows[0]["service_masterid"].ToString();
                ddl_serviceValidity.SelectedValue = dt.Rows[0]["service_validityid"].ToString();
                TextBox_price.Text = dt.Rows[0]["price"].ToString();
                TextBox_quotationHead.Text = dt.Rows[0]["quotation_head"].ToString();
                TextBox_quotationText.Text = dt.Rows[0]["quotation_text"].ToString();

                quotaionForm.Visible = true;
                buttonUpdate.Visible = true;
                ButtonSubmit.Visible = false;
            }
        }


        //  method for the delete 
        protected async void gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gridview.DataKeys[e.RowIndex]["quotation_masterid"].ToString());
                string tableName = "quotation_master";
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;

                var (Success, ErrorMessage) = await commonMethods.DeleteById(id, tableName, UserID, ipAddress);
                if (Success)
                {
                    BindGridview();
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


        // M<ethod to Change status Active or DeActive
        protected async void btnChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                GridViewRow row = btn.NamingContainer as GridViewRow;
                int id = Convert.ToInt32(gridview.DataKeys[row.RowIndex].Value);
                string tableName = "quotation_master";
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                int StatusValue;
                if (btn.Text == "Active")
                {
                    StatusValue = 0;
                }
                else
                {
                    StatusValue = 1;
                }

                var (ErrorMessage, dt) = await commonMethods.UpdateStatus(tableName, id, StatusValue, UserID, ipAddress);
                if (dt != null && dt.Rows.Count > 0)
                {
                    BindGridview();
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

        //method to handel gridview commands(view)
        protected void gridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "view")
                {
                    string MasterId = e.CommandArgument.ToString();
                    DataTable dtFromSession = (DataTable)Session["quotationRecord"];
                    DataRow[] rows = dtFromSession.Select("quotation_masterID = " + MasterId);

                    if (rows.Length > 0)
                    {
                        DataRow matchingRow = rows[0];

                        // Set the details dynamically based on the matching row
                        lbl_serviceName.Text = matchingRow["service_name"].ToString();
                        lbl_serviceValidity.Text = matchingRow["validity"].ToString();
                        lbl_price.Text = matchingRow["price"].ToString();
                        lbl_quotationHead.Text = matchingRow["quotation_head"].ToString();
                        lbl_quotationText.Text = matchingRow["quotation_text"].ToString();

                        viewPopUp.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error: " + ex.Message)})</script>", false);
            }
        }


        #endregion




        #region Other Methods

        //method to clear the input fields
        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            // Set the values from the matching row to the form fields
            ddl_serviceName.SelectedValue = "0";
            ddl_serviceValidity.SelectedValue = "0";
            TextBox_price.Text = "";
            TextBox_quotationHead.Text = "";
            TextBox_quotationText.Text = "";

        }
        protected void Buttonclear()
        {
            // Set the values from the matching row to the form fields
            ddl_serviceName.SelectedValue = "0";
            ddl_serviceValidity.SelectedValue = "0";
            TextBox_price.Text = "";
            TextBox_quotationHead.Text = "";
            TextBox_quotationText.Text = "";
        }

        #endregion

    }

}
