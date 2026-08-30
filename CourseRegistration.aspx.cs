using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourseRegistration
{
    public partial class CourseRegistration : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SelectedCourses"] = new List<(string CourseID, int ScheduleID, int CreditHours)>();
                Session["TotalCredits"] = 0;
                LoadAvailableCourses();
            }

            UpdateTotalsLabel();
        }

        private int GetApprovedCreditHours(string studentID)
        {
            int approvedCredits = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT SUM(c.CreditHours)
            FROM Registration r
            INNER JOIN Course c ON r.CourseID = c.CourseID
            WHERE r.StudentID = @StudentID AND r.IsApproved = 1";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                con.Open();

                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                    approvedCredits = Convert.ToInt32(result);
            }

            return approvedCredits;
        }

        private void LoadAvailableCourses(string searchKeyword = "")
        {
            string studentID = Session["UserID"]?.ToString();

            string query = @"
                SELECT 
                    c.CourseID,
                    c.Name,
                    c.CreditHours,
                    cs.DayOfWeek,
                    cs.StartHour,
                    cs.EndHour,
                    l.Name AS LecturerName
                FROM Course c
                LEFT JOIN Lecturer l ON c.LecturerID = l.LecturerID
                INNER JOIN CourseSchedule cs ON c.CourseID = cs.CourseID
                WHERE c.CourseID NOT IN (
                    SELECT CourseID FROM Registration WHERE StudentID = @StudentID
                )
                AND (c.Name LIKE @Search OR c.CourseID LIKE @Search)
                ORDER BY c.CourseID, cs.DayOfWeek, cs.StartHour";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@Search", "%" + searchKeyword + "%");

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                gvCourses.DataSource = dt;
                gvCourses.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAvailableCourses(txtSearch.Text.Trim());
        }

        protected void gvCourses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddCourse")
            {
                string courseId = e.CommandArgument.ToString();
                int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
                GridViewRow row = gvCourses.Rows[rowIndex];

                int creditHours = Convert.ToInt32(row.Cells[2].Text); // Adjust if needed

                int scheduleId = 0;
                string day = "";
                int start = 0;
                int end = 0;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"SELECT TOP 1 ScheduleID, DayOfWeek, StartHour, EndHour 
                                     FROM CourseSchedule 
                                     WHERE CourseID = @CourseID 
                                     ORDER BY DayOfWeek, StartHour";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        scheduleId = Convert.ToInt32(reader["ScheduleID"]);
                        day = reader["DayOfWeek"].ToString();
                        start = Convert.ToInt32(reader["StartHour"]);
                        end = Convert.ToInt32(reader["EndHour"]);
                    }
                    else
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = $"No schedule found for course {courseId}.";
                        return;
                    }
                    reader.Close();

                    var selectedCourses = Session["SelectedCourses"] as List<(string CourseID, int ScheduleID, int CreditHours)>
                        ?? new List<(string, int, int)>();

                    if (selectedCourses.Any(c => c.CourseID == courseId))
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = $"{courseId} is already selected.";
                        return;
                    }

                    foreach (var selected in selectedCourses)
                    {
                        SqlCommand checkCmd = new SqlCommand("SELECT DayOfWeek, StartHour, EndHour FROM CourseSchedule WHERE ScheduleID = @ScheduleID", con);
                        checkCmd.Parameters.AddWithValue("@ScheduleID", selected.ScheduleID);
                        SqlDataReader checkReader = checkCmd.ExecuteReader();
                        while (checkReader.Read())
                        {
                            string selectedDay = checkReader["DayOfWeek"].ToString();
                            int selectedStart = Convert.ToInt32(checkReader["StartHour"]);
                            int selectedEnd = Convert.ToInt32(checkReader["EndHour"]);

                            if (day == selectedDay &&
                                ((start >= selectedStart && start < selectedEnd) ||
                                 (end > selectedStart && end <= selectedEnd) ||
                                 (start <= selectedStart && end >= selectedEnd)))
                            {
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                lblMessage.Text = $"Time conflict: {courseId} clashes with another course on {day}.";
                                checkReader.Close();
                                return;
                            }
                        }
                        checkReader.Close();
                    }

                    selectedCourses.Add((courseId, scheduleId, creditHours));
                    Session["SelectedCourses"] = selectedCourses;
                    Session["TotalCredits"] = (int)(Session["TotalCredits"] ?? 0) + creditHours;

                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = $"{courseId} added.";
                    UpdateTotalsLabel();
                }
            }
        }

        private void UpdateTotalsLabel()
        {
            var selectedCourses = Session["SelectedCourses"] as List<(string, int, int)>;
            int totalSelectedCredits = (int)(Session["TotalCredits"] ?? 0);

            string studentID = Session["UserID"]?.ToString();
            int approvedCredits = 0;
            if (!string.IsNullOrEmpty(studentID))
            {
                approvedCredits = GetApprovedCreditHours(studentID);
            }

            lblTotalCourses.Text = $"Selected Courses: {selectedCourses?.Count ?? 0}";
            lblTotalCredits.Text = $"Selected Credit Hours: {totalSelectedCredits} | Approved Credit Hours: {approvedCredits} | Total: {totalSelectedCredits + approvedCredits}";
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string studentID = Session["UserID"]?.ToString();
            if (string.IsNullOrEmpty(studentID))
            {
                lblMessage.Text = "Session expired. Please log in again.";
                return;
            }

            var selectedCourses = Session["SelectedCourses"] as List<(string CourseID, int ScheduleID, int CreditHours)>;
            int selectedCredits = (int)(Session["TotalCredits"] ?? 0);

            int approvedCredits = GetApprovedCreditHours(studentID);
            int totalCombinedCredits = selectedCredits + approvedCredits;

            if (selectedCourses == null || selectedCourses.Count == 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "No courses selected.";
                return;
            }

            if (totalCombinedCredits < 5 || totalCombinedCredits > 20)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = $"Credit hours must be between 5 and 20 total. You have {approvedCredits} approved and selected {selectedCredits}, total: {totalCombinedCredits}.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    foreach (var course in selectedCourses)
                    {
                        SqlCommand insertCmd = new SqlCommand(
                            "INSERT INTO Registration (StudentID, CourseID, ScheduleID, IsApproved) VALUES (@StudentID, @CourseID, @ScheduleID, 0)",
                            con);
                        insertCmd.Parameters.AddWithValue("@StudentID", studentID);
                        insertCmd.Parameters.AddWithValue("@CourseID", course.CourseID);
                        insertCmd.Parameters.AddWithValue("@ScheduleID", course.ScheduleID);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Courses submitted for approval.";

                Session["SelectedCourses"] = new List<(string, int, int)>();
                Session["TotalCredits"] = 0;

                LoadAvailableCourses();
                UpdateTotalsLabel();
            }
            catch (System.Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

    }
}
