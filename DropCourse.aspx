<%@ Page Title="Drop Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DropCourse.aspx.cs" Inherits="CourseRegistration.DropCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Drop Approved Courses</h2>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" /><br />

    <asp:GridView ID="gvApprovedCourses" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnRowCommand="gvApprovedCourses_RowCommand" DataKeyNames="RegistrationID">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
            <asp:BoundField DataField="Day" HeaderText="Day" />
            <asp:BoundField DataField="Time" HeaderText="Time" />
            <asp:BoundField DataField="LecturerName" HeaderText="Lecturer" />
            <asp:BoundField DataField="ClassRoom" HeaderText="Room" />
            <asp:ButtonField ButtonType="Button" CommandName="Drop" Text="Drop" ControlStyle-CssClass="btn btn-danger btn-sm" />
        </Columns>
    </asp:GridView>
</asp:Content>
