using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using CourseRegistration.Exception;

namespace CourseRegistration
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString;

            try
            {
                // Check if email already exists
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string checkQuery = "SELECT COUNT(*) FROM Student WHERE Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@Email", email);
                    con.Open();
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new DuplicateEmailException(email);
                    }
                }

                // Check email format
                if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@student\.university\.com$"))
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Email must be in the format name@student.university.com";
                    return;
                }

                // Generate new student ID
                string newStudentID = GenerateStudentID(connectionString);

                // Insert into DB
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string insertQuery = "INSERT INTO Student (StudentID, Name, Email, Password) VALUES (@StudentID, @Name, @Email, @Password)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                    insertCmd.Parameters.AddWithValue("@StudentID", newStudentID);
                    insertCmd.Parameters.AddWithValue("@Name", name);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    insertCmd.ExecuteNonQuery();
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = $"✅ Registration successful! Your Student ID is {newStudentID}. Please login.";
                }
            }
            catch (DuplicateEmailException ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "An unexpected error occurred: " + ex.Message;
            }
        }

        private string GenerateStudentID(string connectionString)
        {
            int nextNumber = 1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 StudentID FROM Student ORDER BY StudentID DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string lastId = result.ToString(); // e.g. S0005
                    if (lastId.Length > 1 && int.TryParse(lastId.Substring(1), out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
            }

            return "S" + nextNumber.ToString("D4"); // S0001, S0002, ...
        }
    }
}
