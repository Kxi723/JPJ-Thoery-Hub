<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ColourBlindTest.aspx.cs" Inherits="JPJ_Theory_Hub.ColourBlindTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseForAssignmentConnectionString1 %>" 
        SelectCommand="SELECT * FROM [QuestionBankTable] WHERE [QuizID] = 4">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseForAssignmentConnectionString1 %>" 
        SelectCommand="SELECT [Text], [is_correct] FROM [QuestionOptionTable] WHERE ([QuestionID] = @QuestionID)">
        <SelectParameters>
            <asp:Parameter Name="QuestionID" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <div class="d-flex justify-content-end kxi-text-unhighlightable mb-5">
        <h1 class="fw-bold liquid-glass kxiLabel size7">Question: 
            <asp:Label ID="lblQuestionNumber" runat="server" style="color: #ff9b36 " ></asp:Label>
             / 2
        </h1>
    </div>

    <div class="col-md-12 mb-5">
        <div class="d-flex align-items-center gap-5">
            <div class="flex-shrink-1">
                <asp:Button ID="btnPrev" runat="server" Text="<<" CssClass="btn btn-outline-light liquid-glass kxiButton size10" OnClick="btnPrev_Click"/>
            </div>
            <div class="row kxi-text-unhighlightable">
                <div class="col-md-8 mx-auto liquid-glass" style="width: 1440px; padding: 10px 20px">
                    <div class="mb-5 mt-5">
                        <div class="text-center mb-5">
                            <asp:Image ID="imgQuestion" runat="server"/>
                        </div>
                        <asp:Label ID="lblQuestionText" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control kxiLabel size8"></asp:Label>
                    </div>
 
                    <div class="row mt-5 mb-4">
                        <div class="col-md-4 mb-5">
                            <asp:Button ID="btnOption1" runat="server" OnCommand="Option_Click" Text="" CssClass="liquid-glass orange btn kxiButton size9"/>
                        </div>
                        <div class="col-md-4 mb-5">
                            <asp:Button ID="btnOption2" runat="server" OnCommand="Option_Click" Text="" CssClass="liquid-glass orange btn kxiButton size9"/>
                        </div>
                        <div class="col-md-4 mb-5">
                            <asp:Button ID="btnOption3" runat="server" OnCommand="Option_Click" Text="" CssClass="liquid-glass orange btn kxiButton size9"/>
                        </div>
                    </div>
                </div>
            </div>
            <div class="flex-shrink-0">
                <asp:Button ID="btnNext" runat="server" Text=">>" CssClass="btn btn-outline-light liquid-glass kxiButton size11" OnClick="btnNext_Click"/>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfCurrentQuestionID" runat="server"/>
</asp:Content>

