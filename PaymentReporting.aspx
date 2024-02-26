<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PaymentReporting.aspx.cs" Inherits="DSERP_Client_UI.PaymentReporting" %>

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
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Payment Report</a></li>
                            </ul>
                            <style>
                                .addleadform {
                                    float: right;
                                }
                            </style>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Payment Report</h2>
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
                                <asp:GridView ID="gridview" runat="server" CssClass="PaymentDetails table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="transition_tableID" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="customer_name" HeaderText="Customer Name" SortExpression="customer_name" />
                                        <asp:BoundField DataField="payment_amount" HeaderText="Amount" SortExpression="payment_amount" />
                                        <asp:BoundField DataField="payment_date" HeaderText="Date" SortExpression="payment_date" DataFormatString="{0:yyyy-MM-dd}" />
                                        <asp:BoundField DataField="payment_mode" HeaderText="Payment Mode" SortExpression="payment_mode" />
                                        <asp:BoundField DataField="reference_id" HeaderText="Reference ID" SortExpression="reference_id" />
                                        <asp:BoundField DataField="bank_name" HeaderText="Bank" SortExpression="bank_name" />
                                        <asp:BoundField DataField="remark" HeaderText="Remark" SortExpression="remark" />
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
            var table = $(".PaymentDetails").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                dom: 'QBfrtip',
                buttons: [
                    'copy', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>

</asp:Content>
