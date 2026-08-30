<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageAccount.aspx.cs" Inherits="CourseRegistration.ManageAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <h2>Manage Account</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" /><br />

        <asp:Label runat="server" Text="User ID:" />
        <asp:Label ID="lblUserID" runat="server" CssClass="form-control" /><br />

        <asp:Label runat="server" Text="Name:" />
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" /><br />

        <asp:Label runat="server" Text="Email:" />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" /><br />

        <asp:Label runat="server" Text="Password:" />
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="SingleLine" /><br /> 

        <br />
        <br />

        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-primary" />

</asp:Content>
