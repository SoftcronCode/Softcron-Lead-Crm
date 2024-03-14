<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuotationMaster.aspx.cs" Inherits="DSERP_Client_UI.QuotationMaster" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://admin.aggarwalsurgicals.com/Content/js/helpers/ckeditor/ckeditor.js"></script>
    <%--<script src="/assets/js/plugins/ckeditor.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pc-container">
        <div class="pc-content">
            <!-- Breadcrumb Section Start -->
            <%-- -------------------------------------------------------------------------------------------------------------- --%>
            <div class="page-header">
                <div class="page-block">
                    <div class="row align-items-center">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="/dashboard">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Quotation Master</a></li>
                            </ul>
                        </div>
                        <style>
                            .addquotform {
                                float: right;
                            }
                        </style>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Quotation Master</h2>
                            </div>
                            <asp:Button type="button" ID="addquotbutton" runat="server" CssClass="btn btn-light-primary addquotform" Text="Add" OnClick="addquotbutton_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Breadcrumb Section End -->
            <%-- -------------------------------------------------------------------------------------------------------------- --%>


            <!-- Add Quotation -->
            <%-- -------------------------------------------------------------------------------------------------------------- --%>
            <div class="row" id="quotaionForm" runat="server">
                <div class="col-lg-12">
                    <div class="card">

                        <div class="card-body">

                            <!-- Common Fields -->
                            <div class="row">

                                <asp:HiddenField ID="HiddenField_quotationID" runat="server" />

                                <div class="form-group col-lg-3">
                                    <label class="form-label">Service Name :</label>
                                    <asp:DropDownList ID="ddl_serviceName" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_serviceName" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3">
                                    <label class="form-label">Service Validity :</label>
                                    <asp:DropDownList ID="ddl_serviceValidity" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_serviceValidity" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label" for="name">Price :</label>
                                        <asp:TextBox ID="TextBox_price" runat="server" class="form-control" placeholder="Enter Price" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox_price" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label" for="name">Quotation Head:</label>
                                        <asp:TextBox ID="TextBox_quotationHead" runat="server" class="form-control" placeholder="Enter New Quotation Head" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox_quotationHead" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group">
                                        <label class="form-label" for="name">Quotation Text:</label>
                                        <asp:TextBox ID="TextBox_quotationText" runat="server" class="ckeditor" TextMode="Multiline" ValidateRequestMode="Disabled" />
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <asp:Button type="button" ID="ButtonSubmit" runat="server" CssClass="btn btn-light-primary me-2" Text="Submit" ValidationGroup="Group1" OnClick="ButtonSubmit_Click" />
                                <asp:Button type="button" ID="buttonUpdate" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" ValidationGroup="Group1" OnClick="buttonUpdate_Click" />
                                <asp:Button type="button" ID="ButtonClear" runat="server" CssClass="btn btn-light-secondary" Text="Clear" OnClick="ButtonClear_Click" />
                            </div>

                        </div>

                    </div>
                </div>
            </div>

            <%-- -------------------------------------------------------------------------------------------------------------- --%>


            <!-- DataTable Start -->
            <%-- -------------------------------------------------------------------------------------------------------------- --%>
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="pb---20">
                                toggle column: <a class="toggle-vis toggle-anchor" data-column="0">sr.no</a>
                                - <a class="toggle-vis toggle-anchor" data-column="1">UserGroupAssignId</a>
                                - <a class="toggle-vis toggle-anchor" data-column="2">UserName</a>
                                - <a class="toggle-vis toggle-anchor" data-column="3">GroupName</a>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="QuotationDetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="quotation_masterid" OnRowCommand="gridview_RowCommand" OnRowDeleting="gridview_RowDeleting" OnRowUpdating="gridview_RowUpdating" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="service_name" HeaderText="Service Name" SortExpression="service_name" />
                                        <asp:BoundField DataField="validity" HeaderText="Service Validity" SortExpression="validity" />
                                        <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" />
                                        <asp:BoundField DataField="quotation_head" HeaderText="Quotation Head" SortExpression="quotation_head" />

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Button ID="btnChangeStatus" runat="server" OnClick="btnChangeStatus_Click" Text='<%# Eval("is_active").ToString() == "1" ? "Active" : "De Activate"  %>' CssClass='<%# Eval("is_active").ToString() == "1" ? "status-active" : "status-deactive" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_view" runat="server" CssClass="me-2" CommandName="view" CommandArgument='<%# Eval("quotation_masterid")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="view"><i class="fas fa-eye"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btn_update" CssClass="me-2" runat="server" CommandName="update" CommandArgument='<%# Eval("quotation_masterid")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="edit"><i class="fa-solid fa-pen-nib"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btn_delete" runat="server" CommandName="delete" CommandArgument='<%# Eval("quotation_masterid")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="delete"><i class="fa-solid fa-trash-can"></i></asp:LinkButton>
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



            <%-- view quotation  --%>
            <div class="view-div" id="viewPopUp" runat="server">
                <div class="view-content">
                    <asp:Button ID="closeViewButton" runat="server" CssClass="close-button" OnClick="closeViewPopUp" Text="×" />
                    <h2>Quotation Details</h2>

                    <hr />
                    <div class="quotation">
                        <div class="quotation-item">
                            <strong>Service Name:</strong>
                            <asp:Label ID="lbl_serviceName" runat="server" />
                        </div>
                        <div class="quotation-item">
                            <strong>Service Validity:</strong>
                            <asp:Label ID="lbl_serviceValidity" runat="server" />
                        </div>
                        <div class="quotation-item">
                            <strong>Price:</strong>
                            <asp:Label ID="lbl_price" runat="server" />
                        </div>
                        <div class="quotation-item">
                            <strong>Subject:</strong>
                            <asp:Label ID="lbl_quotationHead" runat="server" />
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


</asp:Content>



<%--Content PLaceHoler For Javascript--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            // DataTable with buttons
            var table = $(".QuotationDetail").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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

</asp:Content>
