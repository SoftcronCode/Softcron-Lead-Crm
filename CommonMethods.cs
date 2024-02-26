using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace DSERP_Client_UI
{

    public class CommonMethods
    {
        string Url = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        compress compressobj = new compress();  // Unzip Method Class Object.

        public async Task<(bool Success, string ErrorMessage)> DeleteById(int ID, string tablename, string userID, string ipaddress)
        {

            string action = "DELETE";
            string tableName = tablename;
            string UserID = userID;
            string ipAddress = ipaddress;
            var apiUrl = Url + "ERP/Setup/OLDExecute";
            var data = new
            {
                tableName = tableName,
                action = action,
                id = ID,
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
                using (var httpClient = new HttpClient())
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    return (true, responseObject.responseMessage);
                                }
                                else
                                {
                                    return (true, responseObject.responseMessage);
                                }
                            }
                            else
                            {
                                return (false, responseObject.responseMessage);
                            }
                        }
                        else
                        {
                            return (false, responseObject.responseMessage);
                        }
                    }
                    else
                    {
                        return (false, $"Request failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool Success, string ErrorMessage)> SaveData(string UserID, string ipAddress, string Action, int ID, string tablename, List<ColumnData> textBoxDataList)
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
                using (var httpClient = new HttpClient())
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    return (true, responseObject.responseMessage);
                                }
                                else
                                {
                                    return (false, responseObject.responseMessage);
                                }
                            }
                            else
                            {
                                return (false, responseObject.responseMessage);
                            }
                        }
                        else
                        {
                            return (false, responseObject.responseMessage);
                        }
                    }
                    else
                    {
                        return (false, $"Request failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }



        public async Task<(string ErrorMessage, DataTable)> GetRecordByID(string UserID, string ipAddress, string TableName, int ID)
        {
            string action = "GETBYID";
            var apiUrl = Url + "ERP/Setup/OLDExecute";
            var data = new
            {
                tableName = TableName,
                action = action,
                id = ID,
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
                using (var httpClient = new HttpClient())
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    return (responseObject.responseMessage, dt);
                                }
                                else
                                {
                                    return (responseObject.responseMessage, null);
                                }
                            }
                            else
                            {
                                return (responseObject.responseMessage, null);
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
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }


        public async Task<(string ErrorMessage, DataTable)> BindDropDown(string TableName, string ColumnName, string UserID, string ipAddress)
        {
            try
            {
                var apiUrl = Url + "ERP/Setup/GetMasterDataBinding";
                var data = new
                {
                    action = "DROPDOWNBIND",
                    searchText = "",
                    filterID = "0",
                    filterID1 = "0",
                    filterID2 = TableName, //table name 
                    filterID3 = ColumnName, // column name
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
                    using (var httpClient = new HttpClient())
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
                                if (responseObject.responseDynamic != null)
                                {
                                    var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                    if (dt.Rows.Count > 0)
                                    {
                                        return (responseObject.responseMessage, dt);
                                    }
                                    else
                                    {
                                        return (responseObject.responseMessage, null);
                                    }
                                }
                                else
                                {
                                    return (responseObject.responseMessage, null);
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
                }
                catch (Exception ex)
                {
                    return (ex.Message, null);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }


        public async Task<(string ErrorMessage, DataTable)> BindDataTable(string ApiUrl, string UserID, string ipAddress)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = ApiUrl;
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    return (responseObject.responseMessage, dt);
                                }
                                else
                                {
                                    return (responseObject.responseMessage, null);
                                }
                            }
                            else
                            {
                                return (responseObject.responseMessage, null);
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


        public async Task<(string ErrorMessage, DataTable)> SelectAllData(string TableName, string UserID, string ipAddress)
        {
            string action = "SELECT";
            var apiUrl = Url + "ERP/Setup/OLDExecute";
            var data = new
            {
                tableName = TableName,
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
                using (var httpClient = new HttpClient())
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    return (responseObject.responseMessage, dt);
                                }
                                else
                                {
                                    return (responseObject.responseMessage, null);
                                }
                            }
                            else
                            {
                                return (responseObject.responseMessage, null);
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
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }


        public async Task<(string ErrorMessage, DataTable)> CommonMethod(string apiUrl, object data)
        {
            try
            {
                using (var httpClient = new HttpClient())
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    return (responseObject.responseMessage, dt);
                                }
                                else
                                {
                                    return (responseObject.responseMessage, null);
                                }
                            }
                            else
                            {
                                return (responseObject.responseMessage, null);
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
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }


        public async Task<(string ErrorMessage, DataTable)> UpdateStatus(string TableName, int ID, int statusValue, string userID, string ipaddress)
        {
            string action = "UPDATE_STATUS";
            string tableName = TableName;
            string UserID = userID;
            string ipAddress = ipaddress;
            var apiUrl = Url + "ERP/Setup/OLDExecute";
            var data = new
            {
                tableName = tableName,
                action = action,
                id = ID,
                primaryColumn = "string",
                primarydatatype = "string",
                primaryColumnValue = statusValue,
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
                using (var httpClient = new HttpClient())
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
                            if (responseObject.responseDynamic != null)
                            {
                                var unzippedResponse = compressobj.Unzip(responseObject.responseDynamic);
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(unzippedResponse);
                                if (dt.Rows.Count > 0)
                                {
                                    return (responseObject.responseMessage , dt);
                                }
                                else
                                {
                                    return (responseObject.responseMessage, null);
                                }
                            }
                            else
                            {
                                return (responseObject.responseMessage, null);
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
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
    }
}

