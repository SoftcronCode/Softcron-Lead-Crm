<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SendQuotation.aspx.cs" Inherits="DSERP_Client_UI.SendQuotation" %>

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
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Quotation Processing</a></li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Send Quotation</h2>
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
                                <div class="form-group col-md-3">
                                    <label class="form-label">Type</label>
                                    <asp:DropDownList ID="DropDown_type" runat="server" CssClass="form-select" AutoPostBack="false" ValidationGroup="Group1">
                                        <asp:ListItem Value="0" Text="--select--" Selected="true" disabled="disabled"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Lead"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Customer"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DropDown_type" ValidationGroup="Group1" Display="Dynamic" InitialValue="0" ErrorMessage="Select any option" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3">
                                    <div class="d-flex px-0 py-2">
                                        <input type="search" id="txtSearch" runat="server" class="form-control border-serarch shadow-none" placeholder="Search here. . .">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-light-success ms-2" OnClick="btnSearch_Click" ValidationGroup="Group1"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Search Bar End -->




            <!-- DataTable Start -->
            <div class="row" id="client_data" runat="server" style="display: none;">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="QuotationDetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="new_leadid">
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

                                        <asp:BoundField DataField="email_id" HeaderText="Email ID" SortExpression="email_id" />
                                        <asp:BoundField DataField="phone_no" HeaderText="Phone No" SortExpression="phone_no" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- datatable end -->










            <div class="row justify-content-between">
                <div class="col-md-5">
                    <div class="card">
                        <div class="card-header">
                            <h5>Send Quotation Form</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label for="Textbox_CustomerName" class="col-sm-2 col-form-label">Customer Name:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="Textbox_CustomerName" runat="server" class="form-control " placeholder="Enter Customer Name" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Textbox_CustomerName" ValidationGroup="Group2" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="error-message"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label for="Textbox_CustomerEmail" class="col-sm-2 col-form-label">Customer Email:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="Textbox_CustomerEmail" runat="server" class="form-control " placeholder="Enter Customer Email" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Textbox_CustomerEmail" ValidationGroup="Group2" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="error-message"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label for="Textbox_SellingPrice" class="col-sm-2 col-form-label">Selling Price:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="Textbox_SellingPrice" runat="server" class="form-control " placeholder="Enter Selling Price" ReadOnly="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Textbox_SellingPrice" ValidationGroup="Group2" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="error-message"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label for="ddl_DiscountType" class="col-sm-2 col-form-label">Discount Type:</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddl_DiscountType" Class="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_DiscountType_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select--" Value="" />
                                        <asp:ListItem Text="No Discount" Value="0" />
                                        <asp:ListItem Text="Fix Percentage(%)" Value="1" />
                                        <asp:ListItem Text="Fix Amount" Value="2" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_DiscountType" ValidationGroup="Group2" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-3" id="discountAmount" runat="server">
                                <label for="Textbox_discountAmount" class="col-sm-2 col-form-label">Discounted Amount:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="Textbox_discountAmount" runat="server" class="form-control " placeholder="Enter Discount Amount" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Textbox_discountAmount" ValidationGroup="Group2" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="error-message"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:Button type="button" ID="ButtonBack" runat="server" CssClass="btn btn-light-secondary me-2" Text="Back" OnClick="ButtonBack_Click" />
                            <asp:Button type="button" ID="ButtonSendQuotation" runat="server" CssClass="btn btn-light-primary me-2" Text="Submit" ValidationGroup="Group2" OnClick="ButtonSendQuotation_Click" />
                        </div>
                    </div>
                </div>
                <!-- Quotation Detail Section -->
                <div class="col-md-7">
                    <div class="card">
                        <div class="card-header">
                            <h4>Quotation Details:</h4>
                        </div>
                        <div class="card-body quotation-block">
                            <div class="quotation">
                                <div class="quotation-item">
                                    <strong>Service Name :</strong>
                                    <asp:Label ID="lbl_serviceName" runat="server" />
                                </div>
                                <div class="quotation-item">
                                    <strong>Service Validity :</strong>
                                    <asp:Label ID="lbl_validity" runat="server" />
                                </div>
                                <div class="quotation-item">
                                    <strong>Price :</strong>
                                    <asp:Label ID="lbl_price" runat="server" />
                                </div>
                                <div class="quotation-item">
                                    <strong>Subject:</strong>
                                    <asp:Label ID="lbl_quotationSubject" runat="server" />
                                </div>
                                <div class="quotation-item">
                                    <strong>Quotation Text:</strong>
                                    <asp:Label ID="lbl_quotationText" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>



<%--Content PLaceHoler For Javascript--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
