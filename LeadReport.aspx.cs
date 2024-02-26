using Humanizer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class Lead_Reporting : System.Web.UI.Page
    {
        compress compressobj = new compress();  // Unzip Method Class Object.
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDatatable();
        }

        protected void gridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridview.Rows.Count)
            {
                GridViewRow row = gridview.Rows[e.RowIndex];

                // Find the cells by column header (assuming you are using BoundFields)
                TableCell customerTypeCell = row.Cells[gridview.Columns.Cast<DataControlField>().ToList().FindIndex(c => ((BoundField)c).HeaderText == "Customer Type")];
                TableCell leadIdCell = row.Cells[gridview.Columns.Cast<DataControlField>().ToList().FindIndex(c => ((BoundField)c).HeaderText == "Lead ID")];

                if (customerTypeCell != null && leadIdCell != null)
                {
                    string customerType = customerTypeCell.Text;
                    string leadId = leadIdCell.Text;

                    // Pass the customerType and leadId as query string parameters
                    Response.Redirect("NewLead.aspx?customerType=" + customerType + "&leadId=" + leadId, false);
                }
            }
        }


        protected async void BindDatatable()
        {
            string action = "SELECT";
            using (var httpClient = new HttpClient())
            {
              //  string tableName =
              //  ["tableName"] as string;
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
                                //  GenerateToggleColumn(dt);
                                //  BindDataToDataTable(dt);
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

        protected async void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gridview.Rows[e.RowIndex];
            TableCell cell = row.Cells[gridview.Columns.IndexOf(gridview.Columns.OfType<BoundField>().First(column => column.HeaderText == "Customer Type"))];
            TableCell cell1 = row.Cells[gridview.Columns.IndexOf(gridview.Columns.OfType<BoundField>().First(column => column.HeaderText == "Lead ID"))];
            string customerType = cell.Text;
            string result = cell1.Text;
            int leadId = int.Parse(result);
            string tableName="";

            if (customerType == "B2B")
            {
                tableName = "b2b_lead";
            }
            else if (customerType == "B2C")
            {
                tableName = "b2c_lead";
            }
            else if (customerType == "B2G")
            {
                tableName = "b2g_lead";
            }
            string action = "DELETE";
            using (var httpClient = new HttpClient())
            {
                //string tableName = Session["tableName"] as string;
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/OLDExecute";
                var data = new
                {
                    tableName = tableName,
                    action = action,
                    id = leadId,
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
                                BindDatatable();
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


        protected void Button_GridViewCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Followup")
            {
                int leadId = Convert.ToInt32(e.CommandArgument);
                LinkButton button =(LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)button.NamingContainer; 
                int rowno = row.RowIndex;
 
                if (row != null)
                {
                    // Find the cell with the "Customer Type" column by header text
                    //TableCell cell = row.Cells.Cast<TableCell>().FirstOrDefault(c => gridview.Columns.Cast<DataControlField>().Any(column => column.HeaderText == "Customer Type"));

                    // Find the cell with the "Customer Type" column by index
                    int customerTypeColumnIndex = 1; // Assuming "Customer Type" is the second column (index 1)
                    TableCell cell = row.Cells[customerTypeColumnIndex];

                    if (cell != null)
                    {
                        // Extract the "Customer Type" value
                        string customerType = cell.Text;

                        // Set the style for 'followup_details'
                        followup_details.Attributes.Add("style", "display:block; opacity:1;");

                        // Now, you can pass 'leadId' and 'customerType' to your method
                        // Btn_AddFollowup(null, EventArgs.Empty, leadId, customerType);

                        Session["lead_id"] = leadId;
                        Session["customer_type"] = customerType;
                        bindFollowUp(leadId);
                    }
                }
            }
        }




        protected void button_Close(object sender, EventArgs e)
        {
            followup_details.Attributes.Add("style", "display:none");
        }

        


        public async void Btn_AddFollowup(object sender, EventArgs e)
        {
            DateTime parsedDateTime = DateTime.ParseExact(followUpDateTime.Text, "yyyy-MM-ddTHH:mm", null);
            // Format the DateTime in the desired format
            string formattedDateTime = parsedDateTime.ToString("dd-MM-yyyy- HH:mm:ss");
            string leadId = Session["lead_id"].ToString();
            string customer_type = Session["customer_type"].ToString();
       
            string UserID = Request.Cookies["userid"]?.Value;
            string ipAddress = Request.UserHostAddress;
            string followupText = Remark_txt.Text;
            //string followupDate = followUpDateTime.Text;

              List<ColumnData> textBoxDataList = new List<ColumnData>
        {
               new ColumnData { columnValue = leadId, columnName = "lead_id", columnDataType = "105002" },
               new ColumnData { columnValue = customer_type, columnName = "customer_type", columnDataType = "105001" },
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
                                Session.Remove("customer_type");
                                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>success('" + responseObject.responseMessage + "')</script>", false);

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
                                    ChatRepeater.DataSource = dt;
                                    ChatRepeater.DataBind();
                                if (dt.Rows.Count > 0)
                                {
                                    
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


        protected string CalculateTimeDifference(object followupDate)
        {
            if (followupDate != null && followupDate != DBNull.Value)
            {
                string dateStr = followupDate.ToString();

                DateTime fDate = DateTime.ParseExact(dateStr, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string formattedDate = fDate.ToString("yyyy-MM-dd HH:mm:ss");


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
            return string.Empty;
        }





    }

}


//sample data
//protected void BindDatatable()
//{
//    // Create a sample DataTable with sample data
//    DataTable sampleDataTable = CreateSampleDataTable();

//    // Bind the GridView with the sample DataTable
//    gridview.DataSource = sampleDataTable;
//    gridview.DataBind();
//}

//private DataTable CreateSampleDataTable()
//{
//    DataTable dt = new DataTable();

//    // Define columns for the DataTable
//    dt.Columns.Add("lead_id", typeof(int));
//    dt.Columns.Add("customer_type", typeof(string));
//    dt.Columns.Add("client_name", typeof(string));
//    dt.Columns.Add("email", typeof(string));
//    dt.Columns.Add("phone", typeof(string));
//    dt.Columns.Add("product_name", typeof(string));
//    dt.Columns.Add("quantity", typeof(int));
//    dt.Columns.Add("source", typeof(string));
//    dt.Columns.Add("is_active", typeof(bool));
//    dt.Columns.Add("created_date", typeof(DateTime));

//    // Add sample data rows
//    dt.Rows.Add(1, "B2B", "Company A", "companya@example.com", "123-456-7890", "Product A", 100, "Source X", true, DateTime.Now);
//    dt.Rows.Add(2, "B2B", "Company B", "companyb@example.com", "987-654-3210", "Product B", 50, "Source Y", false, DateTime.Now.AddHours(-1));
//    dt.Rows.Add(2, "B2G", "Company c", "companyb@example.com", "987-654-3210", "Product B", 50, "Source Y", false, DateTime.Now.AddHours(-1));
//    // Add more sample data rows as needed

//    return dt;
//}
