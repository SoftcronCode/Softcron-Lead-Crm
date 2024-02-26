using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class E_commerce_Management : System.Web.UI.Page
    {

        compress compressobj = new compress();  // Unzip Method Class Object.
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();


        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                b2b.Attributes.Add("style", "display : block");
                b2c.Attributes.Add("style", "display : none");
                b2g.Attributes.Add("style", "display : none");


                BindProductDropdown();
                BindSourceDropdown();
                BindProductClassDropdown();
                string customerType = Request.QueryString["customerType"];
              //  int leadId = int.Parse(Request.QueryString["leadId"]);
                if (customerType == "B2B")
                {
                    int leadId = int.Parse(Request.QueryString["leadId"]);
                    ddlOptions.SelectedValue = "1";
                    pnlOption1.Visible = true;
                    ButtonUpdateB2B.Visible = true;
                    btnSubmitOption1.Visible = false;

                    int id = leadId;
                    string action = "GETBYID";
                    using (var httpClient = new HttpClient())
                    {
                        string tableName = "b2b_lead";
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
                                    var leadData = JsonConvert.DeserializeObject<List<LeadData>>(unzippedResponse);
                                   
                                    if (leadData.Count > 0)
                                    {
                                        var lead = leadData[0]; // Assuming you have only one lead in the list

                                        partnerName.Text = lead.name_of_partner;
                                        companyName.Text = lead.company_name;
                                        companyAddress.Text = lead.company_address;
                                        emailId.Text = lead.emailid;
                                        contactNo.Text = lead.phone;

                                        // Assuming ddl_product_name and ddl_source are DropDownList controls
                                        ddl_product_name.SelectedValue = lead.product_name_masterid.ToString();
                                        Quantity.Text = lead.quantity.ToString();
                                        ddl_source.SelectedValue = lead.source_masterid.ToString();
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
            
                else if (customerType == "B2C")
                {
                    int leadId = int.Parse(Request.QueryString["leadId"]);
                    ddlOptions.SelectedValue = "2";
                    pnlOption2.Visible = true;
                    buttonUpdateb2c.Visible = true;
                    buttonSubmitb2c.Visible = false;
                    int id = leadId;
                    string action = "GETBYID";
                    using (var httpClient = new HttpClient())
                    {
                        string tableName = "b2c_lead";
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
                                    var B2CCustomerData = JsonConvert.DeserializeObject<List<B2CCustomerData>>(unzippedResponse);

                                    if (B2CCustomerData.Count > 0)
                                    {
                                        var customer = B2CCustomerData[0]; // Assuming you have only one B2C customer in the list

                                        customerName.Text = customer.customer_name;
                                        customerEmail.Text = customer.customer_emailid;
                                        customerPhone.Text = customer.customer_phone;

                                        // Assuming ddl_product_name1 and ddl_source1 are DropDownList controls
                                        ddl_product_name1.SelectedValue = customer.product_name_masterid.ToString();
                                        TextBox1.Text = customer.quantity.ToString();
                                        ddl_source1.SelectedValue = customer.source_masterid.ToString();
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

                else if (customerType == "B2G")
                {
                    int leadId = int.Parse(Request.QueryString["leadId"]);
                    ddlOptions.SelectedValue = "3";
                    pnlOption3.Visible = true;
                    buttonupdateb2g.Visible = true;
                    btnSubmitb2g.Visible = false;

                    int id = leadId;
                    string action = "GETBYID";
                    using (var httpClient = new HttpClient())
                    {
                        string tableName = "b2g_lead";
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
                                    var B2GCustomerData = JsonConvert.DeserializeObject<List<B2GCustomerData>>(unzippedResponse);

                                    if (B2GCustomerData.Count > 0)
                                    {
                                        var customer = B2GCustomerData[0]; // Assuming you have only one B2G customer in the list

                                        tenderName.Text = customer.tender_name;
                                        txtSubmitterEmail.Text = customer.emailid;
                                        txtSubmitterPhone.Text = customer.phone;

                                        // Assuming ddl_product_name1 and ddl_source1 are DropDownList controls
                                        ddl_product_name2.SelectedValue = customer.product_name_masterid.ToString();
                                        quantityb2g.Text = customer.quantity.ToString();
                                        ddl_source2.SelectedValue = customer.source_masterid.ToString();
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

            }
        }

        protected void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlOptions.SelectedValue;
            switch (selectedValue)
            {
                case "1":
                    b2b.Attributes.Add("style", "display : block");
                    b2c.Attributes.Add("style", "display : none");
                    b2g.Attributes.Add("style", "display : none");
                    break;
                case "2":
                    b2b.Attributes.Add("style", "display : none");
                    b2c.Attributes.Add("style", "display : block");
                    b2g.Attributes.Add("style", "display : none");
                    break;
                case "3":
                    b2b.Attributes.Add("style", "display : none");
                    b2c.Attributes.Add("style", "display : none");
                    b2g.Attributes.Add("style", "display : block");
                    break;
            }
        }

        protected async void BindProductDropdown()
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
                    filterID2 = "product_name_master", //table name 
                    filterID3 = "product_name", // column name
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
                            {   //b2b
                                ddl_product_name.DataSource = dt;
                                ddl_product_name.DataTextField = dt.Columns[0].ColumnName;
                                ddl_product_name.DataValueField = dt.Columns[1].ColumnName;
                                ddl_product_name.DataBind();
                                ddl_product_name.Items.Insert(0, new ListItem("--Select--", "0"));

                                //b2c
                                ddl_product_name1.DataSource = dt;
                                ddl_product_name1.DataTextField = dt.Columns[0].ColumnName;
                                ddl_product_name1.DataValueField = dt.Columns[1].ColumnName;
                                ddl_product_name1.DataBind();
                                ddl_product_name1.Items.Insert(0, new ListItem("--Select--", "0"));

                                //b2g
                                ddl_product_name2.DataSource = dt;
                                ddl_product_name2.DataTextField = dt.Columns[0].ColumnName;
                                ddl_product_name2.DataValueField = dt.Columns[1].ColumnName;
                                ddl_product_name2.DataBind();
                                ddl_product_name2.Items.Insert(0, new ListItem("--Select--", "0"));



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

        protected async void BindProductClassDropdown()
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
                    filterID2 = "product_class_master", //table name 
                    filterID3 = "product_class", // column name
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
                            {   //b2b
                                ddl_product_class.DataSource = dt;
                                ddl_product_class.DataTextField = dt.Columns[0].ColumnName;
                                ddl_product_class.DataValueField = dt.Columns[1].ColumnName;
                                ddl_product_class.DataBind();
                                ddl_product_class.Items.Insert(0, new ListItem("--Select--", "0"));

                                //b2c
                                ddl_product_class1.DataSource = dt;
                                ddl_product_class1.DataTextField = dt.Columns[0].ColumnName;
                                ddl_product_class1.DataValueField = dt.Columns[1].ColumnName;
                                ddl_product_class1.DataBind();
                                ddl_product_class1.Items.Insert(0, new ListItem("--Select--", "0"));

                                //b2g
                                ddl_product_class2.DataSource = dt;
                                ddl_product_class2.DataValueField = dt.Columns[1].ColumnName;
                                ddl_product_class2.DataBind();
                                ddl_product_class2.Items.Insert(0, new ListItem("--Select--", "0"));



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
        protected async void BindSourceDropdown()
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
                    filterID2 = "source_master", //table name 
                    filterID3 = "source", // column name
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
                                //b2b
                                ddl_source.DataSource = dt;
                                ddl_source.DataTextField = dt.Columns[0].ColumnName;
                                ddl_source.DataValueField = dt.Columns[1].ColumnName;
                                ddl_source.DataBind();
                                ddl_source.Items.Insert(0, new ListItem("--Select--", "0"));

                                //b2c
                                ddl_source1.DataSource = dt;
                                ddl_source1.DataTextField = dt.Columns[0].ColumnName;
                                ddl_source1.DataValueField = dt.Columns[1].ColumnName;
                                ddl_source1.DataBind();
                                ddl_source1.Items.Insert(0, new ListItem("--Select--", "0"));

                                //b2g
                                ddl_source2.DataSource = dt;
                                ddl_source2.DataTextField = dt.Columns[0].ColumnName;
                                ddl_source2.DataValueField = dt.Columns[1].ColumnName;
                                ddl_source2.DataBind();
                                ddl_source2.Items.Insert(0, new ListItem("--Select--", "0"));


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



        protected void SubmitButton_b2b(object sender, EventArgs e)
        {
            string action = "INSERT";
            string tablename = "b2b_lead";
            int id = 0;
            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
               new ColumnData { columnValue = ddlOptions.SelectedItem.Text, columnName = "customer_type", columnDataType = "105001" },
               new ColumnData { columnValue = partnerName.Text, columnName = "name_of_partner", columnDataType = "105001" },
              new ColumnData { columnValue = companyName.Text, columnName = "company_name", columnDataType= "105001" },
              new ColumnData { columnValue = companyAddress.Text, columnName = "company_address", columnDataType = "105001" },
              new ColumnData { columnValue = emailId.Text, columnName = "emailId", columnDataType = "105001" },
              new ColumnData { columnValue = contactNo.Text, columnName = "phone", columnDataType = "105001" },
               new ColumnData { columnValue = ddl_product_name.SelectedValue, columnName = "product_name_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = Quantity.Text, columnName = "quantity", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_source.SelectedValue, columnName = "source_masterid", columnDataType = "105002" },
            };
            SaveData(action, id, textBoxDataList, tablename);


        }
        protected void UpdateButton_ClickB2B(object sender, EventArgs e)
        {
            String action = "UPDATE";
            int id = int.Parse(Request.QueryString["leadId"]);
            string tablename = "b2b_lead";
            List<ColumnData> textBoxDataList = new List<ColumnData>
            {
               new ColumnData { columnValue = ddlOptions.SelectedItem.Text, columnName = "customer_type", columnDataType = "105001" },
               new ColumnData { columnValue = partnerName.Text, columnName = "name_of_partner", columnDataType = "105001" },
              new ColumnData { columnValue = companyName.Text, columnName = "company_name", columnDataType= "105001" },
              new ColumnData { columnValue = companyAddress.Text, columnName = "company_address", columnDataType = "105001" },
              new ColumnData { columnValue = emailId.Text, columnName = "emailId", columnDataType = "105001" },
              new ColumnData { columnValue = contactNo.Text, columnName = "phone", columnDataType = "105001" },
               new ColumnData { columnValue = ddl_product_name.SelectedValue, columnName = "product_name_masterid", columnDataType = "105002" },
              new ColumnData { columnValue = Quantity.Text, columnName = "quantity", columnDataType = "105002" },
              new ColumnData { columnValue = ddl_source.SelectedValue, columnName = "source_masterid", columnDataType = "105002" },
            };
            SaveData(action, id, textBoxDataList, tablename);
        }

        protected void SubmitButton_b2c(object sender, EventArgs e)
        {
            string action = "INSERT";
            string tablename = "b2c_lead";
            int id = 0;
            List<ColumnData> textBoxDataList = new List<ColumnData>
            {    new ColumnData { columnValue = ddlOptions.SelectedItem.Text, columnName = "customer_type", columnDataType = "105001" },
                 new ColumnData { columnValue = customerName.Text, columnName = "customer_name", columnDataType = "105001" },
                 new ColumnData { columnValue = customerEmail.Text, columnName = "customer_emailId", columnDataType = "105001" },
                 new ColumnData { columnValue = customerPhone.Text, columnName = "customer_phone", columnDataType = "105001" },
                 new ColumnData { columnValue = ddl_product_name1.SelectedValue, columnName = "product_name_masterid", columnDataType = "105002" },
                 new ColumnData { columnValue = TextBox1.Text, columnName = "quantity", columnDataType = "105002" },
                 new ColumnData { columnValue = ddl_source1.SelectedValue, columnName = "source_masterid", columnDataType = "105002" },
            };

            SaveData(action, id, textBoxDataList, tablename);
        }
        protected void UpdateButton_ClickB2C(object sender, EventArgs e)
        {
            String action = "UPDATE";
            int id = int.Parse(Request.QueryString["leadId"]);
            string tablename = "b2c_lead";
            List<ColumnData> textBoxDataList = new List<ColumnData>
            {    new ColumnData { columnValue = ddlOptions.SelectedItem.Text, columnName = "customer_type", columnDataType = "105001" },
                 new ColumnData { columnValue = customerName.Text, columnName = "customer_name", columnDataType = "105001" },
                 new ColumnData { columnValue = customerEmail.Text, columnName = "customer_emailId", columnDataType = "105001" },
                 new ColumnData { columnValue = customerPhone.Text, columnName = "customer_phone", columnDataType = "105001" },
                 new ColumnData { columnValue = ddl_product_name1.SelectedValue, columnName = "product_name_masterid", columnDataType = "105002" },
                 new ColumnData { columnValue = TextBox1.Text, columnName = "quantity", columnDataType = "105002" },
                 new ColumnData { columnValue = ddl_source1.SelectedValue, columnName = "source_masterid", columnDataType = "105002" },
            };

            SaveData(action, id, textBoxDataList, tablename);
        }
        protected void SubmitButton_b2g(object sender, EventArgs e)
        {
            string action = "INSERT";
            string tablename = "b2g_lead";
            int id = 0;
            List<ColumnData> textBoxDataList = new List<ColumnData>
            {     new ColumnData { columnValue = ddlOptions.SelectedItem.Text, columnName = "customer_type", columnDataType = "105001" },
                  new ColumnData { columnValue = tenderName.Text, columnName = "tender_name", columnDataType = "105001" },
                  new ColumnData { columnValue = txtSubmitterEmail.Text, columnName = "emailid", columnDataType = "105001" },
                  new ColumnData { columnValue = txtSubmitterPhone.Text, columnName = "phone", columnDataType = "105001" },
                  new ColumnData { columnValue = ddl_product_name2.SelectedValue, columnName = "product_name_masterid", columnDataType = "105002" },
                  new ColumnData { columnValue = quantityb2g.Text, columnName = "quantity", columnDataType = "105002" },
                  new ColumnData { columnValue = ddl_source2.SelectedValue, columnName = "source_masterid", columnDataType = "105002" },
                };


            SaveData(action, id, textBoxDataList, tablename);
        }
        protected void UpdateButton_ClickB2G(object sender, EventArgs e)
        {
            String action = "UPDATE";
            int id = int.Parse(Request.QueryString["leadId"]);
            string tablename = "b2g_lead";
            List<ColumnData> textBoxDataList = new List<ColumnData>
            {     new ColumnData { columnValue = ddlOptions.SelectedItem.Text, columnName = "customer_type", columnDataType = "105001" },
                  new ColumnData { columnValue = tenderName.Text, columnName = "tender_name", columnDataType = "105001" },
                  new ColumnData { columnValue = txtSubmitterEmail.Text, columnName = "emailid", columnDataType = "105001" },
                  new ColumnData { columnValue = txtSubmitterPhone.Text, columnName = "phone", columnDataType = "105001" },
                  new ColumnData { columnValue = ddl_product_name2.SelectedValue, columnName = "product_name_masterid", columnDataType = "105002" },
                  new ColumnData { columnValue = quantityb2g.Text, columnName = "quantity", columnDataType = "105002" },
                  new ColumnData { columnValue = ddl_source2.SelectedValue, columnName = "source_masterid", columnDataType = "105002" },
                };

            SaveData(action, id, textBoxDataList, tablename);
        }
        protected async void SaveData(string Action, int ID, List<ColumnData> textBoxDataList,string tablename)
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
                                // gridview.DataSource = dt;
                                // gridview.DataBind();
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
       
       



    }


    public class LeadData
    {
        public int b2b_leadID { get; set; }
        public string customer_type { get; set; }
        public string name_of_partner { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string emailid { get; set; }
        public string phone { get; set; }
        public int product_name_masterid { get; set; }
        public int quantity { get; set; }
        public int source_masterid { get; set; }
        public int is_active { get; set; }
        public int is_delete { get; set; }
        public string created_by { get; set; }
        public DateTime created_datetime { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_datetime { get; set; }
        public string ipaddress { get; set; }
    }

    public class B2CCustomerData
    {
        public int b2c_leadID { get; set; }
        public string customer_type { get; set; }
        public string customer_name { get; set; }
        public string customer_emailid { get; set; }
        public string customer_phone { get; set; }
        public int product_name_masterid { get; set; }
        public int quantity { get; set; }
        public int source_masterid { get; set; }
        public int is_active { get; set; }
        public int is_delete { get; set; }
        public string created_by { get; set; }
        public DateTime created_datetime { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_datetime { get; set; }
        public string ipaddress { get; set; }
    }

    public class B2GCustomerData
    {
        public int b2g_leadID { get; set; }
        public string customer_type { get; set; }
        public string tender_name { get; set; }
        public string emailid { get; set; }
        public string phone { get; set; }
        public int product_name_masterid { get; set; }
        public int quantity { get; set; }
        public int source_masterid { get; set; }
        public int is_active { get; set; }
        public int is_delete { get; set; }
        public string created_by { get; set; }
        public DateTime created_datetime { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_datetime { get; set; }
        public string ipaddress { get; set; }
    }

}