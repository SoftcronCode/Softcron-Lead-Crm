<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssignGroupToUser.aspx.cs" Inherits="DSERP_Client_UI.AssignGroupToUser" %>

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
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Assign Group To User Master</a></li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Assign Group To User Master</h2>
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
                            <h4>Assign Group To User</h4>
                        </div>
                        <div class="card-body">
                            <asp:HiddenField ID="HiddenFieldID" runat="server" />
                            <div class="row">
                                <div class="form-group col-lg-3">
                                    <label class="form-label">User Name :</label>
                                    <asp:DropDownList ID="ddl_username" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddl_username" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-6">
                                    <label class="form-label">Group Name :</label>
                                    <div id="output"></div>
                                    <asp:ListBox ID="ddl_grouplist" runat="server" CssClass="chosen-select form-control" placeholder="Select Tag " SelectionMode="Multiple">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:ListBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_grouplist" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="card-footer bg-white border-0">
                                <asp:Button type="button" ID="ButtonSubmit" runat="server" CssClass="btn btn-light-primary me-2" Text="Add" ValidationGroup="Group1" OnClick="ButtonSubmit_Click" />
                                <asp:Button type="button" ID="ButtonUpdate" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" Visible="false" ValidationGroup="Group1" OnClick="ButtonUpdate_Click" />
                                <asp:Button type="button" ID="ButtonClear" runat="server" CssClass="btn btn-light-secondary" Text="Clear" OnClick="ButtonClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- DataTable Start -->
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
                                <asp:GridView ID="gridview" runat="server" CssClass="guestdetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" OnRowUpdating="GridView_RowUpdating" DataKeyNames="user_group_assignid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UserDisplayName" HeaderText="UserName" SortExpression="UserDisplayName" />
                                        <asp:BoundField DataField="groupname" HeaderText="Group Name" SortExpression="groupname" />




                                        <asp:TemplateField HeaderText="action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnviewreport" CssClass="me-2" runat="server" CommandName="update" CommandArgument='<%# Eval("user_group_assignid")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="edit"><i class="fa-solid fa-pen-nib"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- datatable end -->

        </div>
    </div>



</asp:Content>


<%--Content PLaceHoler For Javascript--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        document.getElementById('output').innerHTML = location.search;
        $(".chosen-select").chosen();
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            // DataTable with buttons
            var table = $(".guestdetail").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
