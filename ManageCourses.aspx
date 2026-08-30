<%@ Page Title="Manage Courses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="CourseRegistration.ManageCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Courses and Schedules</h2>

    <!-- Add Course Section -->
    <asp:Panel ID="pnlAddCourse" runat="server" CssClass="mb-4">
        <h4>Add New Course</h4>
        Course Code:
        <asp:TextBox ID="txtCourseID" runat="server" CssClass="form-control mb-2" Placeholder="Course ID" />
        Course Name:
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control mb-2" Placeholder="Course Name" />
        Credit Hours:
        <asp:TextBox ID="txtCredits" runat="server" CssClass="form-control mb-2" Placeholder="Credit Hours" />
        Classroom:
        <asp:TextBox ID="txtClass" runat="server" CssClass="form-control mb-2" Placeholder="Classroom" />
        <br />
        <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" CssClass="btn btn-primary" OnClick="btnAddCourse_Click" />
    </asp:Panel>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

    <!-- Course List -->
    <br />
    <br />
    <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="false" DataKeyNames="CourseID"
        OnSelectedIndexChanged="gvCourses_SelectedIndexChanged" CssClass="table table-bordered">
        <Columns>
            <asp:CommandField ShowSelectButton="true" SelectText="Manage Schedule" />
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="Name" HeaderText="Course Name" />
            <asp:BoundField DataField="CreditHours" HeaderText="Credit Hours" />
            <asp:BoundField DataField="ClassRoom" HeaderText="Classroom" />
        </Columns>
    </asp:GridView>

    <br />

    <!-- Add Schedule Section -->
    <asp:Panel ID="pnlSchedule" runat="server" Visible="false">
        <h4>Manage Schedule for: <asp:Label ID="lblSelectedCourse" runat="server" /></h4>

        Day:
        <asp:DropDownList ID="ddlDay" runat="server" CssClass="form-control mb-2" Width="200px">
            <asp:ListItem Text="Monday" /><asp:ListItem Text="Tuesday" />
            <asp:ListItem Text="Wednesday" /><asp:ListItem Text="Thursday" />
            <asp:ListItem Text="Friday" />
        </asp:DropDownList>

        Start Hour:
        <asp:DropDownList ID="ddlStartHour" runat="server" CssClass="form-control mb-2" Width="100px">
            <%-- 8AM to 4PM --%>
            <asp:ListItem Text="8" /><asp:ListItem Text="9" /><asp:ListItem Text="10" />
            <asp:ListItem Text="11" /><asp:ListItem Text="12" />
            <asp:ListItem Text="13" /><asp:ListItem Text="14" />
            <asp:ListItem Text="15" /><asp:ListItem Text="16" />
        </asp:DropDownList>

        End Hour:
        <asp:DropDownList ID="ddlEndHour" runat="server" CssClass="form-control mb-2" Width="100px">
            <asp:ListItem Text="9" /><asp:ListItem Text="10" /><asp:ListItem Text="11" />
            <asp:ListItem Text="12" /><asp:ListItem Text="13" />
            <asp:ListItem Text="14" /><asp:ListItem Text="15" />
            <asp:ListItem Text="16" /><asp:ListItem Text="17" />
        </asp:DropDownList>

        <br />

        <asp:Button ID="btnAddSchedule" runat="server" Text="Add Schedule" CssClass="btn btn-success mb-3" OnClick="btnAddSchedule_Click" />

        <br />
        <br />

        <asp:GridView ID="gvSchedules" runat="server" AutoGenerateColumns="false" DataKeyNames="ScheduleID" OnRowDeleting="gvSchedules_RowDeleting" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="DayOfWeek" HeaderText="Day" />
                <asp:BoundField DataField="StartHour" HeaderText="Start" />
                <asp:BoundField DataField="EndHour" HeaderText="End" />
                <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
