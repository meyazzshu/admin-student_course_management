<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CourseRegistration.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            background-image: url('Images/background1.JPG'); /* ✅ Replace with your background path */
            background-size: cover;
            background-position: center;
            font-family: Arial, sans-serif;
        }

        .register-container {
            position: fixed;
            top: 0;
            left: 0;
            width: 100vw;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .register-box {
            background-color: rgba(255, 255, 255, 0.95);
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(0,0,0,0.3);
            display: flex;
            align-items: center;
            gap: 30px;
        }

        .register-box img {
            width: 150px;
            height: auto;
            border-radius: 10px;
        }

        .register-form {
            display: flex;
            flex-direction: column;
            width: 280px;
        }

        .register-form asp\:textbox,
        .register-form asp\:label {
            margin-top: 10px;
        }

        .register-form asp\:button {
            margin-top: 20px;
        }

        .small-link {
            font-size: 0.8em;
            margin-top: 15px;
            text-align: center;
        }

        .text-danger {
            font-size: 0.75em;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="register-container">
            <div class="register-box">
                <div style="text-align: center; margin-bottom: 20px;">
                    <img src="Images/logo.png" alt="University Banner" style="max-width: 300px; width: 100%; height: auto;" />
                    <img src="Images/panel.jpg" alt="Login Visual" style="max-width: 300px; width: 100%; height: auto;"/> <!-- Side image -->
                </div>
                <div class="register-form">
                    <h2>Register</h2>

                    <asp:Label ID="lblName" runat="server" Text="Name:" />
                    <asp:TextBox ID="txtName" runat="server" />

                    <br />

                    <asp:Label ID="lblEmail" runat="server" Text="Email:" />
                    <asp:TextBox ID="txtEmail" runat="server" />
                    <asp:RegularExpressionValidator 
                        ID="revEmail" 
                        runat="server" 
                        ControlToValidate="txtEmail" 
                        ErrorMessage="Email must end with @student.university.com"
                        ValidationExpression="^[a-zA-Z0-9._%+-]+@student\.university\.com$"
                        Display="Dynamic" 
                        CssClass="text-danger" 
                        ForeColor="Red" />

                    <br />

                    <asp:Label ID="lblPassword" runat="server" Text="Password:" />
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />

                    <br />
                    <br />

                    <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />

                    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />

                    <div class="small-link">
                        <asp:LinkButton ID="lnkLogin" runat="server" PostBackUrl="~/Login.aspx">Already have an account? Login</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
