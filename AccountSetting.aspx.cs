using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JPJ_Theory_Hub
{
    public partial class AccountSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Session["userID"].ToString().Trim();

                if (id.Equals("99999"))
                {
                    btnLogin.Visible = true;
                    btnLogout.Visible = false;
                    lblProgressTitle.Visible = false;
                    gvStudyProgress.Visible = false;
                }
                else
                {
                    btnLogin.Visible = false;
                    btnLogout.Visible = true;
                    lblProgressTitle.Visible = true;
                    gvStudyProgress.Visible = true;
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("LoginPage.aspx");
        }
    }
}