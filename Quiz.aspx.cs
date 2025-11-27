using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JPJ_Theory_Hub
{
    public partial class Quiz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartQuiz();
            }
        }
        private void StartQuiz()
        {
            SqlDataSource4.InsertParameters["StartTime"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource4.Insert();
        }

        protected void SqlDataSourceAttempt_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                // get quiz id
                int newAttemptID = Convert.ToInt32(e.Command.Parameters["@NewAttemptID"].Value);
                Session["AttemptID"] = newAttemptID;

                // load random 50 question from questionbanktable
                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                Session["QuizQuestions"] = dv.ToTable();

                Session["CurrentQuestionIndex"] = 0;

                DisplayQuestion(0);
            }
        }

        private void DisplayQuestion(int index)
        {
            DataTable dtQuestions = (DataTable)Session["QuizQuestions"];
            // if over 50
            if (index >= dtQuestions.Rows.Count)
            {
                EndQuiz();
                return;
            }

            DataRow currentQuestion = dtQuestions.Rows[index];

            // data from questionbanktable
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

            btnOption1.Text = dtOptions.Rows[0]["Text"].ToString();
            btnOption1.CommandArgument = dtOptions.Rows[0]["Text"].ToString(); // past parameter

            btnOption2.Text = dtOptions.Rows[1]["Text"].ToString();
            btnOption2.CommandArgument = dtOptions.Rows[1]["Text"].ToString();

            btnOption3.Text = dtOptions.Rows[2]["Text"].ToString();
            btnOption3.CommandArgument = dtOptions.Rows[2]["Text"].ToString();

            int attemptID = (int)Session["AttemptID"];
            string savedAnswer = GetSavedAnswer(attemptID, questionID);

            btnOption1.Enabled = true;
            btnOption2.Enabled = true;
            btnOption3.Enabled = true;
            btnOption1.CssClass = "liquid-glass orange btn kxiButton size9";
            btnOption2.CssClass = "liquid-glass orange btn kxiButton size9";
            btnOption3.CssClass = "liquid-glass orange btn kxiButton size9";

            if (!string.IsNullOrEmpty(savedAnswer))
            {
                HighlightAnswer(savedAnswer);
            }

            btnPrev.Enabled = (index > 0);
        }

        private string GetSavedAnswer(int attemptID, int questionID)
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseForAssignmentConnectionString1"].ConnectionString;
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(constr))
            {
                string cmdText = "SELECT [UserAnswer] FROM [AnswerData] WHERE [AttemptID] = @AttemptID AND [QuestionID] = @QuestionID";
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("@AttemptID", attemptID);
                    cmd.Parameters.AddWithValue("@QuestionID", questionID);
                    con.Open();
                    // ExecuteScalar() find the fisrt one, if no then return null
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }

        private void HighlightAnswer(string savedAnswer)
        {
            string disabledCss = "liquid-glass orange btn kxiButton size9 disabled";

            if (btnOption1.CommandArgument.Trim().Equals(savedAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                btnOption1.CssClass = disabledCss;
                btnOption1.Enabled = false;
            }
            else if (btnOption2.CommandArgument.Trim().Equals(savedAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                btnOption2.CssClass = disabledCss;
                btnOption2.Enabled = false;
            }
            else if (btnOption3.CommandArgument.Trim().Equals(savedAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                btnOption3.CssClass = disabledCss;
                btnOption3.Enabled = false;
            }
        }

        protected void Option_Click(object sender, CommandEventArgs e)
        {
            int attemptID = (int)Session["AttemptID"];
            int questionID = int.Parse(hfCurrentQuestionID.Value);
            string userAnswer = e.CommandArgument.ToString();
            bool isRight = CheckAnswer(questionID, userAnswer);

            string existingAnswer = GetSavedAnswer(attemptID, questionID);

            if (!string.IsNullOrEmpty(existingAnswer))
            {
                SqlDataSource3.UpdateParameters["QuestionID"].DefaultValue = questionID.ToString();
                SqlDataSource3.UpdateParameters["UserAnswer"].DefaultValue = userAnswer;
                SqlDataSource3.UpdateParameters["is_right"].DefaultValue = isRight.ToString();
                SqlDataSource3.Update();
            }
            else
            {
                SqlDataSource3.InsertParameters["QuestionID"].DefaultValue = questionID.ToString();
                SqlDataSource3.InsertParameters["UserAnswer"].DefaultValue = userAnswer;
                SqlDataSource3.InsertParameters["is_right"].DefaultValue = isRight.ToString();
                SqlDataSource3.Insert();
            }

            btnOption1.Enabled = true;
            btnOption2.Enabled = true;
            btnOption3.Enabled = true;
            btnOption1.CssClass = "liquid-glass orange btn kxiButton size9";
            btnOption2.CssClass = "liquid-glass orange btn kxiButton size9";
            btnOption3.CssClass = "liquid-glass orange btn kxiButton size9";

            Button clickedButton = (Button)sender;
            clickedButton.Enabled = false;
            clickedButton.CssClass = "liquid-glass orange btn kxiButton size9 disabled";
        }
        private bool CheckAnswer(int questionID, string userAnswer)
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseForAssignmentConnectionString1"].ConnectionString;
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(constr))
            {
                string cmdText = "SELECT [Text] FROM [QuestionOptionTable] WHERE [QuestionID] = @QID AND [is_correct] = 1";
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("@QID", questionID);
                    con.Open();
                    string correctAnswer = cmd.ExecuteScalar()?.ToString();

                    if (string.IsNullOrEmpty(correctAnswer) || string.IsNullOrEmpty(userAnswer))
                    {
                        return false;
                    }

                    return userAnswer.Trim().Equals(correctAnswer.Trim(), StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        private void EndQuiz()
        {
            int attemptID = (int)Session["AttemptID"];
            int totalScore = 0;

            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseForAssignmentConnectionString1"].ConnectionString;
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(constr))
            {
                string cmdText = "SELECT COUNT(*) FROM [AnswerData] WHERE [AttemptID] = @AttemptID AND [is_right] = 1";
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("@AttemptID", attemptID);
                    con.Open();
                    totalScore = (int)cmd.ExecuteScalar();
                }
            }

            SqlDataSource4.UpdateParameters["FinTime"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource4.UpdateParameters["TotalScore"].DefaultValue = totalScore.ToString();
            SqlDataSource4.Update();

            Session.Remove("QuizQuestions");
            Session.Remove("CurrentQuestionIndex");

            string script = @"alert('Congratulation！\nYou have finish all question！ Your Score: " + totalScore + @"');";
            ScriptManager.RegisterStartupScript(this, GetType(), "completion", script, true);
            Response.AppendHeader("Refresh", "0;url=HomePage.aspx");
            
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int currentIndex = (int)Session["CurrentQuestionIndex"];
            currentIndex++;
            Session["CurrentQuestionIndex"] = currentIndex;

            DisplayQuestion(currentIndex);
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int currentIndex = (int)Session["CurrentQuestionIndex"];
            if (currentIndex > 0)
            {
                currentIndex--;
                Session["CurrentQuestionIndex"] = currentIndex;
                DisplayQuestion(currentIndex);
            }
        }
    }
}