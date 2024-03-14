using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace DSERP_Client_UI
{
    public partial class Default : System.Web.UI.Page
    {
        compress compressobj = new compress();  // Unzip Method Class Object.
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string table = Session["tablename"] as string;
                string tableCode = Session["tableCode"] as string;
                string extensionTableId = Session["extensionTableId"] as string;
                string clientMasterID = Session["clientMasterID"] as string;
                string TableAliasName = Session["TableAliasName"] as string;
                labeltablename.InnerText = TableAliasName;

                GenerateTextbox();
            }
            BindDatatable();
        }

        // Method for Bind textbox for input data on page load. according to column name.
        public async void GenerateTextbox()
        {
            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                string tableCode = Session["tableCode"] as string;
                var data = new
                {
                    action = "COLMAS",
                    searchText = "",
                    filterID = tableCode,
                    filterID1 = "0",
                    filterID2 = "",
                    filterID3 = "",
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
                            var extensionTable = JsonConvert.DeserializeObject<List<ResponseColumnName>>(unzippedResponse);
                            Repeater1.DataSource = extensionTable;
                            Repeater1.DataBind();
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

        // method is for bind Type="" in textbox
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label label = e.Item.FindControl("label1") as Label;
                TextBox textBox = (TextBox)e.Item.FindControl("TextBox1");
                DropDownList dropDownList = (DropDownList)e.Item.FindControl("DropDownList1");
                FileUpload fileUpload = (FileUpload)e.Item.FindControl("FileUpload1");
                RequiredFieldValidator textboxrequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("RequiredFieldValidator1");
                RequiredFieldValidator dropdownrequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("RequiredFieldValidator2");
                RegularExpressionValidator emailformatFieldValidator = (RegularExpressionValidator)e.Item.FindControl("RegularExpressionValidator1");

                ResponseColumnName formData = (ResponseColumnName)e.Item.DataItem;
                int DataType = formData.InputDataType;
                int Reference = formData.Validate_isReference;
                Boolean Required = formData.Validate_isRequired;
                Boolean primarykey = formData.IsPrimaryKey;
                string displayname = formData.DisplayName;



                // Set the attributes of the textbox based on the DataName value
                if (DataType == 105001)
                {
                    if (Reference == 0)
                    {
                        textBox.Attributes["type"] = "text";
                        dropDownList.Visible = false;
                        fileUpload.Visible = false;
                        textboxrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                        dropdownrequiredFieldValidator.Visible = false;
                        if (textboxrequiredFieldValidator.Enabled == true)
                        {
                            if (displayname.ToLower().Contains("email"))
                            {
                                emailformatFieldValidator.Enabled = true;
                                dropdownrequiredFieldValidator.Visible = false;
                            }
                            else
                            {
                                emailformatFieldValidator.Enabled = false;
                            }
                        }
                        else
                        {
                            emailformatFieldValidator.Enabled = false;
                        }

                    }
                    else if (Reference == 1)
                    {
                        textBox.Visible = false;
                        fileUpload.Visible = false;
                        dropDownList.Attributes["FieldName"] = DataBinder.Eval(e.Item.DataItem, "FieldName").ToString();
                        dropDownList.Attributes["InputDataType"] = DataBinder.Eval(e.Item.DataItem, "InputDataType").ToString();
                        string tablename = formData.ReferenceTableName;
                        string fieldname = formData.ReferenceFieldName;
                        BindDropdownData(tablename, fieldname, dropDownList);
                        dropdownrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                        emailformatFieldValidator.Visible = false;
                        textboxrequiredFieldValidator.Visible = false;
                    }
                }
                else if (DataType == 105002)
                {
                    if (primarykey == true)
                    {
                        e.Item.Visible = false; // Hide the entire item in the Repeater
                        return;
                    }
                    else if (Reference == 0)
                    {
                        textBox.Attributes["type"] = "number";
                        dropDownList.Visible = false;
                        fileUpload.Visible = false;
                        textboxrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                        emailformatFieldValidator.Enabled = false;
                    }
                    else if (Reference == 1)
                    {
                        textBox.Visible = false;
                        fileUpload.Visible = false;
                        dropDownList.Attributes["FieldName"] = DataBinder.Eval(e.Item.DataItem, "FieldName").ToString();
                        dropDownList.Attributes["InputDataType"] = DataBinder.Eval(e.Item.DataItem, "InputDataType").ToString();
                        string tablename = formData.ReferenceTableName;
                        string fieldname = formData.ReferenceFieldName;
                        BindDropdownData(tablename, fieldname, dropDownList);
                        dropdownrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                        emailformatFieldValidator.Visible = false;
                        textboxrequiredFieldValidator.Visible = false;
                    }
                }
                else if (DataType == 105003)
                {
                    textBox.Attributes["type"] = "date";
                    dropDownList.Visible = false;
                    fileUpload.Visible = false;
                    textboxrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                    emailformatFieldValidator.Enabled = false;
                }
                else if (DataType == 105004)
                {
                    textBox.Attributes["type"] = "datetime-local";
                    dropDownList.Visible = false;
                    fileUpload.Visible = false;
                    textboxrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                    emailformatFieldValidator.Enabled = false;
                }
                else if (DataType == 105005)
                {
                    textBox.Attributes["type"] = "number";
                    textBox.Attributes["min"] = "0";
                    textBox.Attributes["step"] = "0.01";
                    dropDownList.Visible = false;
                    fileUpload.Visible = false;
                    textboxrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                    emailformatFieldValidator.Enabled = false;
                }
                else if (DataType == 105006)
                {
                    textBox.Visible = false;
                    fileUpload.Visible = false;
                    dropDownList.Items.Add(new ListItem("--Select--", "-1"));
                    dropDownList.Items.Add(new ListItem("Yes", "Yes"));
                    dropDownList.Items.Add(new ListItem("No", "No"));
                    dropDownList.Attributes["FieldName"] = DataBinder.Eval(e.Item.DataItem, "FieldName").ToString();
                    dropDownList.Attributes["InputDataType"] = DataBinder.Eval(e.Item.DataItem, "InputDataType").ToString();
                    dropdownrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                    emailformatFieldValidator.Visible = false;
                    textboxrequiredFieldValidator.Visible = false;
                }

                else if (DataType == 105007)
                {
                    if (Reference == 0 || Reference == 1)
                    {
                        fileUpload.Attributes["type"] = "file";
                        textBox.Visible = false;
                        dropDownList.Visible = false;
                        textboxrequiredFieldValidator.Enabled = (Required == false) ? false : true;
                        textboxrequiredFieldValidator.Visible = false;
                        emailformatFieldValidator.Visible = false;
                        dropdownrequiredFieldValidator.Visible = false;
                    }
                }


                textBox.Attributes["FieldName"] = DataBinder.Eval(e.Item.DataItem, "FieldName").ToString();
                textBox.Attributes["InputDataType"] = DataBinder.Eval(e.Item.DataItem, "InputDataType").ToString();
            }
        }

        // This Method Is Used To Bind The Dropdown.
        protected async void BindDropdownData(string tablename, string fieldname, DropDownList dropdownlist)
        {
            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                var data = new
                {
                    action = "DROPDOWNBIND",
                    searchText = "",
                    filterID = "0",
                    filterID1 = "0",
                    filterID2 = tablename,
                    filterID3 = fieldname,
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
                                dropdownlist.DataSource = dt;
                                dropdownlist.DataTextField = dt.Columns[0].ColumnName;
                                dropdownlist.DataValueField = dt.Columns[1].ColumnName;
                                dropdownlist.DataBind();
                                dropdownlist.Items.Insert(0, new ListItem("--Select--", "0") { Selected = true });
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

        //Method is Execute when user click on Save Button.
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string action = "INSERT";
            string id = "0";
            GetInputFieldData(action, id);


        }

        // This method Execute when user click on Update Button.
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            String action = "UPDATE";
            String id = HiddenFieldID.Value;
            GetInputFieldData(action, id);

        }

        // this method is used to get the input data of textbox and dropdowns.
        protected async void GetInputFieldData(string action, string id)
        {
            List<TextBoxData> textBoxDataList = new List<TextBoxData>();
            bool isFirstIteration = true;
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox textBox = (TextBox)item.FindControl("TextBox1");
                    DropDownList dropDownList = (DropDownList)item.FindControl("DropDownList1");
                    FileUpload fileUpload = (FileUpload)item.FindControl("FileUpload1");
                    if (!isFirstIteration)
                    {
                        if (!string.IsNullOrEmpty(textBox.Text))
                        {
                            string fieldvalue = textBox.Text;
                            string fieldName = textBox.Attributes["FieldName"];
                            string fieldDataType = textBox.Attributes["InputDataType"];

                            // TextBoxData object and store the data
                            TextBoxData textBoxData = new TextBoxData
                            {
                                FieldValue = fieldvalue,
                                FieldName = fieldName,
                                FieldDataType = fieldDataType
                            };
                            textBoxDataList.Add(textBoxData);
                        }

                        else if (dropDownList.SelectedItem != null && !string.IsNullOrEmpty(dropDownList.SelectedItem.Text))
                        {
                            string selectedValue = dropDownList.SelectedValue;
                            string fieldNameFromDropDown = dropDownList.Attributes["FieldName"];
                            string fieldTypeFromDropDown = dropDownList.Attributes["InputDataType"];

                            // TextBoxData object for the DropDownList and store the data
                            TextBoxData dropDownListData = new TextBoxData
                            {
                                FieldValue = selectedValue,
                                FieldName = fieldNameFromDropDown,
                                FieldDataType = fieldTypeFromDropDown
                            };

                            textBoxDataList.Add(dropDownListData);
                        }


                        else if (fileUpload.HasFile)
                        {
                            var maxFileSize = 4194304;
                            if (fileUpload.PostedFile.ContentLength < maxFileSize)
                            {
                                string fieldName = textBox.Attributes["FieldName"];
                                string fieldDataType = textBox.Attributes["InputDataType"];
                                string filename = Path.GetFileName(fileUpload.PostedFile.FileName);
                                string contentType = fileUpload.PostedFile.ContentType;
                                string fileUploadFile = Path.GetExtension(fileUpload.FileName.ToString());

                                if (fileUploadFile.ToLower() == ".png" || fileUploadFile.ToLower() == ".jpeg" || fileUploadFile.ToLower() == ".jpg")
                                {
                                    try
                                    {
                                        using (Stream fs = fileUpload.PostedFile.InputStream)
                                        {
                                            using (BinaryReader br = new BinaryReader(fs))
                                            {
                                                // Read the image bytes and convert to base64
                                                byte[] bytes = br.ReadBytes((int)fs.Length);
                                                string base64Image = Convert.ToBase64String(bytes);

                                                // Create a data object and add it to the list
                                                TextBoxData uploadImage = new TextBoxData
                                                {
                                                    FieldValue = base64Image,
                                                    FieldName = fieldName,
                                                    FieldDataType = fieldDataType
                                                };

                                                textBoxDataList.Add(uploadImage);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // Handle exceptions here
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('image should be png or jpg')</script>", false);
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('File size should be less than 4 MB')</script>", false);
                                return;
                            }
                        }

                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Please Select Category Image')</script>", false);
                        //    return;
                        //}
                    }
                    isFirstIteration = false;
                }
            }
            await SaveData(textBoxDataList, action, id);
        }

        // Method for save Data.
        protected async Task SaveData(List<TextBoxData> textBoxDataList, string action, string id)
        {
            using (var httpClient = new HttpClient())
            {
                string tablename = Session["tableName"] as string;
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Setup/OLDExecute";

                var data = new
                {
                    tableName = tablename,
                    action = action,
                    id = id,
                    primaryColumn = "string",
                    primarydatatype = "string",
                    primaryColumnValue = 0,
                    columns = new List<object>(),
                    objCommon = new
                    {
                        insertedUserID = UserID,
                        insertedIPAddress = ipAddress,
                        dateShort = "dd-MM-yyyy",
                        dateLong = "dd-MM-yyyy- HH:mm:ss"
                    }
                };

                // add the dynamically textbox value to list object 
                ProcessTextBoxData(textBoxDataList, data);


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
                                BindDataToDataTable(dt);
                                ResetTextBoxValues(textBoxDataList);
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
                ButtonSubmit.Visible = true;
                ButtonUpdate.Visible = false;
            }
        }

        // this method is used to convert the input date to dd-mm-yyyy format.
        public void ProcessTextBoxData(List<TextBoxData> textBoxDataList, dynamic data)
        {
            foreach (var columnData in textBoxDataList)
            {
                if (columnData.FieldDataType == "105003")
                {
                    // Parse the date using the specified format "dd-MM-yyyy"
                    DateTime dateValue = DateTime.ParseExact(columnData.FieldValue, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                    // Convert the date back to the desired string format "dd-MM-yyyy"
                    string formattedDate = dateValue.ToString("dd-MM-yyyy");
                    var columnObject = new
                    {
                        columnName = columnData.FieldName,
                        columnValue = formattedDate,
                        columnDataType = columnData.FieldDataType,
                    };
                    data.columns.Add(columnObject);
                }
                else if (columnData.FieldDataType == "105004")
                {
                    DateTime parsedDateTime = DateTime.ParseExact(columnData.FieldValue, "yyyy-MM-ddTHH:mm", null);
                    // Format the DateTime in the desired format
                    string formattedDateTime = parsedDateTime.ToString("dd-MM-yyyy- HH:mm:ss");
                    var columnObject = new
                    {
                        columnName = columnData.FieldName,
                        columnValue = formattedDateTime,
                        columnDataType = columnData.FieldDataType,
                    };
                    data.columns.Add(columnObject);
                }

                else
                {
                    var columnObject = new
                    {
                        columnName = columnData.FieldName,
                        columnValue = columnData.FieldValue,
                        columnDataType = columnData.FieldDataType,
                    };
                    data.columns.Add(columnObject);
                }
            }
        }


        // Custom ItemTemplate class to define the format of the "Action" column
        public class ActionTemplate : ITemplate
        {
            private string firstColumnName;

            public ActionTemplate(string firstColumnName)
            {
                this.firstColumnName = firstColumnName;
            }

            public void InstantiateIn(Control container)
            {
                // Place the action buttons here, using LinkButtons or any other controls you prefer
                LinkButton btnUpdate = new LinkButton();
                btnUpdate.ID = "BtnUpdate";
                btnUpdate.CssClass = "me-3";
                btnUpdate.CommandName = "Update";
                btnUpdate.CommandArgument = "<%# Eval(" + firstColumnName + ") %>";
                btnUpdate.Attributes["data-bs-toggle"] = "tooltip";
                btnUpdate.Attributes["data-bs-placement"] = "bottom";
                btnUpdate.ToolTip = "Update";
                btnUpdate.Text = "<i class=\"fa-solid fa-pen-nib\"></i>";

                LinkButton btnDelete = new LinkButton();
                btnDelete.ID = "BtnDelete";
                btnDelete.CommandName = "Delete";
                btnUpdate.CommandArgument = "<%# Eval(" + firstColumnName + ") %>";
                btnUpdate.Attributes["data-bs-toggle"] = "tooltip";
                btnUpdate.Attributes["data-bs-placement"] = "bottom";
                btnDelete.ToolTip = "Delete";
                btnDelete.Text = "<i class=\"fa-solid fa-trash-can\"></i>";

                container.Controls.Add(btnUpdate);
                container.Controls.Add(btnDelete);
            }
        }

        // Item Template For Image
        public class DynamicImageTemplate : ITemplate
        {
            private string dataField;

            public DynamicImageTemplate(string dataField)
            {
                this.dataField = dataField;
            }

            public void InstantiateIn(Control container)
            {
                Image imageControl = new Image();
                imageControl.ID = "Image1";
                imageControl.Width = Unit.Pixel(250);
                imageControl.Height = Unit.Pixel(100);

                container.Controls.Add(imageControl);
            }




        }

        // Item Template For Status
        public class StatusItemTemplate : ITemplate
        {
            private string _ColumnName;
            private GridView _parentGridView;
            private Action<string, int, int> StatusClickHandler;

            public StatusItemTemplate(string ColumnName, GridView parentGridView, Action<string, int, int> StatusClickHandler)
            {
                this._ColumnName = ColumnName;
                this._parentGridView = parentGridView;
                this.StatusClickHandler = StatusClickHandler;
            }

            public void InstantiateIn(Control container)
            {
                // Create a Button control
                Button btnChangeStatus = new Button();
                btnChangeStatus.ID = "btnChangeStatus";
                btnChangeStatus.CommandName = "ChangeStatus";
                btnChangeStatus.DataBinding += BtnChangeStatus_DataBinding; // Subscribe to the DataBinding event
                btnChangeStatus.Click += BtnChangeStatus_Click;

                // Add the Button to the container
                container.Controls.Add(btnChangeStatus);
            }

            // Event handler for the DataBinding event
            private void BtnChangeStatus_DataBinding(object sender, EventArgs e)
            {
                Button btnChangeStatus = (Button)sender;
                object dataItem = DataBinder.GetDataItem(btnChangeStatus.NamingContainer);

                if (dataItem != null)
                {
                    // Assuming _ColumnName holds the field name
                    int status = Convert.ToInt32(DataBinder.Eval(dataItem, _ColumnName));

                    // Set the Text and CssClass based on the status value
                    btnChangeStatus.Text = status == 1 ? "Active" : "Deactivate";
                    btnChangeStatus.CssClass = status == 1 ? "status-active" : "status-deactive";
                }
            }
            protected void BtnChangeStatus_Click(object sender, EventArgs e)
            {


                Button btn = sender as Button;
                GridViewRow row = btn.NamingContainer as GridViewRow;
                int id = Convert.ToInt32(_parentGridView.DataKeys[row.RowIndex].Value);
                int StatusValue;
                String action = "UPDATE_STATUS";
                if (btn.Text == "Active")
                {
                    StatusValue = 0;
                    StatusClickHandler.Invoke(action, StatusValue, id);
                }
                else
                {
                    StatusValue = 1;
                    StatusClickHandler.Invoke(action, StatusValue, id);
                }
            }
        }

        protected async void BtnStatusChange_Click(string action, int StatusValue, int id)
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
                                BindDataToDataTable(dt);
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
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                }
            }
        }




        // Reset All TextBox Values.
        private void ResetTextBoxValues(List<TextBoxData> textBoxDataList)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox textBox = (TextBox)item.FindControl("TextBox1");
                    DropDownList dropDownList = (DropDownList)item.FindControl("DropDownList1");

                    textBox.Text = string.Empty;

                    if (dropDownList != null && dropDownList.Items.Count > 0)
                    {
                        dropDownList.SelectedIndex = 0;
                    }
                }
            }
            // Assuming textBoxDataList is defined as a List<string>
            textBoxDataList.Clear();
            ButtonUpdate.Visible = false;
            ButtonSubmit.Visible = true;
        }

        // method when clear button is click.
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox textBox = (TextBox)item.FindControl("TextBox1");
                    DropDownList dropDownList = (DropDownList)item.FindControl("DropDownList1");

                    textBox.Text = string.Empty;

                    if (dropDownList != null && dropDownList.Items.Count > 0)
                    {
                        dropDownList.SelectedIndex = 0;
                    }
                }
            }
            ButtonUpdate.Visible = false;
            ButtonSubmit.Visible = true;
        }

        // Generate Toggle Column for DataTable
        protected void GenerateToggleColumn(DataTable dt)
        {
            var toggleDiv = new HtmlGenericControl("div");
            toggleDiv.Attributes["class"] = "pb---20";
            toggleDiv.InnerHtml = "Toggle column: ";
            int dataColumnCounter = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string columnName = dt.Columns[i].ColumnName;
                // Skip adding the "is_delete" column to the toggle column

                if (columnName.EndsWith("id") || columnName.Equals("is_delete", StringComparison.OrdinalIgnoreCase) || columnName.Equals("is_active", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                dataColumnCounter++;

                var toggleAnchor = new HtmlGenericControl("a");
                toggleAnchor.Attributes["class"] = "toggle-vis toggle-anchor";
                toggleAnchor.Attributes["data-column"] = dataColumnCounter.ToString();
                toggleAnchor.InnerText = dt.Columns[i].ColumnName;
                toggleDiv.Controls.Add(toggleAnchor);

                if (i < dt.Columns.Count - 1)
                {
                    var separator = new HtmlGenericControl("span");
                    separator.InnerText = " - ";
                    toggleDiv.Controls.Add(separator);
                }
            }
            // Find the div with class "card-body" and add the toggleDiv before the GridView
            var cardBodyDiv = GridView.Parent.FindControl("cardBodyDiv") as HtmlGenericControl;
            if (cardBodyDiv != null)
            {
                // Find the index of the GridView inside the cardBodyDiv's Controls collection
                int gridViewIndex = cardBodyDiv.Controls.IndexOf(GridView);

                // Add the toggleDiv just before the GridView
                cardBodyDiv.Controls.AddAt(gridViewIndex, toggleDiv);
            }
        }

        // Method is used to bind the datatable 
        protected async void BindDatatable()
        {
            string action = "SELECT";
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
                                GenerateToggleColumn(dt);
                                BindDataToDataTable(dt);
                            }
                            else
                            {
                                GridView.DataSource = null;
                                GridView.DataBind();
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

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Check if the column exists in the DataTable
                if (e.Row.DataItem is DataRowView rowView && rowView.Row.Table.Columns.Contains("BaseImage"))
                {
                    string base64Image = rowView["BaseImage"].ToString();

                    if (!string.IsNullOrEmpty(base64Image))
                    {
                        string imageUrl = $"data:image/jpeg;base64,{base64Image}";

                        // Find the Image control in the GridView's template
                        Image img = (Image)e.Row.FindControl("Image1"); // Replace "ImageControlID" with the actual control ID

                        if (img != null)
                        {
                            img.ImageUrl = imageUrl;
                        }
                    }
                }
            }
        }


        // Method Call when GridView Update Button Click.
        protected async void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridView.DataKeys[e.RowIndex].Value);
            string action = "GETBYID";
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
                            List<Dictionary<string, object>> result2List = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(unzippedResponse);
                            SetDataToTextbox(result2List);
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

        // Method For Set The Value To Textbox and DropDowns When Update A Record.
        protected void SetDataToTextbox(List<Dictionary<string, object>> result2List)
        {
            string tablename = Session["tablename"] as string;

            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox textBox = (TextBox)item.FindControl("TextBox1");
                    DropDownList dropDownList = (DropDownList)item.FindControl("DropDownList1");


                    // Get the field name and data type from the attributes.
                    string fieldName = textBox.Attributes["FieldName"];
                    string fieldType = textBox.Attributes["InputDataType"];


                    if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(fieldType))
                    {
                        // Find the corresponding value from result2List.
                        var fieldValue = result2List.FirstOrDefault(dict => dict.TryGetValue(fieldName, out var value))?[fieldName];

                        // Set the value retrieved from result2List into the appropriate textbox or dropdown.
                        if (fieldValue != null)
                        {
                            if (fieldType != "105006")
                            {
                                if (textBox.Visible)
                                {
                                    textBox.Text = fieldValue.ToString();
                                }
                                else if (dropDownList.Visible)
                                {
                                    dropDownList.ClearSelection();

                                    ListItem itemToSelect = dropDownList.Items.FindByValue(fieldValue.ToString());
                                    if (itemToSelect != null)
                                    {
                                        itemToSelect.Selected = true;
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                            else
                            {
                                dropDownList.ClearSelection();
                                // Convert the string to title case (e.g., from "YES" to "Yes")
                                var CaptilizeFieldValue = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fieldValue.ToString().ToLower());
                                ListItem itemToSelect = dropDownList.Items.FindByValue(CaptilizeFieldValue);
                                if (itemToSelect != null)
                                {
                                    itemToSelect.Selected = true;
                                }
                                else
                                {
                                }
                            }

                        }
                    }
                }
            }
            var idFieldName = tablename + "ID";
            idFieldName = char.ToUpper(idFieldName[0]) + idFieldName.Substring(1);
            var idFieldValue = result2List.FirstOrDefault(dict => dict.TryGetValue(idFieldName, out var value))?[idFieldName];

            if (idFieldValue != null)
            {
                HiddenFieldID.Value = idFieldValue.ToString();
            }
            ButtonSubmit.Visible = false;
            ButtonUpdate.Visible = true;

        }

        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        // Method Call when GridView Delete Button Click.
        protected async void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView.DataKeys[e.RowIndex].Value);
            string action = "DELETE";
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
                                BindDataToDataTable(dt);
                            }
                            else
                            {
                                DataRow emptyRow = dt.NewRow();
                                emptyRow[0] = "No data available in table";
                                dt.Rows.Add(emptyRow);
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Error : " + responseObject.responseMessage)})</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("Request failed with status code : " + response.StatusCode)})</script>", false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $"<script>error({JsonConvert.SerializeObject("An error occurred: " + ex.Message)})</script>", false);
                }
            }
        }

        protected void BindDataToDataTable(DataTable dt)
        {
            string tableName = Session["tableName"] as string;
            string columnstatus = "";
            GridView.Columns.Clear();
            string firstColumnName = dt.Columns[0].ColumnName;
            foreach (DataColumn column in dt.Columns)
            {
                string primarycolumn = tableName + "ID";
                if ((column.ColumnName == primarycolumn || !column.ColumnName.EndsWith("id")) && !column.ColumnName.ToLower().Contains("image") && column.ColumnName.ToLower() != "is_delete" && column.ColumnName.ToLower() != "is_active")
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = column.ColumnName;
                    boundField.HeaderText = column.ColumnName;
                    boundField.SortExpression = column.ColumnName;
                    GridView.Columns.Add(boundField);
                }
                else if (column.ColumnName.ToLower().Contains("image")) // Check if column name contains "image"
                {
                    TemplateField imageActionField = new TemplateField();
                    imageActionField.HeaderText = "Image Action";
                    imageActionField.ItemTemplate = new DynamicImageTemplate(column.ColumnName); // Customize this class based on your requirement
                    GridView.Columns.Add(imageActionField);
                }
                else if (column.ColumnName.ToLower().Contains("is_active"))
                {
                    columnstatus = column.ColumnName.ToLower();
                }
            }

            if (columnstatus == "is_active")
            { // Add the "Status" TemplateField
                TemplateField statusField = new TemplateField();
                statusField.HeaderText = "Status";
                StatusItemTemplate itemTemplate = new StatusItemTemplate(columnstatus, GridView, BtnStatusChange_Click);
                statusField.ItemTemplate = itemTemplate;
                GridView.Columns.Add(statusField);
            }

            // Add the "Action" TemplateField at the end
            TemplateField actionField = new TemplateField();
            actionField.HeaderText = "Action";
            actionField.ItemTemplate = new ActionTemplate(firstColumnName);
            GridView.Columns.Add(actionField);

            GridView.DataSource = dt;
            GridView.DataBind();

            GridView.DataKeyNames = new string[] { firstColumnName };
        }


    }
}