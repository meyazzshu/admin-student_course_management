<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCourseRegistration.aspx.cs" Inherits="CourseRegistration.ManageCourseRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Manage Your Course Registrations</h2>

    <asp:GridView ID="gvRegistrations" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" DataKeyNames="RegistrationID" OnRowCommand="gvRegistrations_RowCommand">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
            <asp:BoundField DataField="CreditHours" HeaderText="Credit Hours" />
            <asp:BoundField DataField="IsApproved" HeaderText="Approved" />

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm"
                        CommandName="DeleteCourse" CommandArgument='<%# Eval("RegistrationID") %>'
                        OnClientClick="return confirm('Are you sure you want to delete this course?');"
                        Enabled='<%# !(bool)Eval("IsApproved") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />

    <asp:Label ID="lblTotalCourses" runat="server" CssClass="fw-bold mt-3" ForeColor="Black" />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

</asp:Content>
