using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JPJ_Theory_Hub
{
    public partial class ColourBlindTest : System.Web.UI.Page
    {
        private string constr = ConfigurationManager.ConnectionStrings["DatabaseForAssignmentConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartQuiz();
            }
        }
        private void StartQuiz()
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            Session["QuizQuestions"] = dv.ToTable();

            Session["CurrentQuestionIndex"] = 0;

            DisplayQuestion(0);
        }

        private void DisplayQuestion(int index)
        {
            DataTable dtQuestions = (DataTable)Session["QuizQuestions"];

            DataRow currentQuestion = dtQuestions.Rows[index];

            int questionID = (int)currentQuestion["QuestionID"];
            string question = currentQuestion["Text"].ToString();
            string picturePath = currentQuestion["Picture"].ToString();

            lblQuestionNumber.Text = $"{index + 1}";
            lblQuestionText.Text = question;
            hfCurrentQuestionID.Value = questionID.ToString(); // save id

            if (string.IsNullOrEmpty(picturePath))
            {
                imgQuestion.Visible = false;
            }
            else
            {
                imgQuestion.Visible = true;
                imgQuestion.ImageUrl = picturePath;
            }

            SqlDataSource2.SelectParameters["QuestionID"].DefaultValue = questionID.ToString();
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            DataTable dtOptions = dv.ToTable();
            btnOption1.Visible = true;
            btnOption2.Visible = true;
            btnOption3.Visible = true;

            btnOption1.Text = dtOptions.Rows[0]["Text"].ToString();
            btnOption1.CommandArgument = dtOptions.Rows[0]["Text"].ToString(); // past parameter

            btnOption2.Text = dtOptions.Rows[1]["Text"].ToString();
            btnOption2.CommandArgument = dtOptions.Rows[1]["Text"].ToString();

            btnOption3.Text = dtOptions.Rows[2]["Text"].ToString();
            btnOption3.CommandArgument = dtOptions.Rows[2]["Text"].ToString();

            btnOption1.Enabled = true;
            btnOption2.Enabled = true;
            btnOption3.Enabled = true;
            btnOption1.CssClass = "liquid-glass orange btn kxiButton size9";
            btnOption2.CssClass = "liquid-glass orange btn kxiButton size9";
            btnOption3.CssClass = "liquid-glass orange btn kxiButton size9";

            btnPrev.Enabled = (index > 0);
        }

        protected void Option_Click(object sender, CommandEventArgs e)
        {
            int questionID = int.Parse(hfCurrentQuestionID.Value);
            string userAnswer = e.CommandArgument.ToString();

            string correctAnswer = GetCorrectAnswerText(questionID);

            btnOption1.Enabled = false;
            btnOption2.Enabled = false;
            btnOption3.Enabled = false;

            SetButtonColor(btnOption1, correctAnswer, userAnswer);
            SetButtonColor(btnOption2, correctAnswer, userAnswer);
            SetButtonColor(btnOption3, correctAnswer, userAnswer);
        }

        private void SetButtonColor(Button btn, string correctAnswer, string userAnswer)
        {
            if (!btn.Visible) return;

            string btnArgument = btn.CommandArgument.Trim();
            string correctArg = (correctAnswer ?? "").Trim();
            string userArg = (userAnswer ?? "").Trim();

            if (btnArgument.Equals(correctArg, StringComparison.OrdinalIgnoreCase))
            {
                btn.CssClass = "liquid-glass btn kxiButton size9 correct ";
            }
            else
            {
                btn.CssClass = "liquid-glass btn kxiButton size9 incorrect";
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int currentIndex = (int)Session["CurrentQuestionIndex"];

            int totalQuestions = 2;

            currentIndex++;

            if (currentIndex >= totalQuestions)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert","alert('Congratulation！')", true);
                Response.AppendHeader("Refresh", "1;url=HomePage.aspx");
                return;
            }

            Session["CurrentQuestionIndex"] = currentIndex;
            DisplayQuestion(currentIndex);
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int currentIndex = (int)Session["CurrentQuestionIndex"];
            currentIndex--;
            DisplayQuestion(currentIndex);
        }

        private string GetCorrectAnswerText(int questionID)
        {
            string cmdText = "SELECT [Text] FROM [QuestionOptionTable] WHERE [QuestionID] = @QID AND [is_correct] = 1";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("@QID", questionID);
                    con.Open();

                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }

    }
}