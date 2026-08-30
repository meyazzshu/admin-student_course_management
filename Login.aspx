<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CourseRegistration.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            background-image: url('Images/background1.JPG'); /* Your background image */
            background-size: cover;
            background-position: center;
            font-family: Arial, sans-serif;
        }

        .login-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .login-box {
            background-color: rgba(255, 255, 255, 0.95);
            padding: 40px 30px;
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(0,0,0,0.3);
            display: flex;
            align-items: center;
            gap: 30px;
        }

        .login-box img {
            width: 180px;
            height: auto;
            border-radius: 10px;
        }

        .login-form {
            display: flex;
            flex-direction: column;
            width: 280px;
        }

        .login-form h2 {
            margin: 0 0 20px 0;
            text-align: center;
            color: #333;
        }

        .login-form asp\:textbox {
            padding: 8px;
            margin-top: 5px;
        }

        .login-form asp\:button {
            margin-top: 15px;
        }

        .login-form asp\:label {
            margin-top: 10px;
        }

        .register-link {
            margin-top: 15px;
            text-align: center;
        }

        .register-link asp\:linkbutton {
            color: #0066cc;
            text-decoration: underline;
            font-size: 0.75em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="login-box">
                <div style="text-align: center; margin-bottom: 20px;">
                    <img src="Images/logo.png" alt="University Banner" style="max-width: 300px; width: 100%; height: auto;" />
                    <img src="Images/panel.jpg" alt="Login Visual" style="max-width: 300px; width: 100%; height: auto;"/> <!-- Side image -->
                </div>
                
                <div class="login-form">
                    <h2>Login</h2>

                    <asp:Label ID="lblEmail" runat="server" Text="Email:" />
                    <asp:TextBox ID="txtEmail" runat="server" />

                    <br />

                    <asp:Label ID="lblPassword" runat="server" Text="Password:" />
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />

                    <br />
                    <br />

                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />

                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

                    <div class="register-link">
                        <asp:LinkButton 
                            ID="lnkRegister" 
                            runat="server" 
                            PostBackUrl="~/Register.aspx" 
                            Font-Size="10pt">
                            Don't have an account yet? Register here
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
