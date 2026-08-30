using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace CourseRegistration
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRegisteredCourses();
                if (GetPendingCourseCount() > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowBadge", "<script>document.getElementById('notifBadge').style.display='inline';</script>");
                }

            }
        }

        private int GetPendingCourseCount()
        {
            string studentID = Session["UserID"]?.ToString();
            if (string.IsNullOrEmpty(studentID))
                return 0;

            int count = 0;
            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

            string query = "SELECT COUNT(*) FROM Registration WHERE StudentID = @StudentID AND IsApproved = 0";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                con.Open();
                count = (int)cmd.ExecuteScalar();
            }

            return count;
        }

        private void LoadRegisteredCourses()
        {
            string studentID = Session["UserID"].ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

            string query = @"
            SELECT c.CourseID, c.Name, c.CreditHours, c.ClassRoom
            FROM Registration r
            INNER JOIN Course c ON r.CourseID = c.CourseID
            WHERE r.StudentID = @StudentID AND r.IsApproved = 1";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                gvCourses.DataSource = dt;
                gvCourses.DataBind();

                int totalCredits = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalCredits += Convert.ToInt32(row["CreditHours"]);
                }
                lblCredits.Text = totalCredits.ToString();
            }
        }
    }
}