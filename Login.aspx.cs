using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using CourseRegistration.Exception;

namespace CourseRegistration
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            string role = email.ToLower() == "admin@university.com" ? "Admin" : "Student";
            string table = role;
            string idColumn = role + "ID";

            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = $"SELECT {idColumn}, Name FROM {table} WHERE Email=@Email AND Password=@Password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Session["UserID"] = reader[idColumn].ToString();
                        Session["UserName"] = reader["Name"].ToString();
                        Session["Role"] = role;

                        if (role == "Admin")
                            Response.Redirect("AdminDashboard.aspx");
                        else
                            Response.Redirect("StudentDashboard.aspx");
                    }
                    else
                    {
                        throw new InvalidCredentialsException();
                    }
                }
            }
            catch (InvalidCredentialsException ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
