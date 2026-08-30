<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewTimetable.aspx.cs" Inherits="CourseRegistration.ViewTimetable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Your Timetable</h2>

    <asp:GridView ID="gvTimetable" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
    <Columns>
        <asp:BoundField DataField="Day" HeaderText="Day" />
        <asp:BoundField DataField="Time" HeaderText="Time" />
        <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
        <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
        <asp:BoundField DataField="LecturerName" HeaderText="Lecturer" />
        <asp:BoundField DataField="ClassRoom" HeaderText="Room" />
    </Columns>
</asp:GridView>

</asp:Content>
