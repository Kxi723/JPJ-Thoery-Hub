using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JPJ_Theory_Hub
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string pass = txtPassword.Text.Trim();
            string cpass = txtCPassword.Text.Trim();
            string phoneInput = txtPhone.Text.Trim();
            string userName = txtUserName.Text.Trim();

            if (string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(cpass) || string.IsNullOrEmpty(txtEmail.Text.Trim()) || string.IsNullOrEmpty(phoneInput) || string.IsNullOrEmpty(userName))
            {
                string script = "alert('Please fill in all information.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                return;
            }

            int phoneNumber;
            if (!int.TryParse(phoneInput, out phoneNumber))
            {
                string script = "alert('Phone number must be a valid number.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                return;
            }

            if (pass != cpass)
            {
                string script = "alert('Please fill in correct password.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                return;
            }

            if (pass.Length < 8)
            {
                string script = "alert('Password must at least 8 characters.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                return;
            }

            Session["userName"] = userName;

            SqlDataSource1.Insert();
        }

        protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                int newId = Convert.ToInt32(e.Command.Parameters["@NewUserID"].Value);

                Session["userID"] = newId; 
                
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtUserName.Text = "";

                Response.AppendHeader("Refresh", "1;url=HomePage.aspx");
            }
        }
    }
}