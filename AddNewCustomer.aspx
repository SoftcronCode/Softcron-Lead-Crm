<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddNewCustomer.aspx.cs" Inherits="DSERP_Client_UI.AddNewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

    <div class="pc-container">
        <div class="pc-content">


            <%-- ----------------------------------------------------------------------------------------------- --%>
            <!-- Breadcrumb Section Start -->
            <div class="page-header">
                <div class="page-block">
                    <div class="row align-items-center">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="/dashboard">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Add New Customer</a></li>
                            </ul>
                            <style>
                                .addleadform {
                                    float: right;
                                }
                            </style>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Add New Customer</h2>
                            </div>
                            <button type="button" id="AddNewCustomerBtn" class="btn btn-light-primary addleadform" data-bs-toggle="modal" data-bs-target="#AddLeadModal">Add New Customer</button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- Breadcrumb Section End -->
            <%-- ----------------------------------------------------------------------------------------------- --%>


            <!-- DataTable Start -->
            <%-- -------------------------------------------------------------------------------------------------------------- --%>
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="pb---20">
                                toggle column: <a class="toggle-vis toggle-anchor" data-column="0">Sr. No.</a>
                                - <a class="toggle-vis toggle-anchor" data-column="1">Name</a>
                                - <a class="toggle-vis toggle-anchor" data-column="2">Email</a>
                                - <a class="toggle-vis toggle-anchor" data-column="3">Phone</a>
                                - <a class="toggle-vis toggle-anchor" data-column="4">Gender</a>
                                - <a class="toggle-vis toggle-anchor" data-column="5">country</a>
                                - <a class="toggle-vis toggle-anchor" data-column="6">GSTIN</a>
                                - <a class="toggle-vis toggle-anchor" data-column="7">Date</a>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="CustomerDetails table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="customer_detailsID, new_leadid"
                                    OnRowUpdating="Gridview_RowUpdating" OnRowDeleting="gridview_RowDeleting" OnRowCommand="Button_GridViewCommand" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="customerView" runat="server" CommandName="ViewCustomer" CommandArgument='<%# Eval("customer_detailsID")%>' ToolTip="View" Text='<%# Eval("first_name") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="email_id" HeaderText="Email" SortExpression="email_id" />
                                        <asp:BoundField DataField="phone_no" HeaderText="Phone" SortExpression="phone_no" />
                                        <asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
                                        <asp:BoundField DataField="country" HeaderText="country" SortExpression="country" />
                                        <asp:BoundField DataField="gst_number" HeaderText="GSTIN" SortExpression="gst_number" />
                                        <asp:BoundField DataField="created_date" HeaderText="Created Date" SortExpression="created_date" DataFormatString="{0:yyyy-MM-dd}" />

                                        <asp:TemplateField HeaderText="action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnupdate" CssClass="me-2" runat="server" CommandName="Update" CommandArgument='<%# Eval("customer_detailsID")%>' ToolTip="Edit">
                    <i class="fa-solid fa-pen-nib"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="deletebutton" CssClass="me-2" runat="server" CommandName="Delete" CommandArgument='<%# Eval("customer_detailsID")%>' ToolTip="Delete">
                    <i class="fa-solid fa-trash-can"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnAddSales" CssClass="me-2" runat="server" CommandName="AddSales" CommandArgument='<%# Eval("customer_detailsID")%>' ToolTip="Add Sales">
                 <i class="fa-solid fa-plus"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
            <!-- datatable end -->


            <!-- --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- -->
            <!-- Add New customer Modal -->
            <div class="modal fade" id="AddLeadModal" data-bs-backdrop="static" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="exampleModalLabel">New Customer</h1>
                            <button type="button" id="AddCustomer_CloseBtn" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">

                            <%-- hidden fields --%>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <asp:HiddenField ID="HiddenField_id" runat="server" />
                                    <asp:HiddenField ID="HiddenField_country" runat="server" />
                                    <asp:HiddenField ID="HiddenField_state" runat="server" />
                                    <asp:HiddenField ID="HiddenField_city" runat="server" />
                                </div>
                            </div>
                            <div class="container-fluid">
                                <div class="row">
                                    <!-- LeadId Hidden Field -->
                                    <asp:HiddenField ID="hiddenField_leadid" runat="server" />
                                    <asp:HiddenField ID="hiddenField_customer_id" runat="server" />

                                    <!-- First Name -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="first_name">First Name :</label>
                                        <asp:TextBox ID="TextBox_firstName" runat="server" class="form-control" placeholder="Enter Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_firstName" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter First Name" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                    <!-- Last Name -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="first_name">Last Name :</label>
                                        <asp:TextBox ID="TextBox_lastName" runat="server" class="form-control" placeholder="Enter Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextBox_lastName" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Last Name" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Email -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="email">Email :</label>
                                        <asp:TextBox ID="TextBox_email" runat="server" class="form-control" placeholder="Enter Email" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox_email" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter EmailID" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_email" ValidationGroup="Group1" ErrorMessage="Invalid email format" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>

                                    <!-- Customer Phone -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="phone">Phone :</label>
                                        <asp:TextBox ID="TextBox_phone" runat="server" class="form-control" placeholder="Enter Phone" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_phone" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Phone No." ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Gender -->
                                    <div class="form-group col-lg-4">
                                        <label class="form-label">Customer Gender :</label>
                                        <asp:DropDownList ID="ddl_gender" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddl_gender" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Date Of Birth -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="dob">Date of Birth :</label>
                                        <asp:TextBox ID="TextBox_dob" runat="server" CssClass="form-control" Type="date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="TextBox_dob" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select Date" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Company Name -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="companmy_name">Company Name :</label>
                                        <asp:TextBox ID="TextBox_CompanyName" runat="server" class="form-control" placeholder="Enter Company Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox_CompanyName" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Company Name" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Website Name -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="website">Website Url :</label>
                                        <asp:TextBox ID="TextBox_websiteUrl" runat="server" class="form-control" placeholder="Enter Department Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox_websiteUrl" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Website Url" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Country -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label">Country :</label>
                                        <asp:DropDownList ID="countySel" runat="server" class="form-select" AutoPostBack="false" ValidationGroup="Group1" EnableViewState="true" size="1">
                                            <asp:ListItem Value="0" Selected="True">-- Select Country --</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <!-- State -->
                                    <div class="form-group col-md-4" id="state" runat="server">
                                        <label class="form-label">State :</label>
                                        <asp:DropDownList ID="stateSel" runat="server" class="form-select" AutoPostBack="false" size="1">
                                            <asp:ListItem Value="0" Selected="True">-- Select State --</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <!-- City -->
                                    <div class="form-group col-md-4" id="city" runat="server">
                                        <label class="form-label">City</label>
                                        <asp:DropDownList ID="districtSel" runat="server" class="form-select" AutoPostBack="false" size="1">
                                            <asp:ListItem Value="0" Selected="True">-- Select City --</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <!-- PIN -->
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="pin">PIN Code :</label>
                                        <asp:TextBox ID="TextBox_pin" runat="server" class="form-control" placeholder="Enter PIN Code" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="TextBox_pin" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Pin Code" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- address-->
                                    <div class="form-group col-12">
                                        <label class="form-label" for="name">Address :</label>
                                        <asp:TextBox ID="TextBox_address" runat="server" class="form-control" placeholder="Enter Address" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="TextBox_address" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Address" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>


                                    <%-- gstin number --%>
                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="name">GSTIN Number :</label>
                                        <asp:TextBox ID="TextBox_gst" runat="server" class="form-control" placeholder="Enter Gst" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox_gst" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Gst No." ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Currency -->
                                    <div class="form-group col-lg-4">
                                        <label class="form-label">Currency :</label>
                                        <asp:DropDownList ID="ddl_currency" Class="form-select" runat="server" AutoPostBack="false">
                                            <asp:ListItem Selected="True" Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="₹" Text="₹"></asp:ListItem>
                                            <asp:ListItem Value="$" Text="$"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddl_currency" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Password -->
                                    <div class="form-group col-lg-4">
                                        <label class="form-label" for="password">Password :</label>
                                        <asp:TextBox ID="Text_password" class="form-control" type="password" runat="server" placeholder="Password"></asp:TextBox>
                                        <i class="toggle-password fa-regular fa-eye"></i>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Text_password" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Password" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- checkbox -->
                                    <div class="form-group">
                                        <div class="form-check">
                                            <asp:CheckBox runat="server" CssClass="form-check-input" ID="CheckBox_sendWelcomeMail" />
                                            <label for="CheckBox_sendWelcomeMail">Send Welcome Mail</label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button type="button" ID="ButtonSubmitCustomer" runat="server" CssClass="btn btn-light-primary me-2" Text="Submit" ValidationGroup="Group1" OnClientClick="saveSelectedValues();" OnClick="ButtonSubmitCustomer_Click" Style="display: block" />
                            <asp:Button type="button" ID="ButtonUpdateCustomer" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" ValidationGroup="Group1" OnClientClick="saveSelectedValues();" OnClick="ButtonUpdateCustomer_Click" Style="display: none" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- New customer Model End -->
            <%-- ----------------------------------------------------------------------------------------------- --%>
        </div>
    </div>

    <%-- ----------------------------------------------------------------------------------------------- --%>








    <!-- View Customer Data Modal Start -->
    <div class="modal fade" id="ViewLeadModal" data-bs-backdrop="static" tabindex="-1" aria-labelledby="ViewLeadModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ViewLeadModalLabel">Customer View :-
                                <asp:Label ID="lbl_header_name" runat="server" Text=""></asp:Label></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="tabs">
                    <ul class="nav nav-pills" id="pills-tab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="pills-profile-tab" data-bs-toggle="pill" href="#pills-profile" role="tab" aria-controls="pills-profile" aria-selected="true">Profile</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="pills-notes-tab" data-bs-toggle="pill" href="#pills-notes" role="tab" aria-controls="pills-notes" aria-selected="false">Notes
                               <span class="badge bg-light text-dark">
                                   <asp:Label ID="lbl_notesCount" runat="server" Text=""></asp:Label></span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="pills-docs-tab" data-bs-toggle="pill" href="#pills-docs" role="tab" aria-controls="pills-docs" aria-selected="false">Docs
                                <span class="badge bg-light text-dark">
                                    <asp:Label ID="lbl_docsCount" runat="server" Text=""></asp:Label></span>
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="modal-body">
                    <div class="tab-content" id="pills-tabContent">
                        <!-- Profile Tab Start -->
                        <div class="tab-pane fade show active" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                            <div class="container-fluid">
                                <div class="row justify-content-between">
                                    <div class="col-lg-6 col-12">
                                        <div class="lead-info-heading">
                                            <h4>Customer Information</h4>
                                        </div>

                                        <asp:HiddenField ID="HiddenField_customerID" runat="server" />

                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>First Name : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_firstName" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Last Name : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_lastName" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Email ID : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_emailID" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Phone No. : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_phoneNo" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Gender : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Date Of Birth : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_dateOfBirth" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Company Name : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_companyName" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Website : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_Website" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Address : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_address" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>GST Number : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_gst" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Currency : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_currency" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-lg-6 col-12">
                                        <div class="lead-info-heading">
                                            <h4>General Information</h4>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Country : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_country" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>State : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_state" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>City : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Pin Code : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_pinCode" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Password : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_password" runat="server" Text="" CssClass="badge bg-light-info fs-6"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Created Date : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_created_date" runat="server" Text="" data-bs-toggle="tooltip" data-bs-placement="top"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="d-flex p-2">
                                            <div class="col-4"><span><b>Created By : </b></span></div>
                                            <div class="col-8">
                                                <asp:Label ID="lbl_created_by" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Profile Tab End -->


                        <!-- Notes Tab -->
                        <div class="tab-pane fade" id="pills-notes" role="tabpanel" aria-labelledby="pills-notes-tab">
                            <div class="container-fluid">
                                <div class="row">

                                    <asp:HiddenField ID="HiddenField_noteid" runat="server" />

                                    <div class="form-group col-12">
                                        <asp:TextBox ID="TextBox_note" runat="server" class="form-control" placeholder="Enter Note" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox_note" ValidationGroup="Group2" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Note" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:Button type="button" ID="Button_addNote" runat="server" CssClass="btn btn-light-primary me-2 float-end" Text="Add Note" ValidationGroup="Group2" OnClick="Button_addNote_Click" />
                                <asp:Button type="button" ID="Button_UpdateNote" runat="server" CssClass="btn btn-light-primary me-2 float-end" Text="Update Note" ValidationGroup="Group2" OnClick="Button_UpdateNote_Click" Visible="false" />
                                <div class="clearfix"></div>


                                <!-- Notes Repeater Start -->

                                <asp:Repeater ID="NotesRepeater" runat="server" OnItemCommand="NotesRepeater_ItemCommand">
                                    <ItemTemplate>
                                        <hr />
                                        <div class="media lead-note mt-3">
                                            <a href="#">
                                                <img src="https://crmdemo.wachatty.com/assets/images/user-placeholder.jpg" class="image-small float-start mright10">
                                            </a>
                                            <div class="media-body">
                                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CommandArgument='<%# Eval("notes_tableid") %>' CssClass="float-end text-danger"><i class="fa fa fa-times"></i></asp:LinkButton>
                                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" CommandArgument='<%# Eval("notes_tableid") %>' CssClass="float-end me-2"><i class="fa-regular fa-pen-to-square"></i></asp:LinkButton>
                                                <a href="#">
                                                    <h5 class="media-heading tw-font-semibold mb-0"><%# Eval("created_by") %></h5>
                                                    <span class="tw-text-sm tw-text-neutral-500">Note added: <%# Eval("created_datetime") %></span>
                                                </a>
                                                <div data-note-description="4" class="text-muted mtop10"><%# Eval("note_text") %></div>
                                            </div>
                                            <hr>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <!-- Notes Repeater End -->

                            </div>
                        </div>






                        <!-- Docs Tab -->
                        <div class="tab-pane fade" id="pills-docs" role="tabpanel" aria-labelledby="pills-docs-tab">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="input-group">
                                        <asp:FileUpload ID="FileUpload" runat="server" CssClass="form-control" aria-label="Upload" />
                                        <asp:Button ID="UploadBtn" runat="server" CssClass="btn btn-outline-secondary" Text="Upload" OnClick="UploadBtn_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Repeater ID="DocsRepeater" runat="server" OnItemCommand="DocsRepeater_ItemCommand">
                                        <ItemTemplate>
                                            <div class="d-flex notes-attachment-wrapper align-items-center mt-4">
                                                <div class="col-lg-10">
                                                    <h5 class="media-heading tw-font-semibold mb-2"><%# Eval("created_by") %> Added Docs : <%# Eval("created_datetime") %></h5>

                                                    <div class="float-start me-2"><i class="fa-solid fa-file-lines fs-4"></i></div>
                                                    <a href="/assets/docs/customer/<%# Eval("filename") %>" target="_blank" class="fs-5"><%# Eval("filename") %></a>
                                                </div>
                                                <div class="col-lg-2 text-end">
                                                    <asp:LinkButton ID="DeleteDocs" runat="server" CommandName="Delete" CommandArgument='<%# Eval("docs_tableid") %>' CssClass="text-danger"><i class="fa fa fa-times"></i></asp:LinkButton>
                                                </div>
                                                <div class="clearfix"></div>
                                                <hr>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-light-secondary me-2" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- View Lead Data Modal End -->


</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- script to load countries, state, city -->
    <script src="/assets/js/countries.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-bs-toggle="tooltip"]').tooltip();
        });
    </script>

    <%-- script to save hidden values and function call on update or sumit button click --%>
    <script type="text/javascript">
        function saveSelectedValues() {
            var countyDropdown = document.getElementById("<%= countySel.ClientID %>");
        var stateDropdown = document.getElementById("<%= stateSel.ClientID %>");
        var districtDropdown = document.getElementById("<%= districtSel.ClientID %>");

        var hiddenCounty = document.getElementById("<%= HiddenField_country.ClientID %>");
        var hiddenState = document.getElementById("<%= HiddenField_state.ClientID %>");
        var hiddenDistrict = document.getElementById("<%= HiddenField_city.ClientID %>");



            var countryddlselectedValue = countyDropdown.value;
            // Get the selected text
            var countryselectedText = countyDropdown.options[countyDropdown.selectedIndex].text;

            var stateddlselectedValue = stateDropdown.value;
            // Get the selected text
            var stateselectedText = stateDropdown.options[stateDropdown.selectedIndex].text;

            var districtddlselectedValue = districtDropdown.value;
            // Get the selected text
            var districtselectedText = districtDropdown.options[districtDropdown.selectedIndex].text;


            hiddenCounty.value = countryselectedText;
            hiddenState.value = stateselectedText;
            hiddenDistrict.value = districtselectedText;
        }

    </script>

    <!-- script to bind the country, state, city -->
    <script>
        function BindCountryDropDown(countryValue, stateValue, cityValue) {
            const countrySelection = document.querySelector("#ContentPlaceHolder1_countySel"),
                stateSelection = document.querySelector("#ContentPlaceHolder1_stateSel"),
                citySelection = document.querySelector("#ContentPlaceHolder1_districtSel");

            var Country_value = countryValue;
            var State_value = stateValue;
            var city_value = cityValue;

            if (countryValue === "India") {

                stateSelection.disabled = false;
                citySelection.disabled = false;


                for (let country in stateObject) {
                    countrySelection.options[countrySelection.options.length] = new Option(
                        country,
                        country
                    );
                }
                countrySelection.value = Country_value;

                // Clear and populate states dropdown based on the selected country
                stateSelection.length = 1;
                for (let state in stateObject[Country_value]) {
                    stateSelection.options[stateSelection.options.length] = new Option(
                        state,
                        state
                    );
                }
                stateSelection.value = State_value;

                // Clear and populate cities dropdown based on the selected country and state
                citySelection.length = 1;
                let cities = stateObject[Country_value][State_value];
                for (let i = 0; i < cities.length; i++) {
                    citySelection.options[citySelection.options.length] = new Option(
                        cities[i],
                        cities[i]
                    );
                }
                citySelection.value = city_value;
            }
            else {

                for (let country in stateObject) {
                    countrySelection.options[countrySelection.options.length] = new Option(
                        country,
                        country
                    );
                }
                countrySelection.value = Country_value;

                stateSelection.disabled = true;
                citySelection.disabled = true;
            }
        }


        window.onload = function () {
            const countrySelection = document.querySelector("#ContentPlaceHolder1_countySel"),
                stateSelection = document.querySelector("#ContentPlaceHolder1_stateSel"),
                citySelection = document.querySelector("#ContentPlaceHolder1_districtSel");


            for (let country in stateObject) {
                countrySelection.options[countrySelection.options.length] = new Option(
                    country,
                    country
                );
            }

            //todo : Country Change
            countrySelection.onchange = (e) => {
                // todo: Clear all options from State Selection
                stateSelection.length = 1; // remove all options bar first
                citySelection.length = 1; // remove all options bar first
                console.log(countrySelection.value);
                if (countrySelection.value === "India") {
                    stateSelection.disabled = false;
                    citySelection.disabled = false;

                    // todo: Load states by looping over countryStateInfo
                    for (let state in stateObject[e.target.value]) {
                        stateSelection.options[stateSelection.options.length] = new Option(
                            state,
                            state
                        );
                    }
                }
                else {
                    stateSelection.disabled = true;
                    citySelection.disabled = true;
                }



            };

            // todo : state change

            stateSelection.onchange = (e) => {

                citySelection.length = 1; // remove all options bar first

                let zips = stateObject[countrySelection.value][stateSelection.value];

                for (let i = 0; i < zips.length; i++) {
                    citySelection.options[citySelection.options.length] = new Option(
                        zips[i],
                        zips[i]
                    );
                }
            };
        };
    </script>




    <%-- ----------------------------------------------------------------------------------------------- --%>
    <%-- script from datatable  --%>
    <script type="text/javascript">
        $(document).ready(function () {
            // DataTable with buttons
            var table = $(".CustomerDetails").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'excel', 'pdf', 'print'
                ]
            });

            // Toggle column visibility on anchor click
            $('a.toggle-vis').on('click', function (e) {
                e.preventDefault();
                // Get the column API object
                var column = table.column($(this).attr('data-column'));
                // Toggle the visibility
                column.visible(!column.visible());
            });
        });
    </script>
    <%-- ----------------------------------------------------------------------------------------------- --%>



    <%-- ----------------------------------------------------------------------------------------------- --%>
    <%-- script to show model, clear fields --%>
    <script type="text/javascript">
        function showAddNewLeadModal() {
            $("#AddLeadModal").modal('show');
        }

        function showViewLeadModal() {
            $("#ViewLeadModal").modal('show');
        }

        // Function to Set Notes Tab Active
        function showNotesModal() {
            $("#ViewLeadModal").modal('show');
            $("#pills-profile-tab").removeClass("active");
            $("#pills-notes-tab").addClass("active");
            $("#pills-profile").removeClass('show active');
            $("#pills-notes").addClass('show active');
        }

        // Function to Set Docs Tab Active
        function showDocsModal() {
            $("#ViewLeadModal").modal('show');
            $("#pills-profile-tab").removeClass("active");
            $("#pills-docs-tab").addClass("active");
            $("#pills-profile").removeClass('show active');
            $("#pills-docs").addClass('show active');
        }

        $(".toggle-password").click(function () {
            $(this).toggleClass("fa-eye fa-eye-slash");
            input = $(this).parent().find("input");
            if (input.attr("type") == "password") {
                input.attr("type", "text");
            } else {
                input.attr("type", "password");
            }
        });

    </script>




    <script>
        // Function to clear input fields
        function clearInputFields() {
            document.getElementById('<%= hiddenField_leadid.ClientID %>').value = '';
        document.getElementById('<%= hiddenField_customer_id.ClientID %>').value = '';
        document.getElementById('<%= TextBox_firstName.ClientID %>').value = '';
        document.getElementById('<%= TextBox_lastName.ClientID %>').value = '';
        document.getElementById('<%= TextBox_email.ClientID %>').value = '';
        document.getElementById('<%= TextBox_phone.ClientID %>').value = '';
        document.getElementById('<%= TextBox_dob.ClientID %>').value = '';
        document.getElementById('<%= TextBox_CompanyName.ClientID %>').value = '';
        document.getElementById('<%= TextBox_websiteUrl.ClientID %>').value = '';
        document.getElementById('<%= TextBox_pin.ClientID %>').value = '';
        document.getElementById('<%= TextBox_address.ClientID %>').value = '';
        document.getElementById('<%= TextBox_gst.ClientID %>').value = '';
        document.getElementById('<%= Text_password.ClientID %>').value = '';
        document.getElementById('<%= CheckBox_sendWelcomeMail.ClientID %>').value = '';

        document.getElementById('<%= ddl_gender.ClientID %>').value = '0';
        document.getElementById('<%= countySel.ClientID %>').value = '0';
        document.getElementById('<%= stateSel.ClientID %>').value = '0';
        document.getElementById('<%= districtSel.ClientID %>').value = '0';
        document.getElementById('<%= ddl_currency.ClientID %>').value = '0';

        stateSelection = document.querySelector("#ContentPlaceHolder1_stateSel"),
            citySelection = document.querySelector("#ContentPlaceHolder1_districtSel");
        stateSelection.disabled = false;
        citySelection.disabled = false;

        var buttonSubmit = document.getElementById('<%= ButtonSubmitCustomer.ClientID %>');
        var buttonUpdate = document.getElementById('<%= ButtonUpdateCustomer.ClientID %>');
            buttonSubmit.style.display = 'block';
            buttonUpdate.style.display = 'none';
        }


        // Add an event listener to the button
        document.getElementById('AddCustomer_CloseBtn').addEventListener('click', function () {
            // Call the clearInputFields function when the button is clicked
            clearInputFields();
        });
    </script>
</asp:Content>

