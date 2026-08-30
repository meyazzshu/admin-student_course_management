using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace CourseRegistration
{
    public partial class EditLecturer : System.Web.UI.Page
    {
        string connStr = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadLecturers();
        }

        private void LoadLecturers()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Lecturer", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvLecturers.DataSource = dt;
                gvLecturers.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Lecturer (LecturerID, Name, Email, Department) VALUES (@ID, @Name, @Email, @Dept)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Dept", txtDept.Text.Trim());

                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Lecturer added successfully.";
                    LoadLecturers();
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void gvLecturers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLecturers.EditIndex = e.NewEditIndex;
            LoadLecturers();
        }

        protected void gvLecturers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvLecturers.EditIndex = -1;
            LoadLecturers();
        }

        protected void gvLecturers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string lecturerId = gvLecturers.DataKeys[e.RowIndex].Value.ToString();
            
            GridViewRow row = gvLecturers.Rows[e.RowIndex];

            string name = ((TextBox)row.FindControl("txtName")).Text;
            string email = ((TextBox)row.FindControl("txtEmail")).Text;
            string dept = ((TextBox)row.FindControl("txtDept")).Text;


            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "UPDATE Lecturer SET Name=@Name, Email=@Email, Department=@Dept WHERE LecturerID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Dept", dept);
                cmd.Parameters.AddWithValue("@ID", lecturerId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            gvLecturers.EditIndex = -1;
            LoadLecturers();
        }

        protected void gvLecturers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string lecturerId = gvLecturers.DataKeys[e.RowIndex].Value.ToString();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Lecturer WHERE LecturerID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", lecturerId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadLecturers();
        }
    }

}