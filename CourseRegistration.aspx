<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseRegistration.aspx.cs" Inherits="CourseRegistration.CourseRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Course Registration</h2>

    <div class="d-flex mb-3">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control me-2" Width="300px" />
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-success" />
    </div>

    <br />

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" /><br />
    <asp:Label ID="lblTotalCourses" runat="server" /><br />
    <asp:Label ID="lblTotalCredits" runat="server" /><br /><br />

    <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="false" OnRowCommand="gvCourses_RowCommand" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="Name" HeaderText="Course Name" />
            <asp:BoundField DataField="CreditHours" HeaderText="Credit Hours" />
            <asp:BoundField DataField="DayOfWeek" HeaderText="Day" />
            <asp:BoundField DataField="StartHour" HeaderText="Start Hour" />
            <asp:BoundField DataField="EndHour" HeaderText="End Hour" />
            <asp:BoundField DataField="LecturerName" HeaderText="Lecturer" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary btn-sm"
                        CommandName="AddCourse"
                        CommandArgument='<%# Eval("CourseID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />

    <asp:Button ID="btnSubmit" runat="server" Text="Submit Registration" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
</asp:Content>
