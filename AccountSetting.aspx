<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountSetting.aspx.cs" Inherits="JPJ_Theory_Hub.AccountSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-5 pe-5">
            <div class="col-lg liquid-glass black" style="max-width: 600px;">
                <h3 class="text-center fw-bold mb-5" style="font-size:42px">Account Status</h3>
                <div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-outline-light liquid-glass kxiButton size4" OnClick="btnLogin_Click" Visible="false" />
                    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-outline-light liquid-glass kxiButton size4" OnClick="btnLogout_Click" Visible="false" />
                </div>

                <asp:Label ID="lblProgressTitle" runat="server" Visible="false">
                    <h3 class="text-center fw-bold mb-5 mt-5" style="font-size:38px">Study Progress</h3>
                </asp:Label>

                <asp:GridView ID="gvStudyProgress" runat="server" DataSourceID="SqlDataSourceProgress" AutoGenerateColumns="False" CssClass="table" GridLines="None" Visible="false">
                    <HeaderStyle ForeColor="#000000" Font-size="22px"/>
                    <RowStyle ForeColor="#000000" Font-size="20px"/>
                    <Columns>
                        <asp:TemplateField HeaderText="Quiz ID">
                            <ItemTemplate>
                                Quiz <asp:Label runat="server" Text='<%# Eval("QuizID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Progress (Last Question)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("LastQuestion") %>' /> / <asp:Label runat="server" Text='<%# Eval("TotalQuestions") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No study progress found. Start learning!
                    </EmptyDataTemplate>
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSourceProgress" runat="server"
                    ConnectionString="<%$ ConnectionStrings:DatabaseForAssignmentConnectionString1 %>"
                    SelectCommand=" WITH QuizTotals AS (SELECT QuizID, COUNT(*) AS TotalQuestions FROM questionBankTable WHERE QuizID IN (1, 2, 3) GROUP BY QuizID)
                        SELECT QT.QuizID, QT.TotalQuestions, CASE 
                        WHEN QT.QuizID = 1 THEN ISNULL(SL.lastQuestion, 0)
                        WHEN QT.QuizID = 2 THEN ISNULL(SL.lastQuestion, 50) - 50
                        WHEN QT.QuizID = 3 THEN ISNULL(SL.lastQuestion, 100) - 100
                        ELSE 0 END AS LastQuestion
                        FROM QuizTotals QT LEFT JOIN StudyLog SL ON QT.QuizID = SL.QuizID AND SL.UserID = @UserID">
                    <SelectParameters>
                        <asp:SessionParameter Name="UserID" SessionField="UserID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>

        <div class="col-md-5 ps-5">
            <div class="col-lg liquid-glass black pe-5" style="max-width: 600px">
                <h3 class="text-center fw-bold mb-5" style="font-size:42px">Quiz Record</h3>
                <asp:SqlDataSource ID="SqlDataSourceRecords" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DatabaseForAssignmentConnectionString1 %>" 
                    SelectCommand="SELECT [StartTime], CASE 
                        WHEN [FinTime] IS NULL OR [TotalScore] IS NULL THEN 'Unfinish'
                        ELSE CAST(DATEDIFF(minute, [StartTime], [FinTime]) AS VARCHAR(10)) + ' minutes' END AS DisplayDuration, CASE 
                        WHEN [FinTime] IS NULL OR [TotalScore] IS NULL THEN '-' 
                        ELSE CAST([TotalScore] AS VARCHAR(10)) + ' / 50' END AS DisplayScore
                        FROM [dbo].[AttemptData] WHERE ([UserID] = @UserID) ORDER BY [StartTime] DESC">
                    <SelectParameters>
                        <asp:SessionParameter Name="UserID" SessionField="userID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Repeater ID="RepeaterRecords" runat="server" DataSourceID="SqlDataSourceRecords">
                    <ItemTemplate>
                        <li style="margin-bottom: 15px; margin-left: 20px; font-size: 20px"">
                            <strong>Date:</strong> <%# Eval("StartTime", "{0:yyyy-MM-dd HH:mm}") %> <br />
                            <strong>Scores:</strong> <%# Eval("DisplayScore") %> <br />
                            <strong>Duration:</strong> <%# Eval("DisplayDuration") %>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>