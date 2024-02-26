<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="DSERP_Client_UI.CreateUser" %>

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
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Add New User</a></li>
                            </ul>
                            <style>
                                .addleadform {
                                    float: right;
                                }
                            </style>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Add New User</h2>
                            </div>
                            <button type="button" class="btn btn-light-primary addleadform" data-bs-toggle="modal" data-bs-target="#AddUserModal">Add New User </button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Breadcrumb Section End -->



            <!-- Add New Lead Modal -->
            <div class="modal fade " id="AddUserModal" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="exampleModalLabel">New User </h1>
                            <button type="button" id="AddUser_CloseBtn" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="row">
                                    <asp:HiddenField ID="hiddenfield_userid" runat="server" />

                                    <div class="form-group col-lg-6">
                                        <label class="form-label">UserName :</label>
                                        <asp:TextBox ID="TextBox_userName" runat="server" class="form-control" placeholder="Enter UserName" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_userName" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter UserName" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-6">
                                        <label class="form-label" for="name">UserDisplay Name :</label>
                                        <asp:TextBox ID="TextBox_displayName" runat="server" class="form-control" placeholder="Enter First Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_displayName" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Display Name" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-6">
                                        <label class="form-label" for="name">Email ID :</label>
                                        <asp:TextBox ID="TextBox_email" runat="server" class="form-control" placeholder="Enter Email" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox_email" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_email" ValidationGroup="Group1" ErrorMessage="Invalid email format" Display="Dynamic" ForeColor="Red" CssClass="absolute-position" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-6">
                                        <label class="form-label" for="password">Password :</label>
                                        <asp:TextBox ID="Text_password" class="form-control" type="password" runat="server" placeholder="Password"></asp:TextBox>
                                        <i class="toggle-password fa-regular fa-eye"></i>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Text_password" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter Password" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-12 mb-4">
                                        <h5 class="mt-3">Role :</h5>
                                        <hr />
                                        <div class="form-check form-check-inline">
                                            <asp:RadioButton ID="RadioButton1" runat="server" CssClass="form-check-input" GroupName="flexRadioDefault" />
                                            <label for="RadioButton1" class="form-check-label">Admin </label>
                                        </div>

                                        <div class="form-check form-check-inline">
                                            <asp:RadioButton ID="RadioButton2" runat="server" CssClass="form-check-input" GroupName="flexRadioDefault" />
                                            <label for="RadioButton2" class="form-check-label">User </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button type="button" ID="ButtonSubmitClick" runat="server" CssClass="btn btn-light-primary me-2" Text="Submit" ValidationGroup="Group1" OnClick="ButtonSubmitClick_Click" />
                                <asp:Button type="button" ID="ButtonUpdatClick" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" ValidationGroup="Group1" Visible="false" OnClick="ButtonUpdatClick_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- New Lead Model End -->



            <!-- DataTable Start -->
            <%-- -------------------------------------------------------------------------------------------------------------- --%>
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="pb---20">
                                toggle column: <a class="toggle-vis toggle-anchor" data-column="0">Sr. No.</a>
                                - <a class="toggle-vis toggle-anchor" data-column="1">UserName</a>
                                - <a class="toggle-vis toggle-anchor" data-column="2">DisplayName</a>
                                - <a class="toggle-vis toggle-anchor" data-column="3">EmailID</a>
                                - <a class="toggle-vis toggle-anchor" data-column="4">Password</a>
                                - <a class="toggle-vis toggle-anchor" data-column="5">Role</a>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="UserDetails table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="UserID"
                                    OnRowUpdating="Gridview_RowUpdating" OnRowDeleting="gridview_RowDeleting" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="AppAccessUserName" HeaderText="UserName" SortExpression="AppAccessUserName" />
                                        <asp:BoundField DataField="UserDisplayName" HeaderText="DisplayName" SortExpression="UserDisplayName" />
                                        <asp:BoundField DataField="UserEmailID" HeaderText="EmailID" SortExpression="UserEmailID" />
                                        <asp:BoundField DataField="AppAccessPWD" HeaderText="Password" SortExpression="AppAccessPWD" />
                                        <asp:BoundField DataField="UserRole" HeaderText="Role" SortExpression="UserRole" />

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Button ID="btnChangeStatus" runat="server" OnClick="btnChangeStatus_Click" Text='<%# Eval("ActiveStatus").ToString() == "YES" ? "Active" : "De Activate"  %>' CssClass='<%# Eval("ActiveStatus").ToString() == "YES" ? "status-active" : "status-deactive" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnupdate" CssClass="me-2" runat="server" CommandName="Update" CommandArgument='<%# Eval("UserID")%>' ToolTip="Edit">
                                                <i class="fa-solid fa-pen-nib"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="deletebutton" CssClass="me-2" runat="server" CommandName="Delete" CommandArgument='<%# Eval("UserID")%>' ToolTip="Delete">
                                                 <i class="fa-solid fa-trash-can"></i>
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

        </div>
    </div>






</asp:Content>



<%--Content PLaceHoler For Javascript--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript">
        // DataTable Code
        $(document).ready(function () {
            var table = $(".UserDetails").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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

    <script>

        // Add an event listener to the button
        document.getElementById('AddUser_CloseBtn').addEventListener('click', function () {
            // Call the clearInputFields function when the button is clicked
            clearInputFields();
        });


        function clearFields() {
            // Get all input elements inside the modal
            var inputs = document.querySelectorAll('#AddUserModal input[type="text"], #AddUserModal input[type="password"], #AddUserModal input[type="email"], #AddUserModal input[type="radio"]');

            // Iterate through each input element and reset its value
            inputs.forEach(function (input) {
                input.value = '';
            });
        }
    </script>

</asp:Content>
