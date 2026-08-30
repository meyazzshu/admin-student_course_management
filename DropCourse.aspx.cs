using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace CourseRegistration
{
    public partial class DropCourse : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadApprovedCourses();
        }

        private void LoadApprovedCourses()
        {
            string studentID = Session["UserID"]?.ToString();
            if (string.IsNullOrEmpty(studentID))
            {
                lblMessage.Text = "Session expired. Please log in again.";
                return;
            }

            string query = @"
                SELECT 
                    r.RegistrationID,
                    c.CourseID,
                    c.Name AS CourseName,
                    cs.DayOfWeek AS Day,
                    CONCAT(cs.StartHour, ':00 - ', cs.EndHour, ':00') AS Time,
                    l.Name AS LecturerName,
                    c.ClassRoom
                FROM Registration r
                INNER JOIN Course c ON r.CourseID = c.CourseID
                INNER JOIN CourseSchedule cs ON r.ScheduleID = cs.ScheduleID
                LEFT JOIN Lecturer l ON c.LecturerID = l.LecturerID
                WHERE r.StudentID = @StudentID AND r.IsApproved = 1
                ORDER BY cs.DayOfWeek, cs.StartHour";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                gvApprovedCourses.DataSource = dt;
                gvApprovedCourses.DataBind();
            }
        }

        protected void gvApprovedCourses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Drop")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string regID = gvApprovedCourses.DataKeys[rowIndex].Value.ToString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Registration WHERE RegistrationID = @RegID", con);
                    cmd.Parameters.AddWithValue("@RegID", regID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Course successfully dropped.";
                LoadApprovedCourses();
            }
        }
    }
}
