<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Booking-report.aspx.cs" Inherits="DSERP_Client_UI.Booking_report" %>
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
                                - <a class="toggle-vis toggle-anchor" data-column="1">room_type</a>
                                - <a class="toggle-vis toggle-anchor" data-column="2">room_category</a>
                                - <a class="toggle-vis toggle-anchor" data-column="3">no_of_person</a>
                                - <a class="toggle-vis toggle-anchor" data-column="4">no_of_rooms</a>
                                - <a class="toggle-vis toggle-anchor" data-column="5">checkin_date</a>
                                - <a class="toggle-vis toggle-anchor" data-column="6">checkout_date</a>
                                - <a class="toggle-vis toggle-anchor" data-column="7">verification_id</a>
                                - <a class="toggle-vis toggle-anchor" data-column="8">verify_id_no</a>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="bookingdetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="room_type" HeaderText="Room Type" SortExpression="room_type" />
                                        <asp:BoundField DataField="room_category" HeaderText="Room Category" SortExpression="room_category" />
                                        <asp:BoundField DataField="no_of_person" HeaderText="No Of Persons" SortExpression="no_of_person" />
                                        <asp:BoundField DataField="no_of_rooms" HeaderText="No Of Rooms" SortExpression="no_of_rooms" />
                                        <asp:BoundField DataField="checkin_datetime" HeaderText="Checkin Date" SortExpression="checkin_datetime" />
                                        <asp:BoundField DataField="checkout_datetime" HeaderText="Checkout Date" SortExpression="checkout_datetime" />
                                        <asp:BoundField DataField="verification_id_name" HeaderText="Verification ID" SortExpression="verification_id_name" />
                                        <asp:BoundField DataField="verify_id_no" HeaderText="Verification ID No." SortExpression="verify_id_no" />

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
            var table = $(".bookingdetail").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
