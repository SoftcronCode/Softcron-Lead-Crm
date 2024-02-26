<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SelectQuotation.aspx.cs" Inherits="DSERP_Client_UI.SelectQuotation" %>

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
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Select Quotation</a></li>
                            </ul>
                        </div>
                        <style>
                            .addquotform {
                                float: right;
                            }
                        </style>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Send Quotation</h2>
                            </div>
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
                                toggle column: <a class="toggle-vis toggle-anchor" data-column="0">Sr. No.</a>
                                - <a class="toggle-vis toggle-anchor" data-column="1">Service_name</a>
                                - <a class="toggle-vis toggle-anchor" data-column="1">Service_name</a>
                                - <a class="toggle-vis toggle-anchor" data-column="2">Validity</a>
                                - <a class="toggle-vis toggle-anchor" data-column="3">Price</a>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="QuotationDetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="quotation_masterid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="service_name" HeaderText="Service Name" SortExpression="service_name" />
                                        <asp:BoundField DataField="validity" HeaderText="Validity" SortExpression="validity" />
                                        <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" />

                                        <asp:TemplateField HeaderText="action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_fresh" runat="server" CssClass="me-2 badge bg-success" CommandName="Send" CommandArgument='<%# Eval("quotation_masterid")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send" OnCommand="btn_Send_Command">Send</asp:LinkButton>
                                                <%--<asp:LinkButton ID="btn_renew" CssClass="me-2 badge bg-secondary" runat="server" CommandName="renew" CommandArgument='<%# Eval("quotation_masterid")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="Renew" OnCommand="btn_renew_Command">Renew</asp:LinkButton>--%>
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

    <%-- scrpt for gridview features --%>
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
