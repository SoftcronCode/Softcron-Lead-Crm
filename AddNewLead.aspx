<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddNewLead.aspx.cs" Inherits="DSERP_Client_UI.NewLeadCommanForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

    <div class="pc-container">
        <div class="pc-content">
            <!-- Breadcrumb Section Start -->
            <div class="page-header">
                <div class="page-block">
                    <div class="row align-items-center">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="/dashboard">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Add New Lead</a></li>
                            </ul>
                            <style>
                                .addleadform {
                                    float: right;
                                }
                            </style>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Add New Lead</h2>
                            </div>
                            <button type="button" class="btn btn-light-primary addleadform" data-bs-toggle="modal" data-bs-target="#AddLeadModal">Add New Lead </button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Breadcrumb Section End -->

            <!-- DataTable Start -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="pb---20">
                                <!-- Define your column toggle links here -->
                                <div class="pb---20">
                                    toggle column: <a class="toggle-vis toggle-anchor" data-column="0">Sr. No.</a>
                                    - <a class="toggle-vis toggle-anchor" data-column="1">Name</a>
                                    - <a class="toggle-vis toggle-anchor" data-column="2">Email</a>
                                    - <a class="toggle-vis toggle-anchor" data-column="3">Phone</a>
                                    - <a class="toggle-vis toggle-anchor" data-column="4">Service</a>
                                    - <a class="toggle-vis toggle-anchor" data-column="5">Service-Type</a>
                                    - <a class="toggle-vis toggle-anchor" data-column="6">Country</a>
                                    - <a class="toggle-vis toggle-anchor" data-column="7">Date</a>
                                </div>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="LeadDetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="new_leadid"
                                    OnRowCommand="Button_GridViewCommand" OnRowUpdating="Gridview_RowUpdating" OnRowDeleting="Gridview_RowDeleting" OnRowDataBound="gridview_RowDataBound" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LeadView" runat="server" CommandName="ViewLead" CommandArgument='<%# Eval("new_leadid")%>' ToolTip="View" Text='<%# Eval("first_name") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="email_id" HeaderText="Email" />
                                        <asp:BoundField DataField="phone_no" HeaderText="Phone" />
                                        <asp:BoundField DataField="service_name" HeaderText="Service" />
                                        <asp:BoundField DataField="service_type" HeaderText="Service Type" />
                                        <asp:BoundField DataField="country" HeaderText="Country" />
                                        <asp:BoundField DataField="created_datetime" HeaderText=" Created Date" />

                                        <asp:TemplateField HeaderText="Lead Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddl_status_option" Class="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusOption_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnviewreport" CssClass="me-2" runat="server" CommandName="Update" CommandArgument='<%# Eval("new_leadid")%>' ToolTip="Edit">
                                                        <i class="fa-solid fa-pen-nib"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="deletebutton" CssClass="me-2" runat="server" CommandName="Delete" CommandArgument='<%# Eval("new_leadid")%>' ToolTip="Delete">
                                                        <i class="fa-solid fa-trash-can"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="BtnFollowup" runat="server" CommandName="Followup" CommandArgument='<%# Eval("new_leadid")%>' ToolTip="FollowUp">
                                                        <i class="fa-solid fa-check-double"></i>
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

            <!-- Add New Lead Modal -->
            <div class="modal fade " id="AddLeadModal" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="exampleModalLabel">New Lead</h1>
                            <button type="button" id="AddLead_CloseBtn" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="row">
                                    <asp:HiddenField ID="hiddenfield_leadid" runat="server" />
                                    <asp:HiddenField ID="HiddenField_country" runat="server" />

                                    <div class="form-group col-lg-4">
                                        <label class="form-label">Lead Status:</label>
                                        <asp:DropDownList ID="ddl_leadstatus" Class="form-select" runat="server" AutoPostBack="false" ClientIDMode="AutoID"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_leadstatus" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4">
                                        <label class="form-label">Lead Source</label>
                                        <asp:DropDownList ID="ddl_leadsource" Class="form-select" runat="server" AutoPostBack="false" ClientIDMode="AutoID" OnChange="onLeadSourceChange()"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddl_leadsource" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4" runat="server" id="refered_by" style="display: none;">
                                        <label class="form-label">Refered By Name :</label>
                                        <asp:TextBox ID="TextBox_referedBy" runat="server" class="form-control" placeholder="Enter Refered By Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox_referedBy" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Name" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4">
                                        <label class="form-label">Assign</label>
                                        <asp:DropDownList ID="ddl_assign" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddl_assign" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="name">First Name :</label>
                                        <asp:TextBox ID="TextBox_fname" runat="server" class="form-control" placeholder="Enter First Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_fname" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter First Name" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="name">Last Name :</label>
                                        <asp:TextBox ID="TextBox_lname" runat="server" class="form-control" placeholder="Enter Last Name" />
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="name">Email ID :</label>
                                        <asp:TextBox ID="TextBox_email" runat="server" class="form-control" placeholder="Enter Email" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_email" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_email" ValidationGroup="Group1" ErrorMessage="Invalid email format" Display="Dynamic" ForeColor="Red" CssClass="absolute-position" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="name">Phone No. :</label>
                                        <asp:TextBox ID="TextBox_phone" runat="server" class="form-control" placeholder="Enter Phone" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox_phone" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Phone Number" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="name">Company Name:</label>
                                        <asp:TextBox ID="TextBox_companyname" runat="server" class="form-control" placeholder="Enter Company Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox_companyname" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Company Name" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label class="form-label" for="name">Website :</label>
                                        <asp:TextBox ID="TextBox_website" runat="server" class="form-control" placeholder="Enter Website" />
                                    </div>

                                    <div class="form-group col-lg-4">
                                        <label class="form-label">Service :</label>
                                        <asp:DropDownList ID="ddl_service" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddl_service" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4">
                                        <label class="form-label">Service Type :</label>
                                        <asp:DropDownList ID="ddl_serviceType" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddl_serviceType" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label class="form-label">Country</label>
                                        <asp:DropDownList ID="ddl_country" runat="server" class="form-select" AutoPostBack="false" ValidationGroup="Group1" EnableViewState="true" size="1">
                                            <asp:ListItem Value="0" Selected="True">-- Select Country --</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddl_country" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-12">
                                        <label class="form-label" for="descripition">Descripition</label>
                                        <asp:TextBox ID="TextBox_desc" class="form-control" runat="server" TextMode="MultiLine" placeholder="Enter Descripition" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button type="button" ID="ButtonConvertToCustomer" runat="server" CssClass="btn btn-light-success me-2" Text="Convert To Customer" ValidationGroup="Group1" OnClientClick="saveSelectedValues();" OnClick="ButtonConvertToCustomer_Click" Style="display: block" />
                                <asp:Button type="button" ID="ButtonSubmitLead" runat="server" CssClass="btn btn-light-primary me-2" Text="Add Lead" ValidationGroup="Group1" OnClientClick="saveSelectedValues();" OnClick="ButtonSubmitLead_Click" Style="display: block" />
                                <asp:Button type="button" ID="ButtonUpdateLead" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" ValidationGroup="Group1" OnClientClick="saveSelectedValues();" OnClick="ButtonUpdateLead_Click" Style="display: none" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- New Lead Model End -->





            <!-- FollowUp Modal Start -->
            <div class="modal fade" id="followup_details" tabindex="-1" data-bs-backdrop="static" aria-labelledby="followupModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="followupModalLabel">FollowUp Records</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <%-- timeline code   --%>
                            <style>
                                .cursor-pointer {
                                    cursor: pointer;
                                }
                            </style>

                            <div class="chat-container">
                                <div class="text-center" id="NoRecord" visible="false" runat="server">
                                    <h4>No follow-up record is found !</h4>
                                </div>
                                <div class="box" id="followUp_container" runat="server">
                                    <ul id="first-list">
                                        <asp:Repeater ID="ChatRepeater" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <span></span>
                                                    <div class="info">
                                                        <span class="pr-3"><b>Remark : </b></span><span><%# Eval("followup_text") %></span>
                                                    </div>
                                                    <div class="time">
                                                        <span data-bs-toggle="tooltip" data-bs-placement="top" title='<%# Eval("created_datetime") %>' class="cursor-pointer">
                                                            <%# CalculateTimeDifference(Eval("created_datetime")) %>
                                                        </span>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <script type="text/javascript">
                                            document.addEventListener('DOMContentLoaded', function () {
                                                var tooltips = new bootstrap.Tooltip(document.body, {
                                                    selector: '[data-bs-toggle="tooltip"]'
                                                });
                                            });
                                        </script>
                                    </ul>
                                </div>
                            </div>
                            <hr />
                            <%-- follow up text fields  --%>
                            <div class="row remark">
                                <div class="col-md-6">
                                    <span class="fw-bold">Remark</span>
                                    <asp:TextBox ID="Remark_txt" runat="server" CssClass="form-control mb-2" Rows="5"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="Remark_txt" ValidationGroup="Group2" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <span class="fw-bold">Next Follow Up </span>
                                    <asp:TextBox ID="followUpDateTime" runat="server" CssClass="form-control mb-2" Type="datetime-local"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="followUpDateTime" ValidationGroup="Group2" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-light-secondary me-2" data-bs-dismiss="modal">Close</button>
                            <asp:Button ID="Button_AddFollowUp" runat="server" CssClass="btn btn-light-primary" Text="Add FollowUp" ValidationGroup="Group2" OnClick="Button_AddFollowUp_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- FollowUp Modal End -->





            <!-- View Lead Data Modal Start -->
            <div class="modal fade" id="ViewLeadModal" tabindex="-1" aria-labelledby="ViewLeadModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-xl modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="ViewLeadModalLabel">#<asp:Label ID="lbl_lead_id" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_header_name" runat="server" Text=""></asp:Label></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <!-- Tabs Start -->
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
                        <!-- Tabs End -->
                        <div class="modal-body">
                            <div class="tab-content" id="pills-tabContent">
                                <!-- Profile Tab Start -->
                                <div class="tab-pane fade show active" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                                    <div class="container-fluid">
                                        <div class="row justify-content-between">
                                            <div class="col-lg-6 col-12">
                                                <div class="lead-info-heading">
                                                    <h4>Lead Information</h4>
                                                </div>

                                                <asp:HiddenField ID="HiddenField_lead_ID" runat="server" />

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
                                                    <div class="col-4"><span><b>Email : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Phone : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_phone" runat="server" Text=""></asp:Label>
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
                                                        <asp:Label ID="lbl_website" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Service Name : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_serviceName" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Service Type : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_serviceType" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Country: </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_country" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Remark : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_remark" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-12">
                                                <div class="lead-info-heading">
                                                    <h4>General Information</h4>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Source : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_source" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2" id="referedByName" runat="server" style="display: none !important;">
                                                    <div class="col-4"><span><b>Refered By Name : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_referedByName" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Status : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_status" runat="server" Text="" CssClass="badge bg-light-info fs-6"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Assigned : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_assignName" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Next FollowUp  : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_followupDate" runat="server" Text="" CssClass="badge bg-light-warning fs-6"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Created Date : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_createdDate" runat="server" Text="" data-bs-toggle="tooltip" data-bs-placement="top"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="d-flex p-2">
                                                    <div class="col-4"><span><b>Created By : </b></span></div>
                                                    <div class="col-8">
                                                        <asp:Label ID="lbl_createdBy" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="d-flex justify-content-end mt-3">
                                                <asp:Button type="button" ID="ButtonEditLead" runat="server" CssClass="btn btn-light-primary me-2" Text="Edit Lead" OnClick="ButtonEditLead_Click" />
                                                <asp:Button type="button" ID="ButtonCustomer" runat="server" CssClass="btn btn-light-success me-2" Text="Convert To Customer" OnClick="ButtonCustomer_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Profile Tab End -->

                                <!-- Notes Tab Start -->
                                <div class="tab-pane fade" id="pills-notes" role="tabpanel" aria-labelledby="pills-notes-tab">
                                    <div class="container-fluid">
                                        <div class="row">

                                            <asp:HiddenField ID="HiddenField_noteid" runat="server" />

                                            <div class="form-group col-12">
                                                <asp:TextBox ID="TextBox_note" runat="server" class="form-control" placeholder="Enter Note" TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox_note" ValidationGroup="Group3" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Note" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:Button type="button" ID="Button_addNote" runat="server" CssClass="btn btn-light-primary me-2 float-end" Text="Add Note" ValidationGroup="Group3" OnClick="Button_addNote_Click" />
                                        <asp:Button type="button" ID="Button_UpdateNote" runat="server" CssClass="btn btn-light-primary me-2 float-end" Text="Update Note" ValidationGroup="Group3" OnClick="Button_UpdateNote_Click" Visible="false" />
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
                                <!-- Notes Tab End -->


                                <!-- Docs Tab Start -->
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
                                                            <a href="/assets/docs/lead/<%# Eval("filename") %>" target="_blank" class="fs-5"><%# Eval("filename") %></a>
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
                                <!-- Docs Tab End -->

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- View Lead Data Modal End -->

        </div>
    </div>

</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <!-- Country Js File -->
    <script src="/assets/js/countries.js"></script>

    <script type="text/javascript">
        // DataTable Code
        $(document).ready(function () {
            var table = $(".LeadDetail").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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

    <script type="text/javascript">
        // function to show lead Modal popup.
        function showAddNewLeadModal() {
            $("#AddLeadModal").modal('show');
        }
        // function to show followup modal popup
        function showFollowUpModal() {
            $("#followup_details").modal('show');
        }
        // function to show view lead detail modal popup.
        function showViewLeadModal() {
            $("#ViewLeadModal").modal('show');
        }

        // function to Set Notes Tab Active
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




        // function to show refered by name textbox on lead source dropdown value change.
        function onLeadSourceChange() {
            var ddl = document.getElementById('<%= ddl_leadsource.ClientID %>');
            var textbox_referedBy = document.getElementById('<%= refered_by.ClientID %>');
            if (ddl.value === "10") {
                textbox_referedBy.style.display = 'block';
            }
            else {
                textbox_referedBy.style.display = 'none';
                document.getElementById('<%= TextBox_referedBy.ClientID %>').value = '';
            }
        }

        // function to save country selected value.
        function saveSelectedValues() {
            var countyDropdown = document.getElementById("<%= ddl_country.ClientID %>");
            var hiddenCounty = document.getElementById("<%= HiddenField_country.ClientID %>");

            var countryddlselectedValue = countyDropdown.value;
            var countryselectedText = countyDropdown.options[countyDropdown.selectedIndex].text;

            hiddenCounty.value = countryselectedText;
        }

        // function call when user click on lead edit button from server side .
        function BindCountryDropDown(countryValue) {
            const countrySelection = document.querySelector("#ContentPlaceHolder1_ddl_country");
            var Country_value = countryValue;

            for (let country in stateObject) {
                countrySelection.options[countrySelection.options.length] = new Option(
                    country,
                    country
                );
            }
            countrySelection.value = Country_value;
        }

        // bind country dropdown when page is load.
        window.onload = function () {
            const countrySelection = document.querySelector("#ContentPlaceHolder1_ddl_country");


            for (let country in stateObject) {
                countrySelection.options[countrySelection.options.length] = new Option(
                    country,
                    country
                );
            }
        };
    </script>






    <script>
        // Function to clear input fields
        function clearInputFields() {
            document.getElementById('<%= TextBox_referedBy.ClientID %>').value = '';
            document.getElementById('<%= TextBox_fname.ClientID %>').value = '';
            document.getElementById('<%= TextBox_lname.ClientID %>').value = '';
            document.getElementById('<%= TextBox_email.ClientID %>').value = '';
            document.getElementById('<%= TextBox_phone.ClientID %>').value = '';
            document.getElementById('<%= TextBox_companyname.ClientID %>').value = '';
            document.getElementById('<%= TextBox_website.ClientID %>').value = '';
            document.getElementById('<%= TextBox_desc.ClientID %>').value = '';


            document.getElementById('<%= ddl_leadstatus.ClientID %>').value = '0';
            document.getElementById('<%= ddl_leadsource.ClientID %>').value = '0';
            document.getElementById('<%= ddl_service.ClientID %>').value = '0';
            document.getElementById('<%= ddl_serviceType.ClientID %>').value = '0';
            document.getElementById('<%= ddl_country.ClientID %>').value = '0';

            var referedByName = document.getElementById('<%= refered_by.ClientID %>');
            referedByName.style.display = 'none';

            var buttonSubmit = document.getElementById('<%= ButtonSubmitLead.ClientID %>');
            var buttonUpdate = document.getElementById('<%= ButtonUpdateLead.ClientID %>');
            var buttonConvert = document.getElementById('<%= ButtonConvertToCustomer.ClientID %>');
            buttonSubmit.style.display = 'block';
            buttonUpdate.style.display = 'none';
            buttonConvert.style.display = 'block';
        }


        // Add an event listener to the button
        document.getElementById('AddLead_CloseBtn').addEventListener('click', function () {
            // Call the clearInputFields function when the button is clicked
            clearInputFields();
        });

    </script>

</asp:Content>




