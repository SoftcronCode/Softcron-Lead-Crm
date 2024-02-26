<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="DSERP_Client_UI.View.Login.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password Page</title>
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
                            <div class="text-center mb-5">
                                <a href="#">
                                    <%--<img src="/assets/img/logo.png" alt="logo" height="60" width="190" />--%>
                                    <img src="/assets/img/getmyca-logo.png" alt="logo" height="70" width="150" />
                                </a>
                            </div>
                            <div class="mb-4 text-center">
                                <h3 class="mb-2"><b>Reset Password</b></h3>
                                <p class="text-muted">Please choose your new password</p>
                            </div>
                            <div class="form-group mb-3">
                                <label class="form-label">Password</label>
                                <asp:TextBox ID="textbox_password" runat="server" type="password" class="form-control" placeholder="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textbox_password" ErrorMessage="Password is required." ForeColor="Red" Display="Dynamic" ValidationGroup="Group1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group mb-3">
                                <label class="form-label">Confirm Password</label>
                                <asp:TextBox ID="textbox_confirmPass" runat="server" type="password" class="form-control" placeholder="Confirm Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textbox_confirmPass" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Confirm Password Require" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="textbox_password" ControlToValidate="textbox_confirmPass" ValidationGroup="Group1" ErrorMessage="Passwords do not match." SetFocusOnError="True" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                            </div>
                            <div class="d-grid mt-4">
                                <asp:Button ID="resetPasswordBtn" runat="server" CssClass="btn btn-primary" Text="Reset Password" OnClick="resetPasswordBtn_Click" ValidationGroup="Group1" />
                            </div>
                            <p class="text-center"><a href="/view/login/login" class="link-primary">Back to Login</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
