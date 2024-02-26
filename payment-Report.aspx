<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="payment-Report.aspx.cs" Inherits="DSERP_Client_UI.payment_Report" %>

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
                                - <a class="toggle-vis toggle-anchor" data-column="1">payment_mode</a>
                                - <a class="toggle-vis toggle-anchor" data-column="2">reference_id</a>
                                - <a class="toggle-vis toggle-anchor" data-column="3">total_amount</a>
                                - <a class="toggle-vis toggle-anchor" data-column="4">advance_amount</a>
                                - <a class="toggle-vis toggle-anchor" data-column="5">date</a>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="paymentdetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="payment_mode" HeaderText="Payment Mode " SortExpression="payment_mode" />
                                        <asp:BoundField DataField="reference_id" HeaderText="Reference Id" SortExpression="reference_id" />
                                        <asp:BoundField DataField="total_amount" HeaderText="Total Amount" SortExpression="total_amount" />
                                        <asp:BoundField DataField="advance_amount" HeaderText="Advance Amount" SortExpression="advance_amount" />
                                        <asp:BoundField DataField="inserted_datetime" HeaderText="Date" SortExpression="inserted_datetime" />

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
            var table = $(".paymentdetail").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
