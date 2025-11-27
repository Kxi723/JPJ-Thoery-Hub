using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace JPJ_Theory_Hub
{
    public partial class RoadScenario : System.Web.UI.Page
    {
        private string constr = ConfigurationManager.ConnectionStrings["DatabaseForAssignmentConnectionString1"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartOrResumeStudySession();
            }
        }

        private void StartOrResumeStudySession()
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            DataTable dtQuestions = dv.ToTable();
            Session["QuizQuestions"] = dtQuestions;

            int quizID = 2;
            int lastQuestionID = GetLastQuestionID(quizID);
            int startIndex = 0;

            if (lastQuestionID == 0)
            {
                startIndex = 0;
                int firstQuestionID = (int)dtQuestions.Rows[startIndex]["QuestionID"];

                InsertStudyLog(quizID, firstQuestionID);
            }
            else
            {
                startIndex = FindQuestionIndex(dtQuestions, lastQuestionID);
                startIndex++;

                if (startIndex >= dtQuestions.Rows.Count)
                {
                    startIndex = 0;
                }
            }

            Session["CurrentQuestionIndex"] = startIndex;

            DisplayQuestion(startIndex);
        }

        private int GetLastQuestionID(int quizID)
        {
            if (Session["userID"] == null) return 0;

            string cmdText = "SELECT [lastQuestion] FROM [StudyLog] WHERE [UserID] = @UserID AND [QuizID] = @QuizID";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["userID"]);
                    cmd.Parameters.AddWithValue("@QuizID", quizID);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return (int)result;
                    }
                }
            }
            return 0;
        }

        private int FindQuestionIndex(DataTable dtQuestions, int questionID)
        {
            for (int i = 0; i < dtQuestions.Rows.Count; i++)
            {
                if ((int)dtQuestions.Rows[i]["QuestionID"] == questionID)
                {
                    return i;
                }
            }
            return 0;
        }

        private void DisplayQuestion(int index)
        {
            DataTable dtQuestions = (DataTable)Session["QuizQuestions"];
            int totalQuestions = dtQuestions.Rows.Count;

            if (index >= totalQuestions)
            {
                index = 0;
            }
            if (index < 0)
            {
                index = totalQuestions - 1;
            }
            Session["CurrentQuestionIndex"] = index;

            DataRow currentQuestion = dtQuestions.Rows[index];

            int questionID = (int)currentQuestion["QuestionID"];
            string text = currentQuestion["Text"].ToString();

            UpdateLastQuestion(questionID);

            lblQuestionNumber.Text = $"{index + 1}";
            lblQuestionText.Text = text;
            hfCurrentQuestionID.Value = questionID.ToString();

            SqlDataSource2.SelectParameters["QuestionID"].DefaultValue = questionID.ToString();
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            DataTable dtOptions = dv.ToTable();

            btnOption1.Visible = false;
            btnOption2.Visible = false;
            btnOption3.Visible = false;

            if (dtOptions.Rows.Count >= 1)
            {
                btnOption1.Text = dtOptions.Rows[0]["Text"].ToString();
                btnOption1.CommandArgument = dtOptions.Rows[0]["Text"].ToString();
                btnOption1.Visible = true;
            }
            if (dtOptions.Rows.Count >= 2)
            {
                btnOption2.Text = dtOptions.Rows[1]["Text"].ToString();
                btnOption2.CommandArgument = dtOptions.Rows[1]["Text"].ToString();
                btnOption2.Visible = true;
            }
            if (dtOptions.Rows.Count >= 3)
            {
                btnOption3.Text = dtOptions.Rows[2]["Text"].ToString();
                btnOption3.CommandArgument = dtOptions.Rows[2]["Text"].ToString();
                btnOption3.Visible = true;
            }

            string defaultCss = "liquid-glass orange btn kxiButton size9";

            btnOption1.Enabled = true;
            btnOption1.CssClass = defaultCss;
            btnOption2.Enabled = true;
            btnOption2.CssClass = defaultCss;
            btnOption3.Enabled = true;
            btnOption3.CssClass = defaultCss;
        }

        private void InsertStudyLog(int quizID, int firstQuestionID)
        {
            SqlDataSource3.InsertParameters["UserID"].DefaultValue = Session["userID"].ToString();
            SqlDataSource3.InsertParameters["QuizID"].DefaultValue = quizID.ToString();
            SqlDataSource3.InsertParameters["lastQuestion"].DefaultValue = firstQuestionID.ToString();
            SqlDataSource3.InsertParameters["lastViewed"].DefaultValue = DateTime.Now.ToString();

            SqlDataSource3.Insert();
        }

        private void UpdateLastQuestion(int currentQuestionID)
        {
            int quizID = 2;
            SqlDataSource3.UpdateParameters["UserID"].DefaultValue = Session["userID"].ToString();
            SqlDataSource3.UpdateParameters["QuizID"].DefaultValue = quizID.ToString();
            SqlDataSource3.UpdateParameters["lastQuestion"].DefaultValue = currentQuestionID.ToString();
            SqlDataSource3.UpdateParameters["lastViewed"].DefaultValue = DateTime.Now.ToString();

            SqlDataSource3.Update();
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
                btn.CssClass = "liquid-glass btn kxiButton correct size9";
            }
            else
            {
                btn.CssClass = "liquid-glass btn kxiButton size9 incorrect";
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int currentIndex = (int)Session["CurrentQuestionIndex"];
            currentIndex++;
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