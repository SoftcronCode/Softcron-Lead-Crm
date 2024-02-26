<%@ Page Title="" async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Guest-report.aspx.cs" Inherits="DSERP_Client_UI.Guest_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pc-container">
        <div class="pc-content">
            <!-- DataTable Start -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="pb---20">
                                toggle column: <a class="toggle-vis toggle-anchor" data-column="0">sr.no</a>
                                - <a class="toggle-vis toggle-anchor" data-column="1">firstname</a>
                                - <a class="toggle-vis toggle-anchor" data-column="2">lastname</a>
                                - <a class="toggle-vis toggle-anchor" data-column="3">phone</a>
                                - <a class="toggle-vis toggle-anchor" data-column="4">gender</a>
                                - <a class="toggle-vis toggle-anchor" data-column="5">email_id</a>
                                - <a class="toggle-vis toggle-anchor" data-column="6">city</a>
                                - <a class="toggle-vis toggle-anchor" data-column="7">address</a>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="guestdetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="firstname" HeaderText="Firstname " SortExpression="firstname" />
                                        <asp:BoundField DataField="lastname" HeaderText="Lastname" SortExpression="lastname" />
                                        <asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />
                                        <asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
                                        <asp:BoundField DataField="email_id" HeaderText="Email Id" SortExpression="email_id" />
                                        <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                                        <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />

                                        <%-- <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>

                                                <asp:Button ID="btnChangeStatus" runat="server" OnClick="btnChangeStatus_Click" Text='<%# Eval("is_active").ToString() == "True" ? "Active" : "De Activate"  %>' CssClass='<%# Eval("is_active").ToString() == "True" ? "status-active" : "status-deactive" %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>


                                        <%--<asp:TemplateField HeaderText="action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnviewreport" CssClass="me-2" runat="server" CommandName="update" CommandArgument='<%# Eval("room_master_tableID")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="edit"><i class="fa-solid fa-pen-nib"></i></asp:LinkButton>
                                                <asp:LinkButton ID="button_invoice" runat="server" CommandName="delete" CommandArgument='<%# Eval("room_master_tableID")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="delete"><i class="fa-solid fa-trash-can"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
