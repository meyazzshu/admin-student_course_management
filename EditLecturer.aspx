<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditLecturer.aspx.cs" Inherits="CourseRegistration.EditLecturer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Manage Lecturers</h2>

    <asp:GridView ID="gvLecturers" runat="server" AutoGenerateColumns="False" DataKeyNames="LecturerID"
        OnRowEditing="gvLecturers_RowEditing"
        OnRowCancelingEdit="gvLecturers_RowCancelingEdit"
        OnRowUpdating="gvLecturers_RowUpdating"
        OnRowDeleting="gvLecturers_RowDeleting">

        <Columns>
            <asp:BoundField DataField="LecturerID" HeaderText="ID" ReadOnly="True" />

            <asp:TemplateField HeaderText="Name">
                <ItemTemplate><%# Eval("Name") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Email">
                <ItemTemplate><%# Eval("Email") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Department">
                <ItemTemplate><%# Eval("Department") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDept" runat="server" Text='<%# Bind("Department") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <br />

    <asp:Button ID="btnShowAddModal" runat="server" Text="➕ Add New Lecturer"
    CssClass="btn btn-primary mb-3"
    OnClientClick="showAddLecturerModal(); return false;" />

    <!-- Bootstrap Modal -->
    <div class="modal fade" id="addLecturerModal" tabindex="-1" aria-labelledby="lecturerModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Add New Lecturer</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
          </div>
          <div class="modal-body">
            <asp:TextBox ID="txtID" runat="server" CssClass="form-control mb-2" Placeholder="Lecturer ID (e.g., L0001)" />
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control mb-2" Placeholder="Name" />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-2" Placeholder="Email" />
            <asp:TextBox ID="txtDept" runat="server" CssClass="form-control mb-2" Placeholder="Department" />
          </div>
          <div class="modal-footer">
            <asp:Button ID="btnAdd" runat="server" Text="Add Lecturer" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
          </div>
        </div>
      </div>
    </div>

    &nbsp;<asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-2" />

    <script type="text/javascript">
    function showAddLecturerModal() {
        var modal = new bootstrap.Modal(document.getElementById('addLecturerModal'));
        modal.show();
    }
    </script>

</asp:Content>
