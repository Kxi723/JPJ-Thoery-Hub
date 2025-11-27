using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JPJ_Theory_Hub
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                lblUserName.Text = Session["userName"].ToString();
            }
            else
            {
                lblUserName.Text = "Guest";
            }
            
        }
    }
}