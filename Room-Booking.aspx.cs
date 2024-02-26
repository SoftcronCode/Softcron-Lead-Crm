using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class Room_Booking : System.Web.UI.Page
    {

        compress compressobj = new compress();  // Unzip Method Class Object.
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGenderTypeDropdown();
                BindRoomTypeDropdown();
                BindRoomCategoryDropdown();
                BindVerificationDropdown();
                ClearAllFields();
                BindPaymentMode();
            }

        }

        // Bind Gender Data To Dropdown
        protected async void BindGenderTypeDropdown()
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
                    filterID2 = "gender_master", //table name 
                    filterID3 = "gender", // column name
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
                                DropDownList_Gender.DataSource = dt;
                                DropDownList_Gender.DataTextField = dt.Columns[0].ColumnName;
                                DropDownList_Gender.DataValueField = dt.Columns[1].ColumnName;
                                DropDownList_Gender.DataBind();
                                DropDownList_Gender.Items.Insert(0, new ListItem("--Select--", "0"));
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

        //Bind Row type to Dropdown
        protected async void BindRoomTypeDropdown()
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
                    filterID2 = "room_type_master", //table name 
                    filterID3 = "room_type", // column name
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
                                DropDownList_Room_Type.DataSource = dt;
                                DropDownList_Room_Type.DataTextField = dt.Columns[0].ColumnName;
                                DropDownList_Room_Type.DataValueField = dt.Columns[1].ColumnName;
                                DropDownList_Room_Type.DataBind();
                                DropDownList_Room_Type.Items.Insert(0, new ListItem("--Select--", "0"));
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

        //method to bind room category 
        protected async void BindRoomCategoryDropdown()
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
                    filterID2 = "room_category_master", //table name 
                    filterID3 = "room_category", // column name
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
                                DropDownList_Room_Category.DataSource = dt;
                                DropDownList_Room_Category.DataTextField = dt.Columns[0].ColumnName;
                                DropDownList_Room_Category.DataValueField = dt.Columns[1].ColumnName;
                                DropDownList_Room_Category.DataBind();
                                DropDownList_Room_Category.Items.Insert(0, new ListItem("--Select--", "0"));
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


        //method to bind verification category 
        protected async void BindVerificationDropdown()
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
                    filterID2 = "verification_id_master", //table name 
                    filterID3 = "verification_id_name", // column name
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
                                DropDownListVerificationType.DataSource = dt;
                                DropDownListVerificationType.DataTextField = dt.Columns[0].ColumnName;
                                DropDownListVerificationType.DataValueField = dt.Columns[1].ColumnName;
                                DropDownListVerificationType.DataBind();
                                DropDownListVerificationType.Items.Insert(0, new ListItem("--Select--", "0"));
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


        //method to bind verification category 
        protected async void BindPaymentMode()
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
                    filterID2 = "pay_mode_master", //table name 
                    filterID3 = "payment_mode", // column name
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
                                ddl_paymentmode.DataSource = dt;
                                ddl_paymentmode.DataTextField = dt.Columns[0].ColumnName;
                                ddl_paymentmode.DataValueField = dt.Columns[1].ColumnName;
                                ddl_paymentmode.DataBind();
                                ddl_paymentmode.Items.Insert(0, new ListItem("--Select--", "0"));
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


        //method to save data 
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string action = "INSERT";
            int id = 0;
            SaveData(action, id);
        }

        protected async void SaveData(string Action, int ID)
        {

            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Booking/GuestAndBookingDetail";


                //CHECK IN DATETIME
                string checkinDateStr = Request.Form["check_in_datetime"];
                DateTime checkinDate;
                if (DateTime.TryParse(checkinDateStr, out checkinDate)) ;

                //CHECK OUT DATETIME
                string checkoutDateStr = Request.Form["check_out_datetime"];
                DateTime checkoutDate;
                if (DateTime.TryParse(checkoutDateStr, out checkoutDate)) ;


                var data = new
                {
                    firstName = TextBox_firstname.Text,
                    lastName = TextBox_lastname.Text,
                    gender = DropDownList_Gender.SelectedValue,
                    phone = TextBox_phone.Text,
                    emailId = TextBox_email.Text,
                    city = TextBox_city.Text,
                    address = TextBox_address.Text,
                    noOfPerson = TextBox_numPersons.Text, // You can replace this with your own logic to get the number of persons
                    noOfRooms = TextBox_numRooms.Text, // You can replace this with your own logic to get the number of rooms
                    roomType = DropDownList_Room_Type.SelectedValue, // You can replace this with your own logic to get the room type
                    roomCategory = DropDownList_Room_Category.SelectedValue, // You can replace this with your own logic to get the room category
                    checkinDate = checkinDate, // Assign the parsed checkin date
                    spCheckoutDate = checkoutDate, // Assign the parsed checkout date
                    verifyidName = DropDownListVerificationType.SelectedValue, // You can replace this with your own logic to get the verification type
                    verifyidNo = TextBox_Verification_id.Text,
                    payment = TextBox_Payment.Text, // You can replace this with your own logic to get the payment amount
                    advancePayment = TextBox_AdvancePayment.Text, // You can replace this with your own logic to get the advance payment amount
                    paymentmode = ddl_paymentmode.SelectedValue,
                    referenceid = TextBox_refrence_id.Text,
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
                                //  gridview.DataBind();
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
            ClearAllFields();
        }



        //method to search the record 
        protected async void SearchButton_Click(object sender, EventArgs e)
        {
            string Action = "SELECT";
            using (var httpClient = new HttpClient())
            {
                string UserID = Request.Cookies["userid"]?.Value;
                string ipAddress = Request.UserHostAddress;
                var apiUrl = Url + "ERP/Booking/GuestDetailByMobileNo"; // api url
                var data = new
                {
                    mobileNumber = TextBox_search.Text,
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
                                TextBox_firstname.Text = dt.Rows[0][1].ToString();
                                TextBox_lastname.Text = dt.Rows[0][2].ToString();
                                DropDownList_Gender.SelectedValue = dt.Rows[0][3].ToString();
                                TextBox_phone.Text = dt.Rows[0][4].ToString();
                                TextBox_email.Text = dt.Rows[0][5].ToString();
                                TextBox_city.Text = dt.Rows[0][6].ToString();
                                TextBox_address.Text = dt.Rows[0][7].ToString();
                                //TextBox_Verification_id.Text = dt.Rows[0][7].ToString();
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>success('" + responseObject.responseMessage + "')</script>", false);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>error('Error: " + responseObject.responseMessage + "')</script>", false);
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
                    ClearAllFields();
                }
            }


        }



        //method to clear all feilds
        private void ClearAllFields()
        {
            TextBox_firstname.Text = string.Empty;
            TextBox_lastname.Text = string.Empty;
            //DropDownList_Gender.SelectedValue = string.Empty;
            TextBox_phone.Text = string.Empty;
            TextBox_email.Text = string.Empty;
            TextBox_address.Text = string.Empty;
            TextBox_city.Text = string.Empty;
            TextBox_numPersons.Text = string.Empty;
            TextBox_numRooms.Text = string.Empty;
            //DropDownList_Room_Type.SelectedValue = string.Empty;
            // DropDownList_Room_Category.SelectedValue = string.Empty;
            //  DropDownListVerificationType.SelectedValue = string.Empty;
            TextBox_Verification_id.Text = string.Empty;
            TextBox_Payment.Text = string.Empty;
            TextBox_AdvancePayment.Text = string.Empty;
        }










    }
}