using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSERP_Client_UI
{
    public class ResponseClass
    {
        public List<ResponseObj> responseObject { get; set; }
        public int responseCode { get; set; }
        public string responseDynamic { get; set; }
        public string responseMessage { get; set; }
        public string jsonString { get; set; }
        public int recordCount { get; set; }
    }

    public class ResponseObj
    {
        public int lastInsertedID { get; set; }
    }


    public class ResponseColumnName
    {
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public int InputDataType { get; set; }
        public string ExtensionTableName { get; set; }
        public int ExtensionTableCode { get; set; }
        public int Validate_isReference { get; set; }
        public string ReferenceTableName { get; set; }
        public string ReferenceFieldName { get; set; }
        public Boolean IsPrimaryKey { get; set; }
        public Boolean Validate_isRequired { get; set; }

    }
    public class TextBoxData
    {
        public string FieldValue { get; set; }
        public string FieldName { get; set; }
        public string FieldDataType { get; set; }
    }

    public class ColumnData
    {
        public string columnValue { get; set; }
        public string columnName { get; set; }
        public string columnDataType { get; set; }
    }

    public class Responsegridtable
    {
        public int Softcron_userID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
   
}