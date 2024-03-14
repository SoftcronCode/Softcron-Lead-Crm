<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DSERP_Client_UI.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pc-container">
        <div class="pc-content">
            <!-- Create Table Code Start -->
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <h4>
                                <label id="labeltablename" cssclass="capitalize" runat="server" text=""></label>
                                Table</h4>
                        </div>
                        <div class="card-body">
                            <div class="row align-items-center">
                                <asp:HiddenField ID="HiddenFieldID" runat="server" />
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="form-group col-md-3">
                                            <label id="label1" class="form-label"><%# Eval("DisplayName") %></label>
                                            <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" placeholder='<%# "Enter " + Eval("DisplayName") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox1" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ErrorMessage="Invalid email address" ValidationGroup="Group1" SetFocusOnError="True" CssClass="absolute-position" />
                                            <asp:DropDownList ID="DropDownList1" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList1" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                            <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="col-lg-4">
                                    <asp:Button type="button" ID="ButtonSubmit" runat="server" CssClass="btn btn-light-primary me-2" Text="Save" OnClick="SaveButton_Click" ValidationGroup="Group1" />
                                    <asp:Button type="button" ID="ButtonUpdate" runat="server" CssClass="btn btn-light-primary me-2" Text="Update" Visible="false" OnClick="UpdateButton_Click" />
                                    <asp:Button type="button" ID="ButtonClear" runat="server" CssClass="btn btn-light-secondary" Text="Cancel" OnClick="ClearButton_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Create Table Code End -->


            <!-- DataTable Start -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body" runat="server" id="cardBodyDiv">
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="GridView" runat="server" CssClass="tabledetail table table-striped table-bordered display" Width="100%" AutoGenerateColumns="false" AutoPostBack="true" OnRowUpdating="GridView_RowUpdating" OnRowDeleting="GridView_RowDeleting" OnRowDataBound="OnRowDataBound">
                                    <Columns>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="12" class="dataTables_empty">No record found</td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- DataTable End -->
        </div>
    </div>




   
</asp:Content>




<%--Content PLaceHoler For Javascript--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

     <script type="text/javascript">
         $(document).ready(function () {
             // DataTable with buttons
             var table = $(".tabledetail").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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