<%@ Page Title="Student Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="CourseRegistration.StudentDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Student Dashboard</h2>
    <p class="mb-4 text-muted">Register, manage your courses, view timetable and drop approved courses here.</p>

    <!-- ✅ Student Actions -->
    <div class="row row-cols-1 row-cols-md-2 g-4">
        <!-- Course Registration -->
        <div class="col">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body d-flex align-items-center">
                    <div class="me-3">
                        <i class="bi bi-pencil-square" style="font-size: 2rem; color: #1976d2;"></i>
                    </div>
                    <div>
                        <h5 class="card-title mb-1">Course Registration</h5>
                        <p class="card-text small text-muted">Register for available courses.</p>
                        <asp:HyperLink runat="server" NavigateUrl="CourseRegistration.aspx" CssClass="btn btn-sm btn-primary">Go</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

        <!-- Manage Course -->
        <div class="col">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body d-flex align-items-center">
                    <div class="me-3 position-relative">
                        <i class="bi bi-gear-fill" style="font-size: 2rem; color: #1976d2;"></i>
                        <span id="notifBadge" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" style="display:none;">!</span>
                    </div>
                    <div>
                        <h5 class="card-title mb-1">Manage Course</h5>
                        <p class="card-text small text-muted">View pending registrations.</p>
                        <asp:HyperLink runat="server" NavigateUrl="ManageCourseRegistration.aspx" CssClass="btn btn-sm btn-primary">Go</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

        <!-- View Timetable -->
        <div class="col">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body d-flex align-items-center">
                    <div class="me-3">
                        <i class="bi bi-calendar3" style="font-size: 2rem; color: #1976d2;"></i>
                    </div>
                    <div>
                        <h5 class="card-title mb-1">View Timetable</h5>
                        <p class="card-text small text-muted">Check your class schedule.</p>
                        <asp:HyperLink runat="server" NavigateUrl="ViewTimetable.aspx" CssClass="btn btn-sm btn-primary">Go</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

        <!-- Drop Course -->
        <div class="col">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body d-flex align-items-center">
                    <div class="me-3">
                        <i class="bi bi-x-circle-fill" style="font-size: 2rem; color: #1976d2;"></i>
                    </div>
                    <div>
                        <h5 class="card-title mb-1">Drop Course</h5>
                        <p class="card-text small text-muted">Drop approved courses if needed.</p>
                        <asp:HyperLink runat="server" NavigateUrl="DropCourse.aspx" CssClass="btn btn-sm btn-primary">Go</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- ✅ Registered Courses Summary -->
    <hr class="my-4" />
    <h3>Your Registered Courses</h3>
    <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="Name" HeaderText="Course Name" />
            <asp:BoundField DataField="CreditHours" HeaderText="Credit Hours" />
            <asp:BoundField DataField="ClassRoom" HeaderText="Class Room" />
        </Columns>
    </asp:GridView>

    <h4>Total Credit Hours: <asp:Label ID="lblCredits" runat="server" Text="0" /></h4>
</asp:Content>
