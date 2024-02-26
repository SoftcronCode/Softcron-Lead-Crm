using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSERP_Client_UI
{
    public partial class NewCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                b2b.Attributes.Add("style", "display : block");
                b2c.Attributes.Add("style", "display : none");
                b2g.Attributes.Add("style", "display : none");
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
        protected void btnSubmitLeadConversion_Click(object sender, EventArgs e)
        {
            // Retrieve the value of 'quantity' from the TextBox control
            string quantityValue = quantity.Text;

            // Process the form submission with 'quantityValue'
        }
    }
}