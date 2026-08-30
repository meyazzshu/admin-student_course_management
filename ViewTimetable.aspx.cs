using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace CourseRegistration
{
    public partial class ViewTimetable : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadTimetable();
        }

        private void LoadTimetable()
        {
            string studentID = Session["UserID"]?.ToString();
            if (string.IsNullOrEmpty(studentID)) return;

            string query = @"
                SELECT 
                    cs.DayOfWeek AS Day,
                    CONCAT(cs.StartHour, ':00 - ', cs.EndHour, ':00') AS Time,
                    c.CourseID,
                    c.Name AS CourseName,
                    l.Name AS LecturerName,
                    c.ClassRoom
                FROM Registration r
                INNER JOIN Course c ON r.CourseID = c.CourseID
                INNER JOIN CourseSchedule cs ON r.ScheduleID = cs.ScheduleID
                LEFT JOIN Lecturer l ON c.LecturerID = l.LecturerID
                WHERE r.StudentID = @StudentID AND r.IsApproved = 1
                ORDER BY 
                    CASE 
                        WHEN cs.DayOfWeek = 'Monday' THEN 1
                        WHEN cs.DayOfWeek = 'Tuesday' THEN 2
                        WHEN cs.DayOfWeek = 'Wednesday' THEN 3
                        WHEN cs.DayOfWeek = 'Thursday' THEN 4
                        WHEN cs.DayOfWeek = 'Friday' THEN 5
                        ELSE 6
                    END, cs.StartHour";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                gvTimetable.DataSource = dt;
                gvTimetable.DataBind();
            }
        }
    }
}
