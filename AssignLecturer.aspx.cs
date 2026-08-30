using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace CourseRegistration
{
    public partial class AssignLecturer : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourses();
                LoadLecturers();
                LoadAllCourses();
            }
        }

        private void LoadCourses()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CourseID, Name FROM Course", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlCourses.DataSource = reader;
                ddlCourses.DataTextField = "Name";
                ddlCourses.DataValueField = "CourseID";
                ddlCourses.DataBind();
            }
        }

        private void LoadLecturers()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT LecturerID, Name FROM Lecturer", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlLecturers.DataSource = reader;
                ddlLecturers.DataTextField = "Name";
                ddlLecturers.DataValueField = "LecturerID";
                ddlLecturers.DataBind();
            }
        }

        private void LoadAllCourses()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT c.CourseID, c.Name, c.ClassRoom,
                   ISNULL(l.Name, 'Not Assigned') AS LecturerName
            FROM Course c
            LEFT JOIN Lecturer l ON c.LecturerID = l.LecturerID";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                gvAllCourses.DataSource = reader;
                gvAllCourses.DataBind();
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            string courseId = ddlCourses.SelectedValue;
            string lecturerId = ddlLecturers.SelectedValue;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Course SET LecturerID = @LecturerID WHERE CourseID = @CourseID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LecturerID", lecturerId);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Lecturer assigned successfully.";
            LoadAllCourses();      // <--- Refresh full course table
            LoadCourses();
        }
    }

}