<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddPayment.aspx.cs" Inherits="DSERP_Client_UI.AddPayment" %>

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
                                <li class="breadcrumb-item"><a href="/dashboard">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Add Payment</a></li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Add Customer Payment</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Breadcrumb Section End -->

            <!-- Search Bar Start -->
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row align-items-center">
                                <div class="col-sm-4">
                                    <div class="d-flex px-0 py-2">
                                        <input type="search" id="txtSearch" runat="server" class="form-control border-serarch shadow-none" placeholder="Search here. . .">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-light-success ms-2" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Search Bar End -->

            <!-- Search Table Start -->
            <div class="row" id="customer_data" runat="server" style="display: none;">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="customer_detailsID" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="name_click" runat="server" CommandName="details" CommandArgument='<%# Eval("customer_detailsID")%>' OnClick="OnCustomerSelectClick" Text='<%# Eval("first_name")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="email_id" HeaderText="Email" SortExpression="email_id" />
                                        <asp:BoundField DataField="phone_no" HeaderText="Phone" SortExpression="phone_no" />

                                    </Columns>

                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="12" class="dataTables_empty text-center">No record found</td>
                                        </tr>
                                    </EmptyDataTemplate>

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Search Table end -->


            <!-- Customer Details Section Start -->
            <div class="row" id="customer_details" runat="server" style="display: none;">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5>Customer Details</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">

                                <asp:HiddenField ID="hiddenField_customerid" runat="server" />

                                <div class="col-md-6 col-xl-3">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Customer Name :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_customerName" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-3">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Customer Email :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_customerEmail" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-3">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Customer Phone :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_customerPhone" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-3">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Customer Country :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_country" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Quantity Purchase :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_salesQty" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Sales Amount :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_totalSalesAmount" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Amount Received :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_totalAmountReceived" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Amount Pending:</h6>
                                            <h4>
                                                <asp:Label ID="lbl_totalAmountPending" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Gross Amount:</h6>
                                            <h4>
                                                <asp:Label ID="lbl_grossAmount" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Customer Details Section End -->


            <!-- Add Payment Form Section Start -->
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <h5>Add Payment</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <!-- Payment Amount -->
                                <div class="form-group col-md-3">
                                    <label class="form-label" for="payment-amount">Payment Amount:</label>
                                    <asp:TextBox ID="TextBox_amount" CssClass="form-control" runat="server" placeholder="eg :- 0.00" type="number" step="any" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_amount" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Amount" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                </div>
                                <!-- Payment Date -->
                                <div class="form-group col-md-3">
                                    <label class="form-label" for="payment-date">Payment Date:</label>
                                    <asp:TextBox ID="TextBox_paymentDate" CssClass="form-control" runat="server" type="date" />
                                </div>
                                <!-- Payment Mode -->
                                <div class="form-group col-lg-3 ">
                                    <label class="form-label">Payment Mode:</label>
                                    <asp:DropDownList ID="ddl_paymentMode" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_paymentMode" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <!-- Reference ID -->
                                <div class="form-group col-md-3">
                                    <label class="form-label" for="name">Reference ID:</label>
                                    <asp:TextBox ID="TextBox_referenceid" runat="server" class="form-control" placeholder="Enter Reference Id" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox_referenceid" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Reference Id" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                </div>
                                <!-- Bank Name -->
                                <div class="form-group col-lg-3 ">
                                    <label class="form-label">Bank Name:</label>
                                    <asp:DropDownList ID="ddl_bankname" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddl_bankname" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <!-- Remark -->
                                <div class="form-group col-lg-3 ">
                                    <label class="form-label">Remark:</label>
                                    <asp:TextBox ID="TextBox_remark" runat="server" class="form-control" placeholder="Enter Remark" TextMode="MultiLine" />
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:Button type="button" ID="ButtonAddPayment" runat="server" CssClass="btn btn-light-primary me-2" Text="Add Payment" ValidationGroup="Group1" OnClick="ButtonAddPayment_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Add Payment Form Section End -->

        </div>
    </div>
</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
