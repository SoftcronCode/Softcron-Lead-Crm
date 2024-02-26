<%@ Page Title="" EnableEventValidation="false" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Room-Booking.aspx.cs" Inherits="DSERP_Client_UI.Room_Booking" %>

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
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Booking Master</a></li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Booking Master</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Breadcrumb Section End -->




            <style>
                * {
                    margin: 0;
                    padding: 0
                }

                html {
                    height: 100%
                }

                p {
                    color: grey
                }

                #heading {
                    text-transform: uppercase;
                    color: #673AB7;
                    font-weight: normal
                }

                #msform {
                    text-align: center;
                    position: relative;
                    margin-top: 20px
                }

                fieldset {
                    background: white;
                    border: 0 none;
                    border-radius: 0.5rem;
                    box-sizing: border-box;
                    width: 100%;
                    margin: 0;
                    padding-bottom: 20px;
                    position: relative
                }

                .form-card {
                    text-align: left
                }

                fieldset:not(:first-of-type) {
                    display: none
                }

                input,
                textarea {
                    padding: 8px 15px 8px 15px;
                    border: 1px solid #ccc;
                    border-radius: 0px;
                    margin-bottom: 25px;
                    margin-top: 2px;
                    width: 100%;
                    box-sizing: border-box;
                    font-family: montserrat;
                    color: #2C3E50;
                    background-color: #ECEFF1;
                    font-size: 16px;
                    letter-spacing: 1px
                }

                    input:focus,
                    textarea:focus {
                        -moz-box-shadow: none !important;
                        -webkit-box-shadow: none !important;
                        box-shadow: none !important;
                        border: 1px solid #673AB7;
                        outline-width: 0
                    }

                .action-button {
                    width: 100px;
                    background: #673AB7;
                    font-weight: bold;
                    color: white;
                    border: 0 none;
                    border-radius: 0px;
                    cursor: pointer;
                    padding: 10px 5px;
                    margin: 10px 0px 10px 5px;
                    float: right
                }

                    .action-button:hover,
                    .action-button:focus {
                        background-color: #311B92
                    }

                .action-button-previous {
                    width: 100px;
                    background: #616161;
                    font-weight: bold;
                    color: white;
                    border: 0 none;
                    border-radius: 0px;
                    cursor: pointer;
                    padding: 10px 5px;
                    margin: 10px 5px 10px 0px;
                    float: right
                }

                    .action-button-previous:hover,
                    .action-button-previous:focus {
                        background-color: #000000
                    }

                .card {
                    z-index: 0;
                    border: none;
                    position: relative
                }

                .fs-title {
                    font-size: 25px;
                    color: #673AB7;
                    margin-bottom: 15px;
                    font-weight: normal;
                    text-align: left
                }

                .purple-text {
                    color: #673AB7;
                    font-weight: normal
                }

                .steps {
                    font-size: 25px;
                    color: gray;
                    margin-bottom: 10px;
                    font-weight: normal;
                    text-align: right
                }

                .fieldlabels {
                    color: gray;
                    text-align: left
                }

                #progressbar {
                    margin-bottom: 30px;
                    overflow: hidden;
                    color: lightgrey
                }

                    #progressbar .active {
                        color: #673AB7
                    }

                    #progressbar li {
                        list-style-type: none;
                        font-size: 15px;
                        width: 25%;
                        float: left;
                        position: relative;
                        font-weight: 400
                    }

                    #progressbar #account:before {
                        font-family: FontAwesome;
                        content: "\f13e"
                    }

                    #progressbar #personal:before {
                        font-family: FontAwesome;
                        content: "\f007"
                    }

                    #progressbar #payment:before {
                        font-family: FontAwesome;
                        content: "\f030"
                    }

                    #progressbar #confirm:before {
                        font-family: FontAwesome;
                        content: "\f00c"
                    }

                    #progressbar li:before {
                        width: 50px;
                        height: 50px;
                        line-height: 45px;
                        display: block;
                        font-size: 20px;
                        color: #ffffff;
                        background: lightgray;
                        border-radius: 50%;
                        margin: 0 auto 10px auto;
                        padding: 2px;
                        text-align: center;
                    }

                    #progressbar li:after {
                        content: '';
                        width: 100%;
                        height: 2px;
                        background: lightgray;
                        position: absolute;
                        left: 0;
                        top: 25px;
                        z-index: -1
                    }

                    #progressbar li.active:before,
                    #progressbar li.active:after {
                        background: #673AB7
                    }

                .progress {
                    height: 20px
                }

                .progress-bar {
                    background-color: #673AB7
                }

                .fit-image {
                    width: 100%;
                    object-fit: cover
                }

               /* .form-card input {
                    padding: 8px 15px 8px 15px;
                    border: 1px solid #ccc;
                    border-radius: 0px;
                    margin-bottom: 25px;
                    margin-top: 2px;
                    width: 100%;
                    box-sizing: border-box;
                    font-family: montserrat;
                    color: #2C3E50;
                    background-color: #ECEFF1;
                    font-size: 16px;
                    letter-spacing: 1px
                }*/

                .step-hidden {
                    display: none;
                }

                .submit {
                    width: 100px;
                    background: #673AB7;
                    font-weight: bold;
                    color: white;
                    border: 0 none;
                    border-radius: 0px;
                    cursor: pointer;
                    padding: 10px 5px;
                    margin: 10px 0px 10px 5px;
                    float: right;
                    text-align: center;
                }

                .row {
                    display: flex;
                    justify-content: space-between; /* Adjust alignment as needed */
                }

                .col {
                    flex: 1;
                    margin: 5px; /* Add margin for spacing if desired */
                }

                /*search bar at the top*/
                .search-bar {
                    display: flex;
                    align-items: center;
                }

                .search-container {
                    display: flex;
                    align-items: center;
                }

                .search-input {
                    flex: 1;
                    margin: 0;
                    border: 1px solid #ccc;
                    width: 200px;
                    padding: 10px;
                    border-radius: 4px;
                }

                .search {
                    background: #673AB7;
                    font-weight: bold;
                    color: white;
                    border: none;
                    border-radius: 0px;
                    cursor: pointer;
                    padding: 10px 15px;
                    margin-left: 10px;
                    margin-top: 22px;
                }
            </style>

            <%-- form page start  here  --%>
            <div class="col-lg-6">
                <div class="search-bar">
                    <div class="search-container">
                        <%-- <input type="text" id="phone" class="search-input" placeholder="Enter Phone Number" />--%>
                        <asp:TextBox ID="TextBox_search" name="phone" class="search-input" type="text" runat="server" placeholder="Enter Phone Number"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchButton_Click" CssClass="search" />
                    </div>
                </div>
            </div>


            <div class="row justify-content-center">

                <div class="card px-0 pt-4 pb-0 mt-3 mb-3">

                    <form id="msform">
                        <!-- progressbar -->
                        <ul id="progressbar">
                            <li class="active" id="account"><strong>Personal Details</strong></li>
                            <li id="personal"><strong>Booking Details</strong></li>
                            <li id="payment"><strong>Verification Details</strong></li>
                            <li id="confirm"><strong>Payment Details</strong></li>
                        </ul>
                        <div class="progress">
                            <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                        <br>
                        <!-- fieldsets -->
                        <fieldset>
                            <div class="form-card">
                                <div class="row">
                                    <div class="col-7">
                                        <h2 class="fs-title">Personal Information:</h2>
                                    </div>
                                    <div class="col-5">
                                        <h2 class="steps">Step 1 - 4</h2>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label class="form-label">First Name :</label>
                                        <asp:TextBox ID="TextBox_firstname" name="FirstName" class="form-control" type="text" runat="server" placeholder="Enter First Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox_firstname" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-4">
                                        <label class="form-label">Last Name :</label>
                                        <asp:TextBox ID="TextBox_lastname" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Enter Last Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox_lastname" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-4">
                                        <label class="form-label">Gender </label>
                                        <asp:DropDownList ID="DropDownList_Gender" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList_Gender" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-4">
                                        <label class="form-label">Phone Number :</label>
                                        <asp:TextBox ID="TextBox_phone" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Enter Phone Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox_phone" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-4">
                                        <label class="form-label">Email :</label>
                                        <asp:TextBox ID="TextBox_email" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox_email" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ErrorMessage="Invalid email format" ForeColor="Red" ValidationGroup="Group1" SetFocusOnError="True" CssClass="absolute-position"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-lg-4">
                                        <label class="form-label">City :</label>
                                        <asp:TextBox ID="TextBox_city" runat="server" CssClass="form-control" type="text" placeholder="Enter City"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox_city" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-12">
                                        <label class="form-label">Address :</label>
                                        <asp:TextBox ID="TextBox_address" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Enter Address"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TextBox_address" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <input type="button" name="next" class="next action-button" value="Next" />
                        </fieldset>
                        <fieldset class="step-hidden">
                            <div class="form-card step2">
                                <div class="row">
                                    <div class="col-7">
                                        <h2 class="fs-title">Booking Information:</h2>
                                    </div>
                                    <div class="col-5">
                                        <h2 class="steps">Step 2 - 4</h2>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label class="form-label">Number of Persons :</label>
                                        <asp:TextBox ID="TextBox_numPersons" runat="server" CssClass="form-control" type="number" placeholder="Enter Number of Persons"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBox_numPersons" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="form-label">Number of Rooms :</label>
                                        <asp:TextBox ID="TextBox_numRooms" runat="server" CssClass="form-control" type="number" placeholder="Enter Number of Rooms"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="TextBox_numRooms" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <label class="form-label">Room Type :</label>
                                        <asp:DropDownList ID="DropDownList_Room_Type" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList_Room_Type" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="form-label">Room Category :</label>
                                        <asp:DropDownList ID="DropDownList_Room_Category" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList_Room_Category" ValidationGroup="Group3" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-6">
                                        <label class="fieldlabels" for="check_in_datetime">Check-In Date and Time: *</label>
                                        <input type="datetime-local" id="check_in_datetime" name="check_in_datetime" />
                                    </div>
                                    <div class="col-6">
                                        <label class="fieldlabels" for="check_out_datetime">Check-Out Date and Time: *</label>
                                        <input type="datetime-local" id="check_out_datetime" name="check_out_datetime" />
                                    </div>
                                </div>
                            </div>
                            <input type="button" name="next" class="next action-button" value="Next" />
                            <input type="button" name="previous" class="previous action-button-previous" value="Previous" />
                        </fieldset>
                        <fieldset class="step-hidden">
                            <div class="form-card step3">
                                <div class="row">
                                    <div class="col-7">
                                        <h2 class="fs-title">Verification Information</h2>
                                    </div>
                                    <div class="col-5">
                                        <h2 class="steps">Step 3 - 4</h2>
                                    </div>
                                </div>
                                <div class="row justify-content-start">
                                    <div class="col-lg-3">
                                        <label class="form-label">Verification Id Type</label>
                                        <asp:DropDownList ID="DropDownListVerificationType" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownListVerificationType" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="form-label">Verification Id :</label>
                                        <asp:TextBox ID="TextBox_Verification_id" name="Verification Id " class="form-control" type="text" runat="server" placeholder="Verification Identification "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox_Verification_id" ValidationGroup="Group4" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>
                            <input type="button" name="next" class="next action-button" value="Next" />
                            <input type="button" name="previous" class="previous action-button-previous" value="Previous" />
                        </fieldset>
                        <fieldset class="step-hidden">
                            <div class="form-card step4">
                                <div class="row">
                                    <div class="col-7">
                                        <h2 class="fs-title">Payment Information:</h2>
                                    </div>
                                    <div class="col-5">
                                        <h2 class="steps">Step 4 - 4</h2>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label class="form-label">Mode of Payment </label>
                                        <asp:DropDownList ID="ddl_paymentmode" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddl_paymentmode" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="form-label">Payment :</label>
                                        <asp:TextBox ID="TextBox_Payment" runat="server" CssClass="form-control" type="text" placeholder="Enter Payment"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextBox_Payment" ValidationGroup="Group4" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="form-label">Advance Payment :</label>
                                        <asp:TextBox ID="TextBox_AdvancePayment" runat="server" CssClass="form-control" type="text" placeholder="Enter Advance Payment"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextBox_AdvancePayment" ValidationGroup="Group4" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="form-label">Refrence Id :</label>
                                        <asp:TextBox ID="TextBox_refrence_id" runat="server" CssClass="form-control" type="text" placeholder="Enter Advance Payment"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="SubmitButton_Click" CssClass="next action-button" />
                            <input type="button" name="previous" class="previous action-button-previous" value="Previous" />

                        </fieldset>
                    </form>
                </div>
            </div>

        </div>

    </div>

    <script>
        $(document).ready(function () {

            var current_fs, next_fs, previous_fs; //fieldsets
            var opacity;
            var current = 1;
            var steps = $("fieldset").length;

            setProgressBar(current);

            $(".next").click(function () {

                current_fs = $(this).parent();
                next_fs = $(this).parent().next();

                //Add Class Active
                $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

                //show the next fieldset
                next_fs.show();
                //hide the current fieldset with style
                current_fs.animate({ opacity: 0 }, {
                    step: function (now) {
                        // for making fielset appear animation
                        opacity = 1 - now;

                        current_fs.css({
                            'display': 'none',
                            'position': 'relative'
                        });
                        next_fs.css({ 'opacity': opacity });
                    },
                    duration: 500
                });
                setProgressBar(++current);
            });

            $(".previous").click(function () {

                current_fs = $(this).parent();
                previous_fs = $(this).parent().prev();

                //Remove class active
                $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

                //show the previous fieldset
                previous_fs.show();

                //hide the current fieldset with style
                current_fs.animate({ opacity: 0 }, {
                    step: function (now) {
                        // for making fielset appear animation
                        opacity = 1 - now;

                        current_fs.css({
                            'display': 'none',
                            'position': 'relative'
                        });
                        previous_fs.css({ 'opacity': opacity });
                    },
                    duration: 500
                });
                setProgressBar(--current);
            });

            function setProgressBar(curStep) {
                var percent = parseFloat(100 / steps) * curStep;
                percent = percent.toFixed();
                $(".progress-bar")
                    .css("width", percent + "%")
            }

            $(".submit").click(function () {
                return false;
            })

        });
    </script>


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
