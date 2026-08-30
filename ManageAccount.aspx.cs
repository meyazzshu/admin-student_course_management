using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourseRegistration
{
    public partial class ManageAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["UserID"] != null)
            {
                LoadUserData();
            }
        }

        private void LoadUserData()
        {
            string userId = Session["UserID"].ToString();
            string role = Session["Role"].ToString(); // "Student" or "Admin"
            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

            string query = $"SELECT * FROM {role} WHERE {role}ID = @UserID";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblUserID.Text = userId;
                    txtName.Text = reader["Name"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPassword.Text = reader["Password"].ToString();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string userId = Session["UserID"].ToString();
            string role = Session["Role"].ToString();
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (role == "Student" && !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@student\.university\.com$"))
            {
                lblMessage.Text = "Student email must end with @student.university.com";
                return;
            }

            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;
            string query = $"UPDATE {role} SET Name = @Name, Email = @Email, Password = @Password WHERE {role}ID = @UserID";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Account updated successfully.";
                }
            }
            catch (SqlException ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }

}