using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace JPJ_Theory_Hub
{
    public partial class RoadSign : System.Web.UI.Page
    {
        // step 2: create connection
        private string conStr = ConfigurationManager.ConnectionStrings["DatabaseForAssignmentConnectionString1"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartQuestionBank();
            }
        }

        private void StartQuestionBank()
        {
            int sectionID = 1; // Road Sign is 1

            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty); // retrieve data from database
            DataTable dt = dv.ToTable(); // save data into a table which can store in session
            Session["QuestionTable"] = dt;

            int lastReadQuestionID = FindLastRead(sectionID);
            int startRow = 0;

            // new users or havent start read yet
            if (lastReadQuestionID == 0) {
                int firstQuestionID = (int)dt.Rows[startRow]["QuestionID"]; // retrieve id from dt then convert to int

                CreateNewRecord(sectionID, firstQuestionID);
            } 
            else {
                startRow = FindRow(dt, lastReadQuestionID);
                startRow++; // move to next question

                if (startRow >= dt.Rows.Count) {
                    startRow = 0; // if reach the end, back to beginning
                }
            }

            DisplayQuestion(startRow);
        }

        private int FindLastRead(int sectionID)
        {
            // step 2: create connection (auto close)
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                // step 3: open connection
                conn.Open();

                // step 4: create sql command
                string query = "SELECT [lastQuestion] FROM [StudyLog] WHERE [UserID] = @UserID AND [QuizID] = @QuizID";

                // auto close (step6)
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    comm.Parameters.AddWithValue("@UserID", Session["userID"]);
                    comm.Parameters.AddWithValue("@QuizID", sectionID);

                    //step 5: manipulate data, read n execute but return in object
                    object result = comm.ExecuteScalar();

                    if (result != null && result != DBNull.Value){
                        return (int)result; // convert to integer
                    }
                }
            }
            return 0;
        }

        private void CreateNewRecord(int sectionID, int firstQuestionID)
        {
            SqlDataSource3.InsertParameters["UserID"].DefaultValue = Session["userID"].ToString();
            SqlDataSource3.InsertParameters["QuizID"].DefaultValue = sectionID.ToString();
            SqlDataSource3.InsertParameters["lastQuestion"].DefaultValue = firstQuestionID.ToString();
            SqlDataSource3.InsertParameters["lastViewed"].DefaultValue = DateTime.Now.ToString();

            SqlDataSource3.Insert();
        }

        private int FindRow(DataTable dt, int questionID)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((int)dt.Rows[i]["QuestionID"] == questionID) {
                    return i;
                }
            }
            return 0;
        }

        private void DisplayQuestion(int row)
        {
            Session["CurrentQuestionRow"] = row;

            DataTable dt = (DataTable)Session["QuestionTable"];
            DataRow currentQuestion = dt.Rows[row];
            lblQuestionNumber.Text = $"{row + 1}";

            string question = currentQuestion["Text"].ToString();
            lblQuestionText.Text = question;

            int questionID = (int)currentQuestion["QuestionID"];
            UpdateLatestRecord(questionID);
            hfCurrentQuestionID.Value = questionID.ToString();

            string picturePath = currentQuestion["Picture"].ToString();
            if (string.IsNullOrEmpty(picturePath)) {
                imgQuestion.Visible = false;
            }
            else {
                imgQuestion.Visible = true;
                imgQuestion.ImageUrl = picturePath;
            }

            SqlDataSource2.SelectParameters["QuestionID"].DefaultValue = questionID.ToString();
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            DataTable dtOptions = dv.ToTable();

            btnOption1.Text = dtOptions.Rows[0]["Text"].ToString();
            btnOption2.Text = dtOptions.Rows[1]["Text"].ToString();
            btnOption3.Text = dtOptions.Rows[2]["Text"].ToString();
            btnOption1.CommandArgument = dtOptions.Rows[0]["Text"].ToString();
            btnOption2.CommandArgument = dtOptions.Rows[1]["Text"].ToString();
            btnOption3.CommandArgument = dtOptions.Rows[2]["Text"].ToString();
            // button design setting
            btnOption1.Enabled = true;
            btnOption2.Enabled = true;
            btnOption3.Enabled = true;
            btnOption1.CssClass = "liquid-glass orange btn kxiButton size9";
            btnOption2.CssClass = "liquid-glass orange btn kxiButton size9";
            btnOption3.CssClass = "liquid-glass orange btn kxiButton size9";
        }

        private void UpdateLatestRecord(int currentQuestionID)
        {
            int quizID = 1;
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

            ClickedEffect(btnOption1, correctAnswer, userAnswer);
            ClickedEffect(btnOption2, correctAnswer, userAnswer);
            ClickedEffect(btnOption3, correctAnswer, userAnswer);
        }

        private string GetCorrectAnswerText(int questionID)
        {
            string query = "SELECT [Text] FROM [QuestionOptionTable] WHERE [QuestionID] = @QID AND [is_correct] = 1";
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();

                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    comm.Parameters.AddWithValue("@QID", questionID);

                    return comm.ExecuteScalar()?.ToString();
                }
            }
        }

        private void ClickedEffect(Button btn, string correctAnswer, string userAnswer)
        {
            string btnArgument = btn.CommandArgument.Trim();
            string correctArg = correctAnswer.Trim();

            if (btnArgument.Equals(correctArg, StringComparison.OrdinalIgnoreCase)) {
                btn.CssClass = "liquid-glass btn kxiButton size9 correct ";
            }
            else {
                btn.CssClass = "liquid-glass btn kxiButton size9 incorrect";
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int currentRow = (int)Session["CurrentQuestionRow"];
            currentRow++;
            DisplayQuestion(currentRow);
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int currentRow = (int)Session["CurrentQuestionRow"];
            currentRow--;
            DisplayQuestion(currentRow);
        }

    }
}