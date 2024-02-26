<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NewCustomer.aspx.cs" Inherits="DSERP_Client_UI.NewCustomer" %>
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
                                <h2 class="mb-0">Lead Conversion Management</h2>
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

                        <style>
                            

                            .search-container {
                                display: flex;
                                align-items: center;
                                background-color: #f2f2f2;
                                border-radius: 25px;
                                padding: 5px;
                                width: 300px;
                                margin: 0 ;
                            }

                            #search-bar {
                                width: 80%;
                                border: none;
                                border-radius: 25px;
                                padding: 10px;
                            }

                            #search-button {
                                background-color: #007BFF; /* Change the background color as desired */
                                border: none;
                                color: white;
                                border-radius: 25px;
                                padding: 10px 20px;
                                cursor: pointer;
                            }

                        </style>
                       
                        <style>
                            /* General styles for the form container */
                            .partner-form {
                                background-color: #f7f7f7;
                                padding: 20px;
                                border: 1px solid #e1e1e1;
                                border-radius: 5px;
                            }

                            /* Style for section titles */
                            .form-title {
                                font-size: 24px;
                                color: #333;
                                margin: 20px 0 10px;
                            }

                            /* Styles for form groups and labels */
                            .form-group {
                                margin-bottom: 15px;
                            }

                                .form-group label {
                                    font-weight: bold;
                                    color: #333;
                                }

                            /* Style for form controls */
                            .form-control {
                                width: 100%;
                                padding: 10px;
                                border: 1px solid #ccc;
                                border-radius: 4px;
                            }

                            /* Style for the "Submit" button */
                            .btn-primary {
                                background-color: #007bff;
                                color: #fff;
                                border: none;
                                padding: 10px 20px;
                                border-radius: 4px;
                                cursor: pointer;
                            }

                                .btn-primary:hover {
                                    background-color: #0056b3;
                                }

                            /* Hide all form sections by default */
                            .panel {
                                display: none;
                            }

                                /* Display the selected form section */
                                .panel.active {
                                    display: block;
                                }
                        </style>

                        <!--  form 1 -->
                        <div class="partner-form" id="b2b" runat="server">
                            
                            <asp:Panel ID="pnlOption1" runat="server">
                                <h2 class="form-title">Partner Lead Conversion</h2>
                                <div class="search-container">
                                    <input type="text" id="search-bar" placeholder="Search...">
                                    <button id="search-button">Search</button>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="partnerName">Partner's Name:</label>
                                            <asp:TextBox ID="partnerName" runat="server" CssClass="form-control" placeholder="Enter Partner's Name" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="dob">Date of Birth:</label>
                                            <input type="date" id="dob" name="dob" class="form-control" placeholder="Enter Date of Birth">
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
                                            <label for="productName">Product:</label>
                                            <asp:DropDownList ID="productDropDown" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Product 1" Value="Product1" />
                                                <asp:ListItem Text="Product 2" Value="Product2" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="productValidity">Validity:</label>
                                            <asp:TextBox ID="productValidity" runat="server" CssClass="form-control" placeholder="Validity" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="quantity">Quantity:</label>
                                            <asp:TextBox ID="quantity" runat="server" CssClass="form-control" placeholder="Quantity" />
                                        </div>
                                    </div>
                            

                               
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="salePrice">Sale Price:</label>
                                            <asp:TextBox ID="salePrice" runat="server" CssClass="form-control" placeholder="Sale Price" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="gstin">GSTIN:</label>
                                            <asp:TextBox ID="gstin" runat="server" CssClass="form-control" placeholder="gstin" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="pan">PAN:</label>
                                            <asp:TextBox ID="pan" runat="server" CssClass="form-control" placeholder="pan" />
                                        </div>
                                    </div>
                                     <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="invoiceNumber">Invoice number:</label>
                                            <asp:TextBox ID="invoiceNumber" runat="server" CssClass="form-control" placeholder="Invoice number" />
                                        </div>
                                    </div>

                                      <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="po">Purchase Order Number:</label>
                                            <asp:TextBox ID="po" runat="server" CssClass="form-control" placeholder="PO number" />
                                        </div>
                                    </div>


                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="totalPrice">Total Price:</label>
                                            <asp:TextBox ID="totalPrice" runat="server" CssClass="form-control" placeholder="Total Price" />
                                        </div>
                                    </div>

                                 <div class="form-group col-md-4">
                                            <label for="paymentmode">Mode of Payment:</label>
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="cash" Value="cash" />
                                                <asp:ListItem Text="card" Value="card" />
                                            </asp:DropDownList>
                                        </div>

                                     <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="payment">Payment Recieved :</label>
                                            <asp:TextBox ID="payment" runat="server" CssClass="form-control" placeholder="Payment Recieved" />
                                        </div>
                                    </div>
                                     
                                  
                                </div>
                                 
                                    

                                <div class="form-group">
                                    <asp:Button ID="btnSubmitLeadConversion" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                </div>
                            </asp:Panel>
                        </div>



                        <%--  form 2  --%>
                        <div class="partner-form" id="b2c" runat="server">
                            <asp:Panel ID="pnlOption2" runat="server">
                                <h2 class="form-title">B2C Customer Details</h2>
                                <div class="search-container">
                                    <input type="text" id="search-bar" placeholder="Search...">
                                    <button id="search-button">Search</button>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="customerName">Customer Name:</label>
                                            <asp:TextBox ID="customerName" runat="server" CssClass="form-control" placeholder="Enter Customer Name" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="dob">Date of Birth:</label>
                                            <input type="date" id="dob" name="dob" class="form-control" placeholder="Enter Date of Birth">
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
                               

                                <div class="form-group col-md-4">
                                    <label for="productName">Product Name:</label>
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Product 1" Value="Product1" />
                                        <asp:ListItem Text="Product 2" Value="Product2" />
                                    </asp:DropDownList>
                                </div>
                                 <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="quantity">Quantity: </label>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Quantity Required " />
                                        </div>
                                    </div>
                                      <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="source">Validity: </label>
                                            <asp:TextBox ID="validity" runat="server" CssClass="form-control" placeholder="validity" />
                                        </div>
                                      </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="salePrice">Sale Price:</label>
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Enter Sale Price" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="invoiceNumber">Invoice Number:</label>
                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" placeholder="Enter Invoice Number" />
                                        </div>
                                    </div>
                                    
                                 <div class="form-group col-md-4">
                                            <label for="paymentmode">Mode of Payment:</label>
                                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="cash" Value="cash" />
                                                <asp:ListItem Text="card" Value="card" />
                                            </asp:DropDownList>
                                        </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="totalPayment">Total Payment:</label>
                                            <asp:TextBox ID="totalPayment" runat="server" CssClass="form-control" placeholder="Enter Total Payment" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="paymentReceived">Payment Received:</label>
                                            <asp:TextBox ID="paymentReceived" runat="server" CssClass="form-control" placeholder="Enter Payment Received" />
                                        </div>
                                    </div>


                                </div>

                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                </div>
                            </asp:Panel>
                        </div>

                        <!-- form 3 -->
                        <div class="partner-form" id="b2g" runat="server">
                            <asp:Panel ID="pnlOption3" runat="server">
                                <h2 class="form-title">Government Tender Details</h2>
                                  <div class="search-container">
                                    <input type="text" id="search-bar" placeholder="Search...">
                                    <button id="search-button">Search</button>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                          <label for="tenderName">Tender Name:</label>
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Enter Tender Name" />
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
                                    <div class="form-group col-md-4">
                                        <label for="productName">Product Name:</label>
                                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Product 1" Value="Product1" />
                                            <asp:ListItem Text="Product 2" Value="Product2" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="quantity">Quantity: </label>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Quantity Required " />
                                        </div>
                                    </div>
                                      <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="source">Validity: </label>
                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" placeholder="Validity" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="salePrice">Sale Price:</label>
                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" placeholder="Enter Sale Price" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="invoiceNumber">Invoice Number:</label>
                                            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" placeholder="Enter Invoice Number" />
                                        </div>
                                    </div>
                                      <div class="form-group col-md-4">
                                            <label for="paymentmode">Mode of Payment:</label>
                                            <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="cash" Value="cash" />
                                                <asp:ListItem Text="card" Value="card" />
                                            </asp:DropDownList>
                                        </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="totalPayment">Total Payment:</label>
                                            <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" placeholder="Enter Total Payment" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="paymentReceived">Payment Received:</label>
                                            <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" placeholder="Enter Payment Received" />
                                        </div>
                                    </div>


                                </div>

                                <div class="form-group">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                </div>
                            </asp:Panel>
                        </div>


                    </div>
            </div>       
        </div>
    </div>
</asp:Content>
