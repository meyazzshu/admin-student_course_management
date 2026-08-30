using System;

namespace CourseRegistration
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string role = Session["Role"]?.ToString();
                string name = Session["UserName"]?.ToString();

                if (!string.IsNullOrEmpty(name))
                {
                    lblWelcome.Text = "Hi, " + name;
                    lblRole.Text = "(" + role + ")";
                }

                if (role == "Admin")
                {
                    adminLinks.Visible = true;
                    studentLinks.Visible = false;
                    lnkDashboard.NavigateUrl = "AdminDashboard.aspx";
                }
                else
                {
                    adminLinks.Visible = false;
                    studentLinks.Visible = true;
                    lnkDashboard.NavigateUrl = "StudentDashboard.aspx";
                }
            }
        }
    }

}