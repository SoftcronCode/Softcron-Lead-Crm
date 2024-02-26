<%@ Page Title=""  Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NewLead.aspx.cs" Inherits="DSERP_Client_UI.E_commerce_Management" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pc-container">
        <div class="pc-content">
            <!-- Breadcrumb Section Start -->
            <div class="page-header">
                <div class="page-block">
                    <div class="row align-items-center">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Group Master</a></li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Lead Form Management</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Breadcrumb Section End -->

            <!-- Add User Section Start -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <asp:DropDownList ID="ddlOptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                                <asp:ListItem Text="B2B" Value="1" />
                                <asp:ListItem Text="B2C" Value="2" />
                                <asp:ListItem Text="B2G" Value="3" />
                            </asp:DropDownList>

                        </div>

                        <!--  form 1 -->
                        <div class="partner-form" id="b2b" runat="server">
                            <asp:Panel ID="pnlOption1" runat="server" >
                                <h2 class="form-title">B2B Partner Details</h2>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="partnerName">Name of Partner:</label>
                                            <asp:TextBox ID="partnerName" runat="server" CssClass="form-control" placeholder="Enter Partner Name" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="companyName">Company Name:</label>
                                            <asp:TextBox ID="companyName" runat="server" CssClass="form-control" placeholder="Enter Company Name" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="companyAddress">Company Address:</label>
                                            <asp:TextBox ID="companyAddress" runat="server" CssClass="form-control" placeholder="Enter Company Address" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="emailId">Email ID:</label>
                                            <asp:TextBox ID="emailId" runat="server" CssClass="form-control" placeholder="Enter Email ID" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="contactNo">Contact No:</label>
                                            <asp:TextBox ID="contactNo" runat="server" CssClass="form-control" placeholder="Enter Contact No" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Product Name:</label>
                                            <asp:DropDownList ID="ddl_product_name" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_product_name" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Product Class:</label>
                                            <asp:DropDownList ID="ddl_product_class" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_product_class" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Product Validity:</label>
                                            <asp:DropDownList ID="ddl_product_validity" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddl_product_validity" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="quantity">Quantity: </label>
                                            <asp:TextBox ID="Quantity" runat="server" CssClass="form-control" placeholder="Quantity Required " />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Source:</label>
                                            <asp:DropDownList ID="ddl_source" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_source" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue=""></asp:RequiredFieldValidator>
                                        </div>

                                    </div>

            
                                </div>
                               

                                <div class="form-group">
                                    <asp:Button ID="btnSubmitOption1" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="SubmitButton_b2b" />
                                       <asp:Button type="button" ID="ButtonUpdateB2B" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" Visible="false" OnClick="UpdateButton_ClickB2B" />
                                </div>
                                  </asp:Panel>
                                 </div>
                           

                      <%--  form 2  --%>
                        <div class="partner-form" id="b2c" runat="server">
                            <asp:Panel ID="pnlOption2" runat="server" >
                                <h2 class="form-title">B2C Customer Details</h2>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="customerName">Customer Name:</label>
                                            <asp:TextBox ID="customerName" runat="server" CssClass="form-control" placeholder="Enter Customer Name" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="customerEmail">Email:</label>
                                            <asp:TextBox ID="customerEmail" runat="server" CssClass="form-control" placeholder="Enter Email" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="customerPhone">Phone:</label>
                                            <asp:TextBox ID="customerPhone" runat="server" CssClass="form-control" placeholder="Enter Phone Number" />
                                        </div>
                                    </div>
                               

                                <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="form-label">Product Name:</label>
                                            <asp:DropDownList ID="ddl_product_name1" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_product_name1" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                     <div class="col-md-4">
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Product Class:</label>
                                            <asp:DropDownList ID="ddl_product_class1" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddl_product_class1" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Product Validity:</label>
                                            <asp:DropDownList ID="ddl_product_validity1" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddl_product_validity1" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="quantity">Quantity: </label>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Quantity Required " />
                                        </div>
                                 </div>
                                     
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Source:</label>
                                            <asp:DropDownList ID="ddl_source1" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddl_source1" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue=""></asp:RequiredFieldValidator>
                                        </div>

                                </div>

                                <div class="form-group">
                                    <asp:Button ID="buttonSubmitb2c" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="SubmitButton_b2c" />
                                      <asp:Button type="button" ID="buttonUpdateb2c" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" Visible="false" OnClick="UpdateButton_ClickB2C" />
                                 
                                </div>
                            </asp:Panel>
                        </div>


                        <!-- form 3 -->
                        <div class="partner-form" id="b2g" runat="server">
                            <asp:Panel ID="pnlOption3" runat="server">
                                <h2 class="form-title">Government Tender Details</h2>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                          <label for="tenderName">Tender Name:</label>
                                    <asp:TextBox ID="tenderName" runat="server" CssClass="form-control" placeholder="Enter Tender Name" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="submitterEmail">Email:</label>
                                            <asp:TextBox ID="txtSubmitterEmail" runat="server" CssClass="form-control" placeholder="Enter Your Email" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="submitterPhone">Phone:</label>
                                            <asp:TextBox ID="txtSubmitterPhone" runat="server" CssClass="form-control" placeholder="Enter Your Phone Number" />
                                        </div>
                                    </div>
                                   <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="form-label">Product Name:</label>
                                            <asp:DropDownList ID="ddl_product_name2" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddl_product_name2" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                       <div class="col-md-4">
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Product Class:</label>
                                            <asp:DropDownList ID="ddl_product_class2" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddl_product_class2" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group col-lg-3">
                                            <label class="form-label">Product Validity:</label>
                                            <asp:DropDownList ID="ddl_product_validity2" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddl_product_validity2" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>





                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="quantity">Quantity: </label>
                                            <asp:TextBox ID="quantityb2g" runat="server" CssClass="form-control" placeholder="Quantity Required " />
                                        </div>
                                    </div>
                                      <div class="form-group col-lg-3">
                                            <label class="form-label">Source:</label>
                                            <asp:DropDownList ID="ddl_source2" runat="server" CssClass="form-select" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddl_source2" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue=""></asp:RequiredFieldValidator>
                                        </div>

                                </div>

                                <div class="form-group">
                                    <asp:Button ID="btnSubmitb2g" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="SubmitButton_b2g" />
                                    <asp:Button type="button" ID="buttonupdateb2g" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" Visible="false" OnClick="UpdateButton_ClickB2G" />

                                </div>
                            </asp:Panel>
                        </div>

                    </div>
            </div>       
        </div>
    </div>
</asp:Content>

