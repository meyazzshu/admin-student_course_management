<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignLecturer.aspx.cs" Inherits="CourseRegistration.AssignLecturer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Assign Lecturer to Course</h2>
    Course:
    <asp:DropDownList ID="ddlCourses" runat="server" CssClass="form-control mb-2" />
    Lecturers:
    <asp:DropDownList ID="ddlLecturers" runat="server" CssClass="form-control mb-2" />
    <br />
    <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="btn btn-primary" OnClick="btnAssign_Click" />
    <br /><asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-2" />

    <br />
    <br />

    <h4>All Courses</h4>
    <asp:GridView ID="gvAllCourses" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="Name" HeaderText="Course Name" />
            <asp:BoundField DataField="ClassRoom" HeaderText="Classroom" />
            <asp:BoundField DataField="LecturerName" HeaderText="Lecturer" />
        </Columns>
    </asp:GridView>

</asp:Content>
