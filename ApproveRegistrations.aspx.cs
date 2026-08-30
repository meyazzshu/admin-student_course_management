using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace CourseRegistration
{
    public partial class ApproveRegistrations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadPendingRegistrations();
        }

        private void LoadPendingRegistrations(string studentIdFilter = "")
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

            string query = @"
                SELECT r.RegistrationID, s.StudentID, s.Name AS StudentName,
                       c.CourseID, c.Name AS CourseName, c.CreditHours
                FROM Registration r
                JOIN Student s ON r.StudentID = s.StudentID
                JOIN Course c ON r.CourseID = c.CourseID
                WHERE r.IsApproved = 0";

            if (!string.IsNullOrEmpty(studentIdFilter))
            {
                query += " AND s.StudentID LIKE @StudentID";
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (!string.IsNullOrEmpty(studentIdFilter))
                {
                    cmd.Parameters.AddWithValue("@StudentID", "%" + studentIdFilter + "%");
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                gvRegistrations.DataSource = dt;
                gvRegistrations.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string studentId = txtSearch.Text.Trim();
            LoadPendingRegistrations(studentId);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadPendingRegistrations(); // load all
        }

        protected void gvRegistrations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string regID = e.CommandArgument.ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

            if (e.CommandName == "Approve")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Registration SET IsApproved = 1 WHERE RegistrationID = @RegID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RegID", regID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Registration approved.";
            }
            else if (e.CommandName == "Reject")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Registration WHERE RegistrationID = @RegID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RegID", regID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Registration rejected.";
            }

            LoadPendingRegistrations(txtSearch.Text.Trim()); // reload and preserve search
        }
    }

}