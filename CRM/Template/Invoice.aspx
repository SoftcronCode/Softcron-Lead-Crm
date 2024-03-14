<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="DSERP_Client_UI.Invoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice</title>
    <link rel="stylesheet" href="/assets/css/style.css" id="main_style_link" />
    <link rel="stylesheet" href="/assets/css/style-preset.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="card">
                <div class="card-body">
                    <div class="row mt-2 mb-5">
                        <div class="col-md-6">
                            <img src="/assets/img/logo.png" height="60" width="198" alt="logo" />
                        </div>
                        <div class="col-md-6">
                            <h1 class="text-end">Invoice</h1>
                        </div>
                    </div>

                    <div class="row mb-5">
                        <div class="col-md-6">
                            <div>
                                <asp:Label ID="lbl_customer_Name" runat="server" Text=""></asp:Label>
                            </div>
                            <div>Thank you for availing services from our company.</div>
                        </div>
                        <div class="col-md-6 text-end">
                            <div>
                                Invoice No. :
                                <asp:Label ID="lbl_invoiceNumber" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                                Date :
                                <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>


                    <div class="row mb-4 justify-content-between">
                        <div class="col-md-3">
                            <h6 class="mb-3">From:</h6>
                            <div><strong>Softcron Technolgy</strong></div>
                            <div>519 Gf, Omaxe City, Rohtak</div>
                            <div>Rohtak, Haryana - 124001</div>
                            <div>Info@softcro.com | 91-9044892448</div>
                            <div>GSTIN : 06AMHPdfgfdg540145</div>
                        </div>
                        <div class="col-md-3">
                            <h6 class="mb-3">Billed To:</h6>
                            <div>
                                <strong>
                                    <asp:Label ID="lbl_customerName" runat="server" Text=""></asp:Label></strong>
                            </div>
                            <div>
                                <asp:Label ID="lbl_customerAddress" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lbl_custEmail" runat="server" Text=""></asp:Label>
                                |
                                <asp:Label ID="lbl_custPhone" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                                GSTIN :
                                <asp:Label ID="lbl_custGst" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive-sm mt-5">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Item</th>
                                    <th>Validity</th>
                                    <th>Unit Price</th>
                                    <th>Qty</th>
                                    <th>Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%# Eval("service_name") %></td>
                                            <td><%# Eval("validity") %></td>
                                            <td><%# Eval("unit_price") %></td>
                                            <td><%# Eval("product_qty") %></td>
                                            <td><%# Eval("total_amount") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>

                    <div class="row mt-5">
                        <div class="col-md-8">
                            <h4>PAYMENT DETAILS</h4>
                            <div>SOFTCRON TECHNOLOGY</div>
                            <div>Punjab National Bank (PNB)</div>
                            <div>A/C No. – 0948002100019473</div>
                            <div>IFSC- PUNB0094800</div>
                            <div>PayPal – softcrontechnology@gmail.com</div>
                            <div>Online – https://www.softcron.com/checkout</div>
                        </div>
                        <div class="col-md-4 ml-auto">
                            <table class="table table-clear">
                                <tbody>
                                    <tr>
                                        <td class="text-start"><strong>SubTotal:</strong></td>
                                        <td class="text-end">
                                            <asp:Label ID="lbl_subTotal" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="text-start"><strong>Discount</strong></td>
                                        <td class="text-end">
                                            <asp:Label ID="lbl_discountAmt" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr id="gst" runat="server" style="display: none;">
                                        <td class="text-start"><strong>GST(<asp:Label ID="lbl_gstVal" runat="server" Text=""></asp:Label>%)</strong></td>
                                        <td class="text-end"> <asp:Label ID="lbl_gstAmt" runat="server" Text=""></asp:Label></td>
                                    </tr>

                                    <tr id="cgst" runat="server" style="display: none;">
                                        <td class="text-start"><strong>CGST(<asp:Label ID="lbl_cgst" runat="server" Text=""></asp:Label>%)</strong></td>
                                        <td class="text-end"><asp:Label ID="lbl_cgstAmt" runat="server" Text=""></asp:Label></td>
                                    </tr>

                                    <tr id="sgst" runat="server" style="display: none;">
                                        <td class="text-start"><strong>SGST(<asp:Label ID="lbl_sgst" runat="server" Text=""></asp:Label>%)</strong></td>
                                        <td class="text-end"><asp:Label ID="lbl_sgstAmt" runat="server" Text=""></asp:Label></td>
                                    </tr>





                                    <tr>
                                        <td class="text-start"><strong>Total</strong></td>
                                        <td class="text-end">
                                            <asp:Label ID="lbl_totalAmt" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div>
                        <p>Terms & Conditions:</p>
                        <ol type="1">
                            <li>No payment is refundable once it is credited to our account. No Cash Payment is acceptable. Cash deposit in bank account is strictly restricted from outside of Rohtak. A sum of Rs.300/- or 1% (Whichever is higher) on total transaction amount will be charge extra on your account.</li>
                            <li>All services described herein above in this invoice are digital hence cannot be delivered in physical. All the material, services and elementary information will be delivered online through respective methods on the designated URLs.</li>
                            <li>In case of digital services, the respective URLs may get changed with a prior notice to the respective client/party.</li>
                            <li>All the billed amounts shall be paid within a span of 10 working days from the date of issuing the invoice, failing after which Softcron Technology have rights to withdraw the services delivered against the respective invoice with immediate effects until the full payment of the total invoice amount.</li>
                            <li>Any kind of electronic/DD/Pay Order/Cheque payment is subject to realization and credit into our account. No product or service will be delivered unless the payment reflects in our account statement.</li>
                        </ol>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
