using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace CourseRegistration
{
    public partial class ManageCourseRegistration : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadRegistrations();
        }

        private void LoadRegistrations()
        {
            string studentID = Session["UserID"].ToString();

            string query = @"
                SELECT 
                    r.RegistrationID,
                    c.CourseID,
                    c.Name AS CourseName,
                    c.CreditHours,
                    STRING_AGG(cs.DayOfWeek + ' ' + 
                        CAST(cs.StartHour AS VARCHAR) + '-' + 
                        CAST(cs.EndHour AS VARCHAR), ', ') AS Schedule,
                    r.IsApproved
                FROM Registration r
                INNER JOIN Course c ON r.CourseID = c.CourseID
                LEFT JOIN CourseSchedule cs ON c.CourseID = cs.CourseID
                WHERE r.StudentID = @StudentID
                GROUP BY r.RegistrationID, c.CourseID, c.Name, c.CreditHours, r.IsApproved";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                gvRegistrations.DataSource = dt;
                gvRegistrations.DataBind();

                lblTotalCourses.Text = $"Total courses registered: {dt.Rows.Count}";
            }
        }

        protected void gvRegistrations_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCourse")
            {
                int registrationID = Convert.ToInt32(e.CommandArgument);
                DeleteRegistration(registrationID);
                LoadRegistrations();
            }
        }

        private void DeleteRegistration(int registrationID)
        {
            string query = "DELETE FROM Registration WHERE RegistrationID = @RegID AND IsApproved = 0";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RegID", registrationID);
                con.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Registration deleted.";
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Cannot delete. This course has already been approved.";
                }
            }
        }
    }
}
