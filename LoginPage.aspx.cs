using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JPJ_Theory_Hub
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //step 2 - create connection
            string connStr = ConfigurationManager.ConnectionStrings["DatabaseForAssignmentConnectionString1"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            //step 3 - open connection
            conn.Open();

            //step 4 - create sql command - select, insert, update, delete
            string query = "SELECT * FROM UsersTable WHERE UserName=@UserName " + "AND Password=@Password";

            SqlCommand comm = new SqlCommand(query, conn);
            comm.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
            comm.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

            //step 5 - manipulate data, read n execute
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                Session["userName"] = reader["UserName"].ToString();
                Session["userID"] = reader["UserID"].ToString();

                Response.AppendHeader("Refresh", "1;url=HomePage.aspx");
            }
            else
            {
                string script = "alert('Account Not Found.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                txtUserName.Text = "";
                return;
            }
            //step 6 - close connection
            reader.Close();
            conn.Close();
        }

        protected void lbGuestLogin_Click(object sender, EventArgs e)
        {
            Session["userName"] = "Guest";
            Session["userID"] = 99999;
            Response.AppendHeader("Refresh", "1;url=HomePage.aspx");
        }
    }
}