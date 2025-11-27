using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace JPJ_Theory_Hub
{
    public partial class ThisPageIsSafeAF : System.Web.UI.Page
    {
        private string NULL;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["originalUserName"] = Session["userName"];
            Session["userName"] = "Admin";
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Session["userName"] = Session["originalUserName"];
            Session.Remove("originalUserName");
        }

        protected void btnSectionA_Click(object sender, EventArgs e)
        {
            lblSection.Text = "1";
            pnlUploadPic.Visible = true;

            btnSectionA.CssClass = "btn btn-light liquid-glass kxiButton size1 disabled";
            btnSectionB.CssClass = "btn btn-outline-light liquid-glass kxiButton size2 abled";
            btnSectionC.CssClass = "btn btn-outline-light liquid-glass kxiButton size3 abled";
        }

        protected void btnSectionB_Click(object sender, EventArgs e)
        {
            lblSection.Text = "2";
            pnlUploadPic.Visible = false;

            btnSectionA.CssClass = "btn btn-outline-light liquid-glass kxiButton size1 abled";
            btnSectionB.CssClass = "btn btn-light liquid-glass kxiButton size2 disabled";
            btnSectionC.CssClass = "btn btn-outline-light liquid-glass kxiButton size3 abled";
        }

        protected void btnSectionC_Click(object sender, EventArgs e)
        {
            lblSection.Text = "3";
            pnlUploadPic.Visible = false;

            btnSectionA.CssClass = "btn btn-outline-light liquid-glass kxiButton size1 abled";
            btnSectionB.CssClass = "btn btn-outline-light liquid-glass kxiButton size2 abled";
            btnSectionC.CssClass = "btn btn-light liquid-glass kxiButton size3 disabled";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedSection = lblSection.Text;

            if (string.IsNullOrEmpty(selectedSection))
            {
                string script = "alert('Please select the question section.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                return;
            }

            if (fileUploadPic.HasFile)
            {
                string savePath = Server.MapPath("~/Image/");
                string fileName = System.IO.Path.GetFileName(fileUploadPic.FileName);
                string filePath = System.IO.Path.Combine(savePath, fileName);
                fileUploadPic.SaveAs(filePath);
                lblPicturePath.Text = "~/Image/" + fileName;
            }
            else
            {
                lblPicturePath.Text = NULL;
            }

            SqlDataSource1.InsertParameters["QuizID"].DefaultValue = selectedSection;
            SqlDataSource1.InsertParameters["Text"].DefaultValue = txtQuestion.Text;
            SqlDataSource1.InsertParameters["Picture"].DefaultValue = lblPicturePath.Text;

            try
            {
                SqlDataSource1.Insert();

                txtQuestion.Text = "";
                txtOption1.Text = "";
                txtOption2.Text = "";
                txtOption3.Text = "";
            }
            catch (Exception ex)
            {
                string errorScript = $"alert('Error saving data: {ex.Message.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "")}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorAlert", errorScript, true);
                return;
            }
        }

        protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            int newQuestionId = Convert.ToInt32(e.Command.Parameters["@NewQuestionID"].Value);

            InsertOption(newQuestionId, txtOption1.Text, int.Parse(ddlCorrectAnswer.SelectedValue) == 1);
            InsertOption(newQuestionId, txtOption2.Text, int.Parse(ddlCorrectAnswer.SelectedValue) == 2);
            InsertOption(newQuestionId, txtOption3.Text, int.Parse(ddlCorrectAnswer.SelectedValue) == 3);
        }

        private void InsertOption(int questionId, string optionText, bool isCorrect)
        {
            SqlDataSource2.InsertParameters["QuestionID"].DefaultValue = questionId.ToString();
            SqlDataSource2.InsertParameters["Text"].DefaultValue = optionText;
            SqlDataSource2.InsertParameters["is_correct"].DefaultValue = isCorrect ? "true" : "false";
            SqlDataSource2.Insert();
        }
    }
}