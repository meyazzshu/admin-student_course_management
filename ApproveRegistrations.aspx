<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveRegistrations.aspx.cs" Inherits="CourseRegistration.ApproveRegistrations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Pending Course Registrations</h2>

    <div class="mb-3">
        <asp:Label ID="lblSearch" runat="server" Text="Search by Student ID:" />
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control d-inline-block" Width="200px" />
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-success btn-sm ms-2" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-outline-secondary btn-sm ms-1" />
    </div>

    <asp:GridView ID="gvRegistrations" runat="server" AutoGenerateColumns="false" DataKeyNames="RegistrationID"
        OnRowCommand="gvRegistrations_RowCommand" CssClass="table table-bordered table-hover">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="Student ID" />
            <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
            <asp:BoundField DataField="CreditHours" HeaderText="Credit Hours" />

            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success btn-sm"
                        CommandName="Approve" CommandArgument='<%# Eval("RegistrationID") %>' />

                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger btn-sm ms-2"
                        CommandName="Reject" CommandArgument='<%# Eval("RegistrationID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br /><br />

    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
</asp:Content>
