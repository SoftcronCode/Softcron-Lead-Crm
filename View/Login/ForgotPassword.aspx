<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DSERP_Client_UI.View.Login.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password Page</title>
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
                            <div class="text-center">
                                <a href="#">
                                    <%--<img src="/assets/img/logo.png" alt="logo" height="60" width="190" />--%>
                                    <img src="/assets/img/getmyca-logo.png" alt="logo" height="70" width="150" />
                                </a>
                            </div>
                            <div class="mb-4 mt-4 text-center">
                                <h3 class="mb-0"><b>Forgot Password</b></h3>
                            </div>
                            <div class="form-group mb-3">
                                <label class="form-label">Email Address</label>
                                <asp:TextBox ID="txt_email" runat="server" type="email" class="form-control" placeholder="Email Address" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_email" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Enter EmailID" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_email" ValidationGroup="Group1" ErrorMessage="Invalid email format" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                            <p class="text-center"><a href="/View/Login/login" class="link-primary">Back to Login</a></p>
                            <div class="d-grid mt-3">
                                <asp:Button ID="PasswordResetEmailBtn" runat="server" CssClass="btn btn-primary" Text="Send Password Reset Email" ValidationGroup="Group1" OnClick="PasswordResetEmailBtn_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
