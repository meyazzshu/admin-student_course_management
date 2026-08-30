using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace CourseRegistration
{
    public partial class ManageCourses : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourses();
            }
        }

        private void LoadCourses()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Course", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCourses.DataSource = dt;
                gvCourses.DataBind();
            }
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Course (CourseID, Name, CreditHours, ClassRoom) VALUES (@ID, @Name, @CreditHours, @Class)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", txtCourseID.Text.Trim());
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@CreditHours", txtCredits.Text.Trim());
                cmd.Parameters.AddWithValue("@Class", txtClass.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
            }

            txtCourseID.Text = txtName.Text = txtCredits.Text = txtClass.Text = "";
            lblMessage.Text = "Course added successfully.";
            LoadCourses();
        }

        protected void gvCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            string courseId = gvCourses.SelectedDataKey.Value.ToString();
            ViewState["SelectedCourseID"] = courseId;
            lblSelectedCourse.Text = courseId;
            pnlSchedule.Visible = true;
            LoadSchedules(courseId);
        }

        private void LoadSchedules(string courseId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CourseSchedule WHERE CourseID = @CourseID", con);
                da.SelectCommand.Parameters.AddWithValue("@CourseID", courseId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSchedules.DataSource = dt;
                gvSchedules.DataBind();
            }
        }

        protected void btnAddSchedule_Click(object sender, EventArgs e)
        {
            string courseId = ViewState["SelectedCourseID"]?.ToString();
            if (string.IsNullOrEmpty(courseId))
            {
                lblMessage.Text = "Select a course first.";
                return;
            }

            string day = ddlDay.SelectedValue;
            int start = int.Parse(ddlStartHour.SelectedValue);
            int end = int.Parse(ddlEndHour.SelectedValue);

            if (end <= start)
            {
                lblMessage.Text = "End time must be after start time.";
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO CourseSchedule (CourseID, DayOfWeek, StartHour, EndHour) VALUES (@CourseID, @Day, @Start, @End)", con);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                cmd.Parameters.AddWithValue("@Day", day);
                cmd.Parameters.AddWithValue("@Start", start);
                cmd.Parameters.AddWithValue("@End", end);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Schedule added.";
            LoadSchedules(courseId);
        }

        protected void gvSchedules_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int scheduleId = Convert.ToInt32(gvSchedules.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM CourseSchedule WHERE ScheduleID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", scheduleId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            string courseId = ViewState["SelectedCourseID"]?.ToString();
            if (!string.IsNullOrEmpty(courseId))
                LoadSchedules(courseId);
        }
    }
}
