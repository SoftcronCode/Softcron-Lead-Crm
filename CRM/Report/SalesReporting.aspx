<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SalesReporting.aspx.cs" Inherits="DSERP_Client_UI.SalesReporting" %>

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
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Sales Report</a></li>
                            </ul>
                            <style>
                                .addleadform {
                                    float: right;
                                }
                            </style>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Sales Report</h2>
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
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="SalesDetails table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="sales_itemId" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="customer_name" HeaderText="Customer Name" SortExpression="customer_name" />
                                        <asp:BoundField DataField="service_name" HeaderText="Service Name" SortExpression="service_name" />
                                        <asp:BoundField DataField="validity" HeaderText="Class" SortExpression="validity" />
                                        <asp:BoundField DataField="product_qty" HeaderText="Quantity" SortExpression="product_qty" />
                                        <asp:BoundField DataField="unit_price" HeaderText="Unit Price" SortExpression="unit_price" />
                                        <asp:BoundField DataField="total_amount" HeaderText="Total" SortExpression="total_amount" />
                                        <asp:BoundField DataField="created_datetime" HeaderText="Date" SortExpression="created_datetime" DataFormatString="{0:yyyy-MM-dd}" />
                                    </Columns>

                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="12" class="dataTables_empty text-center">No record found!</td>
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

    <!-- script from datatable  -->
    <script type="text/javascript">
        $(document).ready(function () {
            // DataTable with buttons
            var table = $(".SalesDetails").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                dom: 'QBfrtip',
                buttons: [
                    'copy', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>

</asp:Content>
