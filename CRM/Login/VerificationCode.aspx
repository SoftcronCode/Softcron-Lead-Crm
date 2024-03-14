<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="VerificationCode.aspx.cs" Inherits="DSERP_Client_UI.View.Login.VereficationCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OTP Verification </title>
    <!-- [Font] Family -->
    <link rel="stylesheet" href="/assets/css/inter.css" id="main_font_link" />
    <!-- [Tabler Icons] https://tablericons.com -->
    <link rel="stylesheet" href="/assets/css/tabler-icons.min.css" />
    <!-- [Feather Icons] https://feathericons.com -->
    <link rel="stylesheet" href="/assets/css/feather.css" />
    <!-- [Font Awesome Icons] https://fontawesome.com/icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <!-- [Template CSS Files] -->
    <link rel="stylesheet" href="/assets/css/style.css" id="main_style_link" />
    <link rel="stylesheet" href="/assets/css/style-preset.css" />
    <!-- [Toastr Files] -->
    <link media="screen" rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/jquery/1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <script type="text/javascript" src="/assets/js/toastr-msg.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="auth-main">
            <div class="auth-wrapper v1">
                <div class="auth-form">
                    <div class="card my-5">
                        <div class="card-body">
                            <div class="mb-4">
                                <div class="text-center mb-5">
                                    <a href="#">
                                        <%--<img src="/assets/img/logo.png" alt="logo" height="60" width="190" />--%>
                                        <img src="/assets/img/getmyca-logo.png" alt="logo" height="70" width="150" />
                                    </a>
                                </div>
                                <h3 class="mb-2 text-center"><b>Enter Verification Code</b></h3>
                                <p class="text-center">We`ve send you code on
                                    <asp:Label ID="lbl_emailid" runat="server" Text=""></asp:Label></p>
                            </div>
                            <div id="otp" class="inputs d-flex flex-row justify-content-center mt-2">
                                <asp:TextBox runat="server" class="m-2 text-center form-control rounded" type="text" ID="first" MaxLength="1" required="true" />
                                <asp:TextBox runat="server" class="m-2 text-center form-control rounded" type="text" ID="second" MaxLength="1" required="true" />
                                <asp:TextBox runat="server" class="m-2 text-center form-control rounded" type="text" ID="third" MaxLength="1" required="true" />
                                <asp:TextBox runat="server" class="m-2 text-center form-control rounded" type="text" ID="fourth" MaxLength="1" required="true" />
                                <asp:TextBox runat="server" class="m-2 text-center form-control rounded" type="text" ID="fifth" MaxLength="1" required="true" />
                                <asp:TextBox runat="server" class="m-2 text-center form-control rounded" type="text" ID="sixth" MaxLength="1" required="true" />
                            </div>
                            <div class="d-grid mt-4">
                                <asp:Button ID="submitOTP" runat="server" CssClass="btn btn-primary" OnClick="submitOTP_Click" Text="Continue" />
                            </div>
                            <div class="d-flex justify-content-center align-items-end mt-3">
                                <p class="mb-0">Did not receive the email?</p>
                                <asp:LinkButton ID="resendOTP" runat="server" CssClass="link-primary ms-2" Text="Resend Code" OnClick="resendOTP_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        document.addEventListener("DOMContentLoaded", function (event) {
            function OTPInput() {
                const inputs = document.querySelectorAll('#otp > *[id]');
                for (let i = 0; i < inputs.length; i++) {
                    inputs[i].addEventListener('keydown', function (event) {
                        if (event.key === "Backspace") {
                            inputs[i].value = '';
                            if (i !== 0)
                                inputs[i - 1].focus();
                        } else {
                            const keyValue = event.key;
                            const numericRegex = /^[0-9]*$/;
                            if (!numericRegex.test(keyValue)) {
                                event.preventDefault();
                                return;
                            }

                            inputs[i].value = keyValue;
                            if (i !== inputs.length - 1)
                                inputs[i + 1].focus();

                            event.preventDefault();
                        }
                    });
                }
            }
            OTPInput();
        });
    </script>


</body>
</html>
