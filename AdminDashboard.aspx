<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="CourseRegistration.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Admin Dashboard</h2>
    <p class="mb-4 text-muted">Manage registrations, assign lecturers, and organize the system from here.</p>

    <div class="row row-cols-1 row-cols-md-2 g-4">
        <!-- Manage Courses -->
        <div class="col">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body d-flex align-items-center">
                    <div class="me-3">
                        <i class="bi bi-book-fill" style="font-size: 2rem; color: #2e7d32;"></i>
                    </div>
                    <div>
                        <h5 class="card-title mb-1">Manage Courses</h5>
                        <p class="card-text small text-muted">Add, update or delete course details.</p>
                        <asp:HyperLink runat="server" NavigateUrl="ManageCourses.aspx" CssClass="btn btn-sm btn-success">Go</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

        <!-- Assign Lecturers -->
        <div class="col">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body d-flex align-items-center">
                    <div class="me-3">
                        <i class="bi bi-person-badge-fill" style="font-size: 2rem; color: #2e7d32;"></i>
                    </div>
                    <div>
                        <h5 class="card-title mb-1">Assign Lecturers</h5>
                        <p class="card-text small text-muted">Assign lecturers to their courses.</p>
                        <asp:HyperLink runat="server" NavigateUrl="AssignLecturer.aspx" CssClass="btn btn-sm btn-success">Go</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

        <!-- Approve Registrations -->
        <div class="col">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body d-flex align-items-center">
                    <div class="me-3">
                        <i class="bi bi-check2-circle" style="font-size: 2rem; color: #2e7d32;"></i>
                    </div>
                    <div>
                        <h5 class="card-title mb-1">Approve Registrations</h5>
                        <p class="card-text small text-muted">Review and approve student course selections.</p>
                        <asp:HyperLink runat="server" NavigateUrl="ApproveRegistrations.aspx" CssClass="btn btn-sm btn-success">Go</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

        <!-- Add Lecturer -->
        <div class="col">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body d-flex align-items-center">
                    <div class="me-3">
                        <i class="bi bi-person-plus-fill" style="font-size: 2rem; color: #2e7d32;"></i>
                    </div>
                    <div>
                        <h5 class="card-title mb-1">Add Lecturer</h5>
                        <p class="card-text small text-muted">Register new lecturers into the system.</p>
                        <asp:HyperLink runat="server" NavigateUrl="EditLecturer.aspx" CssClass="btn btn-sm btn-success">Go</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
