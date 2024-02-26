<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="DSERP_Client_UI.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/js-cookie/2.0.4/js.cookie.min.js"></script>
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
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Add Sales</a></li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Customer Sales</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Breadcrumb Section End -->

            <!-- Search Bar Start -->
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row align-items-center">
                                <div class="col-sm-4">
                                    <div class="d-flex px-3 py-2">
                                        <input type="search" id="txtSearch" runat="server" class="form-control border-serarch shadow-none" placeholder="Search here. . .">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-light-success ms-2" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <div class="col-sm-8 text-sm-end mt-3 mt-sm-0">
                                    <button type="button" class="btn btn-light-primary" onclick="redirectToAddCustomerPage()">Add New Customer</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Search Bar End -->


            <!-- Search Table Start -->
            <div class="row" id="customer_data" runat="server" style="display: none;">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive dt-responsive">
                                <asp:GridView ID="gridview" runat="server" CssClass="QuotationDetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="customer_detailsID" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="name_click" runat="server" CommandName="details" CommandArgument='<%# Eval("customer_detailsID")%>' OnClick="OnCustomerSelectClick" Text='<%# Eval("first_name")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="email_id" HeaderText="Email" SortExpression="email_id" />
                                        <asp:BoundField DataField="phone_no" HeaderText="Phone" SortExpression="phone_no" />
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
            <!-- Search Table end -->


            <!-- Customer Details Section Start -->
            <div class="row" id="customer_details" runat="server" style="display: none;">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5>Customer Details</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">

                                <asp:HiddenField ID="hiddenField_customerid" runat="server" />

                                <div class="col-md-6 col-xl-3">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Customer Name :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_customerName" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-3">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Customer Email :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_customerEmail" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-3">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Customer Phone :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_customerPhone" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-3">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Customer Country :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_country" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Quantity Purchase :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_salesQty" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Sales Amount :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_totalSalesAmount" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Amount Received :</h6>
                                            <h4>
                                                <asp:Label ID="lbl_totalAmountReceived" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Amount Pending:</h6>
                                            <h4>
                                                <asp:Label ID="lbl_totalAmountPending" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xl-2">
                                    <div class="card">
                                        <div class="card-body p-3">
                                            <h6 class="mb-2 f-w-400 text-muted">Total Gross Amount:</h6>
                                            <h4>
                                                <asp:Label ID="lbl_grossAmount" runat="server" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Customer Details Section End -->








            <!-- Add Sales Table Start -->
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <h5>Add Sales Form</h5>
                        </div>
                        <div class="card-body">
                            <div class="row pb-5">
                                <div class="form-group col-md-3">
                                    <label class="form-label" for="po-number">PO Number:</label>
                                    <asp:TextBox ID="TextBox_ponumber" runat="server" CssClass="form-control" placeholder="Enter PO Number" />
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="form-label" for="po-date">PO Date:</label>
                                    <asp:TextBox ID="TextBox_podate" CssClass="form-control" runat="server" type="date" />
                                </div>
                                <div class="form-group col-lg-3 ">
                                    <label class="form-label">Invoice Status:</label>
                                    <asp:DropDownList ID="ddl_invoicestatus" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="form-label" for="po-date">Invoice Number:</label>
                                    <asp:TextBox ID="TextBox_invoice_number" CssClass="form-control" runat="server" placeholder="Enter Invoice Number" />
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="form-label" for="po-date">Invoice Date:</label>
                                    <asp:TextBox ID="TextBox_invoicedate" CssClass="form-control" runat="server" type="date" />
                                </div>
                            </div>

                            <table class="table bottom-border" id="maintable">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Validity</th>
                                        <th>Quantity</th>
                                        <th>Unit Price</th>
                                        <th>Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="data-contact-person last-tr">
                                        <td>
                                            <select name="p_name1" class="form-select product_name" /></td>
                                        <td>
                                            <select name="p_validity1" class="form-select product_validity"></select></td>
                                        <td>
                                            <input type="text" name="p_qty1" class="form-control product_qty" /></td>
                                        <td>
                                            <input type="text" name="p_price1" class="form-control product_price" /></td>
                                        <td>
                                            <input type="text" name="total1" class="form-control total" readonly="readonly" /></td>
                                        <td>
                                            <button type="button" id="btnAdd" class="classAdd btn btn-icon btn-light-primary"><i class="fa-solid fa-plus"></i></button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="row justify-content-end">
                                <div class="col-md-8">
                                    <table class="table bottom-border text-end">
                                        <tbody>
                                            <tr>
                                                <td><strong>Sub Total :</strong></td>
                                                <td><span class="subtotal-value">₹0.00</span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <strong>Discount</strong>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="input-group">
                                                                <input type="text" id="discount-input" class="form-control input-discount" aria-label="Text input with dropdown button">
                                                                <button id="dropdownButton" class="btn btn-outline-success dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">%</button>
                                                                <ul class="dropdown-menu dropdown-menu-end">
                                                                    <li><a class="dropdown-item" href="#" data-value="%">%</a></li>
                                                                    <li><a class="dropdown-item" href="#" data-value="Fixed Amount">Fixed Amount</a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td><span class="discount-value">- ₹0.00</span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <strong>GST</strong>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="input-group">
                                                                <input type="text" id="gst-input" class="form-control input-discount gst-text" aria-label="Text input with button">
                                                                <button id="gst" class="btn btn-outline-success" type="button">%</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td><span class="gst-value">₹0.00</span></td>
                                            </tr>
                                            <tr class="last-tr">
                                                <td><strong>Total :</strong></td>
                                                <td><span class="total-value">₹0.00</span></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer card-footer-btn">
                            <button class="btn btn-light-secondary me-2" id="submitAndPayment">Submit And Payment</button>
                            <button class="btn btn-light-primary me-2" id="btnSubmit">Submit</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Add Sales Table End -->






        </div>
    </div>

</asp:Content>




<%--Content PLaceHoler For Javascript--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <!-- Add Sales Form Script Start -->
    <script type="text/javascript">

        // Add an event listener for the 'load' event
        window.addEventListener('load', function () {
            BindServiceNameDropdown(1);
            BindValidityDropdown(1);
        });


        // generate the row on (+) Button Click
        $(document).ready(function () {
            $(document).on("click", ".classAdd", function () { //
                var rowCount = $('.data-contact-person').length + 1;
                var contactdiv = '<tr class="data-contact-person">' +
                    '<td><select name="p_name' + rowCount + '" class="form-select product_name" ClientIDMode="AutoID" OnChange="onProductNameChange(' + rowCount + ')"/></td>' +
                    '<td><select name="p_validity' + rowCount + '"  class="form-select product_validity"/></td>' +
                    '<td><input type="text" name="p_qty' + rowCount + '" class="form-control product_qty" /></td>' +
                    '<td><input type="text" name="p_price' + rowCount + '" class="form-control product_price" /></td>' +
                    '<td><input type="text" name="total' + rowCount + '" class="form-control total" readonly="readonly" /></td>' +
                    '<td><button type="button" id="btnDelete" class="deleteContact btn btn-icon btn-light-secondary"><i class="fa-solid fa-minus"></i></button></td>' +
                    '</tr>';
                $('#maintable').append(contactdiv); // Adding these controls to Main table class
                // Call the function to populate the new dropdown
                BindServiceNameDropdown(rowCount);
                BindValidityDropdown(rowCount);
            });

            // Calculate the total amount based on the price and quantity inputs
            $('#maintable').on('input', 'input[name^="p_qty"], input[name^="p_price"]', function () {
                // Get the row number from the input name attribute
                var rowCount = $(this).attr('name').match(/\d+/)[0];

                // Get the quantity and price values
                var quantity = parseFloat($('input[name="p_qty' + rowCount + '"]').val()) || 0;
                var price = parseFloat($('input[name="p_price' + rowCount + '"]').val()) || 0;

                // Calculate the total
                var total = quantity * price;

                // Set the calculated total to the total input
                $('input[name="total' + rowCount + '"]').val(total.toFixed(2));

                updateSubTotal();
            });

            function updateSubTotal() {
                var subTotal = 0;
                // Loop through each row with class 'data-contact-person'
                $('.data-contact-person').each(function () {
                    // Get the row number from the row class
                    var rowCount = $(this).index() + 1;

                    // Get the total value of the current row
                    var rowTotal = parseFloat($('input[name="total' + rowCount + '"]').val()) || 0;

                    // Add the total value to the subtotal
                    subTotal += rowTotal;
                });

                // Set the calculated subtotal to the Sub Total field
                $('.subtotal-value').text('₹' + subTotal.toFixed(2));
                updateDiscountValue();
                CalculateFinalAmount();
            }

            // run when click on (-) button
            $('#maintable').on("click", ".deleteContact", function () {
                var rowCount = $(this).closest('tr').index() + 1;
                $(this).closest('tr').remove();

                $('.data-contact-person').each(function (index) {
                    var newRowCount = index + 1;
                    $(this).find('input, select').each(function () {
                        var currentName = $(this).attr('name');
                        var newName = currentName.replace(/\d+/, newRowCount);
                        $(this).attr('name', newName);
                    });
                });

                updateSubTotal();
            });

            // Call the updateSubTotal function initially to set the initial subtotal
            updateSubTotal();



            // Set the default button text to the discount dropdown.
            var defaultText = $('.dropdown-item:first-child').data('value');
            $('#dropdownButton').text(defaultText);

            // Update the discount-value when the discount input or dropdown changes
            function updateDiscountValue() {
                var subTotal = parseFloat($('.subtotal-value').text().replace('₹', '')) || 0;
                var discountInput = parseFloat($('.input-discount').val()) || 0;
                var selectedDiscountType = $('#dropdownButton').text();

                var discountValue = 0;

                if (selectedDiscountType === '%') {
                    discountValue = (subTotal * discountInput) / 100;
                } else if (selectedDiscountType === 'Fixed Amount') {
                    discountValue = discountInput;
                }

                $('.discount-value').text('- ₹' + discountValue.toFixed(2));
                CalculateGSTAmount();
                CalculateFinalAmount();
            }

            // Update the button text based on the selected value in the dropdown
            $('.dropdown-item').on('click', function () {
                var selectedValue = $(this).data('value');
                $('#dropdownButton').text(selectedValue);
                updateDiscountValue();
            });

            // Attach the updateDiscountValue function to the input and dropdown change events
            $('.input-discount, #dropdownButton').on('input change', function () {
                updateDiscountValue();
            });

            // Calculate the GST Amount 
            function CalculateGSTAmount() {
                var subTotalAmount = parseFloat($('.subtotal-value').text().replace('₹', '')) || 0;
                var discountAmount = parseFloat($('.discount-value').text().replace('₹', '')) || 0;
                var gstPercent = parseFloat($('.gst-text').val()) || 0;
                var finalAmount = subTotalAmount - discountAmount;
                var gstAmount = (finalAmount * gstPercent) / 100;
                $('.gst-value').text('₹' + gstAmount.toFixed(2));
                CalculateFinalAmount();
            }

            $(document).on('input change', '.gst-text', function () {
                CalculateGSTAmount();
            });

            // Calculate the final total Amount To be Paid.
            function CalculateFinalAmount() {
                var subTotalAmount = parseFloat($('.subtotal-value').text().replace('₹', '')) || 0;
                var discountAmount = parseFloat($('.discount-value').text().replace('- ₹', '')) || 0;
                var gstAmount = parseFloat($('.gst-value').text().replace('₹', '')) || 0;
                var FinalAmount = (subTotalAmount - discountAmount) + gstAmount;
                $('.total-value').text('₹' + FinalAmount.toFixed(2));
            }
        });


        // Bind Product Name DropDown
        function BindServiceNameDropdown(rowCount) {
            var ddl_productname = $("select[name='p_name" + rowCount + "']");
            var jsonData = <%= Session["Service_Name_Data"] %>;

            ddl_productname.empty();    // Add an option with value 0 and text --select--            
            var defaultOption = $('<option value="0">--select--</option>');
            ddl_productname.append(defaultOption);

            // Populate the dropdown with data
            jsonData.forEach(function (item) {
                var option = $('<option></option>').attr('value', item.Service_masterID).text(item.Service_name);
                ddl_productname.append(option);
            });
        }

        // Bind Product Validity DropDown
        function BindValidityDropdown(rowCount) {
            var ddl_validity = $("select[name='p_validity" + rowCount + "']");
            var jsonData1 = <%= Session["Service_Validity_Data"] %>;
            console.log(jsonData1);

            ddl_validity.empty();        // Add an option with value 0 and text --select--            
            var defaultOption = $('<option value="0">--select--</option>');
            ddl_validity.append(defaultOption);

            // Populate the dropdown with data
            jsonData1.forEach(function (item) {
                var option = $('<option></option>').attr('value', item.service_validityID).text(item.validity);
                ddl_validity.append(option);
            });
        }









        // function to check validation for details inpur fields.
        function CheckValidation() {
            var CustId = document.getElementById('<%= hiddenField_customerid.ClientID %>').value;
            var poNumberTextbox = document.getElementById('<%= TextBox_ponumber.ClientID %>');
            var poNumber = poNumberTextbox.value;
            var poDateTextbox = document.getElementById('<%= TextBox_podate.ClientID %>');
            var poDate = poDateTextbox.value;
            var invoiceStatusDropDown = document.getElementById('<%= ddl_invoicestatus.ClientID %>');
            var invoiceStatus = invoiceStatusDropDown.value;
            var invoiceNumberTextBox = document.getElementById('<%= TextBox_invoice_number.ClientID %>');
            var invoiceNumber = invoiceNumberTextBox.value;
            var invoiceDateTextBox = document.getElementById('<%= TextBox_invoicedate.ClientID %>');
            var invoiceDate = invoiceDateTextBox.value;
            var discountInput = document.getElementById('discount-input');
            var discountValue = discountInput.value;
            var gstInput = document.getElementById('gst-input');
            var gstValue = gstInput.value;


            var hasError = false;

            if (CustId === '') {
                warning("Please Select a Customer First");
                hasError = true;
                // return;
            }

            hasError = poNumber === '' ? (poNumberTextbox.classList.add('is-invalid'), true) : (poNumberTextbox.classList.remove('is-invalid'), false);
            hasError = poDate === '' ? (poDateTextbox.classList.add('is-invalid'), true) : (poDateTextbox.classList.remove('is-invalid'), false);
            hasError = invoiceStatus === '0' ? (invoiceStatusDropDown.classList.add('is-invalid'), true) : (invoiceStatusDropDown.classList.remove('is-invalid'), false);
            hasError = invoiceNumber === '' ? (invoiceNumberTextBox.classList.add('is-invalid'), true) : (invoiceNumberTextBox.classList.remove('is-invalid'), false);
            hasError = invoiceDate === '' ? (invoiceDateTextBox.classList.add('is-invalid'), true) : (invoiceDateTextBox.classList.remove('is-invalid'), false);
            hasError = discountValue === '' ? (discountInput.classList.add('is-invalid'), true) : (discountInput.classList.remove('is-invalid'), false);
            hasError = gstValue === '' ? (gstInput.classList.add('is-invalid'), true) : (gstInput.classList.remove('is-invalid'), false);

            return hasError;
        }

        // function to check validation for customer sales input fields.
        function validateForm() {
            var isValid = true;

            $('#maintable input, #maintable select').each(function () {
                if ($(this).val() === '' || $(this).val() === '0') {
                    $(this).addClass('is-invalid');
                    isValid = false;
                } else {
                    $(this).removeClass('is-invalid');
                }
            });

            return isValid;
        }



        $(document).ready(function () {
            $("#btnSubmit, #submitAndPayment").click(function (event) {

                showPreloader();

                var isSubmitAndPayment = $(this).attr('id') === 'submitAndPayment';

                event.preventDefault();

                var isValid = validateForm();
                var hasError = CheckValidation();

                if (isValid && !hasError) {



                    var customerID = document.getElementById('<%= hiddenField_customerid.ClientID %>').value;
                    var poNumber = document.getElementById('<%= TextBox_ponumber.ClientID %>').value;
                    var poDateValue = document.getElementById('<%= TextBox_podate.ClientID %>').value;
                    var invoiceStatus = document.getElementById('<%= ddl_invoicestatus.ClientID %>').value;
                    var invoiceNumber = document.getElementById('<%= TextBox_invoice_number.ClientID %>').value;
                    var invoiceDateValue = document.getElementById('<%= TextBox_invoicedate.ClientID %>').value;
                    var subtotalValue = document.querySelector('.subtotal-value').innerText.replace('₹', '');;
                    var discountValue = document.querySelector('.input-discount').value;
                    var discountbuttonText = document.getElementById('dropdownButton').innerText;
                    var discountAmount = document.querySelector('.discount-value').innerText.replace('- ₹', '');;
                    var gstPercentValue = document.querySelector('.gst-text').value;
                    var gstValue = document.querySelector('.gst-value').innerText.replace('₹', '');;
                    var totalValue = document.querySelector('.total-value').innerText.replace('₹', '');


                    var poDate = new Date(poDateValue);
                    if (!isNaN(poDate.getTime())) {
                        // Extract day, month, and year components
                        var day = poDate.getDate().toString().padStart(2, '0');
                        var month = (poDate.getMonth() + 1).toString().padStart(2, '0'); // Note: Months are zero-based
                        var year = poDate.getFullYear();

                        // Create the formatted date string
                        var formattedPoDate = day + '-' + month + '-' + year;

                    }
                    else {
                        warning("Invalid invoiceDate format");                       
                        hidePreloader();
                        return;
                    }


                    var invoiceDate = new Date(invoiceDateValue);
                    if (!isNaN(invoiceDate.getTime())) {
                        // Extract day, month, and year components
                        var day = invoiceDate.getDate().toString().padStart(2, '0');
                        var month = (invoiceDate.getMonth() + 1).toString().padStart(2, '0'); // Note: Months are zero-based
                        var year = invoiceDate.getFullYear();

                        // Create the formatted date string
                        var formattedInvoiceDate = day + '-' + month + '-' + year;

                    } else {
                        warning("Invalid invoiceDate format");                        
                        hidePreloader();
                        return;
                    }

                    var apiUrl = "https://localhost:44338/ERP/Setup/OLDExecute";
                    var userID = Cookies.get("userid");
                    var ipAddress = "<%= Session["ipAddress"] %>";

                    var columnData = [
                        { columnValue: customerID, columnName: "customer_detailsID", columnDataType: "105002" },
                        { columnValue: poNumber, columnName: "po_number", columnDataType: "105001" },
                        { columnValue: formattedPoDate, columnName: "po_date", columnDataType: "105003" },
                        { columnValue: invoiceStatus, columnName: "invoice_status_masterid", columnDataType: "105002" },
                        { columnValue: invoiceNumber, columnName: "invoice_number", columnDataType: "105001" },
                        { columnValue: formattedInvoiceDate, columnName: "invoice_date", columnDataType: "105003" },
                        { columnValue: subtotalValue, columnName: "sub_total", columnDataType: "105005" },
                        { columnValue: discountValue, columnName: "discount", columnDataType: "105002" },
                        { columnValue: discountbuttonText, columnName: "discount_type", columnDataType: "105001" },
                        { columnValue: discountAmount, columnName: "discount_amount", columnDataType: "105005" },
                        { columnValue: gstPercentValue, columnName: "gst_percentage", columnDataType: "105002" },
                        { columnValue: gstValue, columnName: "gst_amount", columnDataType: "105005" },
                        { columnValue: totalValue, columnName: "final_total", columnDataType: "105005" },
                        { columnValue: userID, columnName: "created_by", columnDataType: "105002" }
                    ];


                    var data = {
                        tableName: "Sales_table",
                        action: "INSERT",
                        id: 0,
                        primaryColumn: "string",
                        primarydatatype: "string",
                        primaryColumnValue: 0,
                        columns: columnData,
                        objCommon: {
                            insertedUserID: userID,
                            insertedIPAddress: ipAddress,
                            dateShort: "dd-MM-yyyy",
                            dateLong: "dd-MM-yyyy- HH:mm:ss"
                        }
                    };


                    // Make the AJAX request
                    $.ajax({
                        url: apiUrl,
                        type: "POST",
                        contentType: "application/json",
                        data: JSON.stringify(data),
                        dataType: "json",
                        success: function (response) {
                            // console.log(response);
                            if (response.responseCode === 1) {
                                if (response.responseObject !== null) {
                                    var lastInsertedID = response.responseObject[0].lastInsertedID;
                                    var salesId = String(lastInsertedID);
                                    SaveSalesItem(salesId, isSubmitAndPayment);
                                }
                                else {
                                    error("Error: Empty responseObject");
                                    hidePreloader();
                                }
                            }
                            else {
                                error("Error:" + response.responseMessage);
                                hidePreloader();
                            }
                        },
                        error: function (error) {
                            error("Error :" + JSON.stringify(error));
                            hidePreloader();
                        }

                    });
                }
                else {

                    event.preventDefault();
                    warning("Please fill in all required fields.");
                    hidePreloader();
                    return;

                }
            });
        });



        // Get all the Sales data
        function getAllSalesItemData() {
            try {
                var data = [];
                $('tr.data-contact-person').each(function () {
                    var ProductName = $(this).find('.product_name').val();
                    var ProductValidity = $(this).find('.product_validity').val();
                    var ProductQty = $(this).find('.product_qty').val();
                    var ProductPrice = $(this).find('.product_price').val();
                    var Total = $(this).find('.total').val();


                    var alldata = {
                        'ProductNameId': ProductName,
                        'ProductValidityId': ProductValidity,
                        'ProductQuantity': ProductQty,
                        'ProductPrice': ProductPrice,
                        'Total': Total
                    };
                    data.push(alldata);
                });
                return data;

            } catch (error) {
                // error is a function in toaster.js file to display error
                error('An error occurred:', error);
                throw error;
            }
        }





        function SaveSalesItem(salesId, isSubmitAndPayment) {

            var Sales_id = String(salesId);
            var salesItemArray;
            try {
                salesItemArray = getAllSalesItemData();
                console.log(salesItemArray);
            }
            catch (error) {
                // error is a function in toaster.js file to display error
                error('Error in getAllSalesItemData:', error);
                hidePreloader();
                return;
            }

            //  var salesItemArray = getAllSalesItemData();
            var apiUrl = "https://localhost:44338/ERP/Setup/OLDExecute";
            var userID = Cookies.get("userid");
            var ipAddress = "<%= Session["ipAddress"] %>";


            // Loop through each item in the JSON array
            var promises = salesItemArray.map(function (salesData) {
                var columnData = [
                    { columnValue: Sales_id, columnName: "sales_tableID", columnDataType: "105002" },
                    { columnValue: salesData.ProductNameId, columnName: "service_masterid", columnDataType: "105002" },
                    { columnValue: salesData.ProductValidityId, columnName: "service_validityid", columnDataType: "105002" },
                    { columnValue: salesData.ProductQuantity, columnName: "product_qty", columnDataType: "105002" },
                    { columnValue: salesData.ProductPrice, columnName: "unit_price", columnDataType: "105005" },
                    { columnValue: salesData.Total, columnName: "total_amount", columnDataType: "105005" },
                    { columnValue: userID, columnName: "created_by", columnDataType: "105002" }
                ];

                var data = {
                    tableName: "sales_item",
                    action: "INSERT",
                    id: 0,
                    primaryColumn: "string",
                    primarydatatype: "string",
                    primaryColumnValue: 0,
                    columns: columnData,
                    objCommon: {
                        insertedUserID: userID,
                        insertedIPAddress: ipAddress,
                        dateShort: "dd-MM-yyyy",
                        dateLong: "dd-MM-yyyy- HH:mm:ss"
                    }
                };


                return $.ajax({
                    type: "POST",
                    url: apiUrl,
                    contentType: "application/json",
                    data: JSON.stringify(data)
                });
            });


            Promise.all(promises)
                .then(function (responses) {
                    // console.log(responses);
                    var allResponseCodes = responses.map(response => response.responseCode);
                    if (allResponseCodes.every(code => code === 1)) {

                        // success is a function in toastr.js file.
                        success(responses[0].responseMessage);
                        alert(responses[0].responseMessage);
                        //  console.log(isSubmitAndPayment);
                        if (isSubmitAndPayment) {
                            var customerID = document.getElementById('<%= hiddenField_customerid.ClientID %>').value;
                            window.location.href = "/add-payment?cid={" + customerID + "}";
                        } else {
                            //location.reload(true);
                            var customerID = document.getElementById('<%= hiddenField_customerid.ClientID %>').value;
                            var saleid = String(Sales_id);
                            window.location.href = "/invoice?cid=" + customerID + "&sid=" + saleid +"";
                        }
                    }
                    else {
                        // Find the first response with responseCode !== 1
                        var failedResponse = responses.find(response => response.responseCode !== 1);
                        // error is a function in toastr.js file.
                        error("Some rows failed to insert. Message: " + failedResponse.responseMessage);
                        hidePreloader();
                    }
                })
                .catch(function (error) {
                    hidePreloader();
                    // error is a function in toastr.js file.
                   // error("Error saving data: " + error);
                    alert("Error : " + error);
                });
        }
    </script>
    <!-- Add Sales Form Script End -->



    <script>
        function redirectToAddCustomerPage() {
            window.location.href = '/add-new-customer?openModal=true';
        }
    </script>

</asp:Content>
