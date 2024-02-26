<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DSERP_Client_UI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
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
                            <h4 class="text-center f-w-500 my-4">Login with your UserName</h4>
                            <div class="form-group mb-3">
                                <asp:TextBox ID="Text_username" class="form-control" type="Text" runat="server" placeholder="Email UserName"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <asp:TextBox ID="Text_password" class="form-control" type="password" runat="server" placeholder="Password"></asp:TextBox>
                                <i class="toggle-password fa-regular fa-eye"></i>
                            </div>
                            <div class="d-flex mt-1 justify-content-end align-items-center">                              
                                <a href="/view/login/forgotpassword">
                                    <h6 class="text-secondary f-w-400 mb-0">Forgot Password?</h6>
                                </a>
                            </div>
                            <div class="d-grid mt-4">
                                <asp:Button type="button" ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="BtnLogin_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>



    <script>
        $(".toggle-password").click(function () {
            $(this).toggleClass("fa-eye fa-eye-slash"); 
            input = $(this).parent().find("input");
            if (input.attr("type") == "password") {
                input.attr("type", "text");
            } else {
                input.attr("type", "password");
            }
        });
    </script>
</body>
</html>
