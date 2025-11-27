<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThisPageIsSafeAF.aspx.cs" Inherits="JPJ_Theory_Hub.ThisPageIsSafeAF" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mt-4">
        <div class="col-12 mb-4">
            <asp:Button ID="btnSectionA" runat="server" Text="Road Sign" CssClass="btn btn-outline-light liquid-glass kxiButton size1" OnClick="btnSectionA_Click"/>
            <asp:Button ID="btnSectionB" runat="server" Text="Traffic Rule" CssClass="btn btn-outline-light liquid-glass kxiButton size2" OnClick="btnSectionB_Click" />
            <asp:Button ID="btnSectionC" runat="server" Text="Law" CssClass="btn btn-outline-light liquid-glass kxiButton size3" OnClick="btnSectionC_Click" />
        </div>
    </div>
    <div class="row kxi-text-unhighlightable">
        <div class="col-sm-3 col-md-8 mx-auto"> 
            <div class="mb-5 mt-2">
                <h6 class="text-start" style="font-size:42px">&nbsp;&nbsp;&nbsp;Question</h6>
                <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control liquid-glass kxiTextbox"></asp:TextBox>
            </div>

            <asp:Panel ID="pnlUploadPic" runat="server" Visible="false" CssClass="mb-1">
                <h6 class="text-start" style="font-size:28px">&nbsp;&nbsp;&nbsp;Upload Picture</h6>
                <asp:FileUpload ID="fileUploadPic" runat="server" CssClass="d-none" />
                <label for="<%= fileUploadPic.ClientID %>" class="btn liquid-glass kxiButton size5">
                    <div class="text-center">Choose File</div>
                </label>
            </asp:Panel>

            <h6 class="text-start mt-4" style="font-size:36px">&nbsp;&nbsp;&nbsp;Options</h6>
            <div class="row">
                <div class="col-md-4 mb-5">
                    <asp:TextBox ID="txtOption1" runat="server" CssClass="form-control liquid-glass kxiOptionbox" placeholder="  Option 1"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-5">
                    <asp:TextBox ID="txtOption2" runat="server" CssClass="form-control liquid-glass kxiOptionbox" placeholder="  Option 2"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-5">
                    <asp:TextBox ID="txtOption3" runat="server" CssClass="form-control liquid-glass kxiOptionbox" placeholder="  Option 3"></asp:TextBox>
                </div>
            </div>
            <div class="mb-5">
                <h6 class="text-start" style="font-size:28px">&nbsp;&nbsp;&nbsp;Correct Answer</h6>
                <asp:DropDownList ID="ddlCorrectAnswer" runat="server" CssClass="liquid-glass form-select">
                    <asp:ListItem Value="1">Option 1</asp:ListItem>
                    <asp:ListItem Value="2">Option 2</asp:ListItem>
                    <asp:ListItem Value="3">Option 3</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="text-end mt-4 mb-5">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-outline-light liquid-glass kxiButton size4" OnClick="btnAdd_Click" />
            </div>
        </div>
    </div>
    <p> 
        <asp:Label ID="lblSection" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblPicturePath" runat="server" Visible="False"></asp:Label>
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseForAssignmentConnectionString1 %>" 
            OnInserted="SqlDataSource1_Inserted" 
            SelectCommand="SELECT * FROM [QuestionBankTable]" 
            InsertCommand="INSERT INTO [QuestionBankTable] ([QuizID], [Text], [Picture]) VALUES (@QuizID, @Text, @Picture); SET @NewQuestionID = SCOPE_IDENTITY();">
            <InsertParameters>
                <asp:Parameter Name="QuizID" Type="Int32" />
                <asp:Parameter Name="Text" Type="String" />
                <asp:Parameter Name="Picture" Type="String" />
                <asp:Parameter Name="NewQuestionID" Type="Int32" Direction="Output" />
            </InsertParameters>
        </asp:SqlDataSource>
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseForAssignmentConnectionString1 %>" 
            SelectCommand="SELECT * FROM [QuestionOptionTable]" 
            InsertCommand="INSERT INTO [QuestionOptionTable] ([QuestionID], [Text], [is_correct]) VALUES (@QuestionID, @Text, @is_correct)">
            <InsertParameters>
                <asp:Parameter Name="QuestionID" Type="Int32" />
                <asp:Parameter Name="Text" Type="String" />
                <asp:Parameter Name="is_correct" Type="Boolean" />
            </InsertParameters>
        </asp:SqlDataSource>
    </p>
    <hr/>
    <div class="row mt-4 kxi-text-unhighlightable">
        <div class="col-md-6 mb-4">
            <h1 class="text-center">Question Table</h1>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="QuestionID" DataSourceID="SqlDataSource1" style="text-align: center">
                <Columns>
                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" InsertVisible="False" ReadOnly="True" SortExpression="QuestionID">
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle CssClass="font-monospace" Font-Size="24px" HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QuizID" HeaderText="Section" SortExpression="QuizID">
                        <ControlStyle Height="80px" />
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle CssClass="font-monospace" Font-Size="24px" HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Text" HeaderText="Question">
                        <ControlStyle Width="400px" />
                        <FooterStyle Font-Bold="False" HorizontalAlign="Left" />
                        <HeaderStyle Font-Size="24px" HorizontalAlign="Center" Width="400px" />
                    </asp:BoundField>
                    <asp:ImageField DataImageUrlField="Picture" HeaderText="Picture">
                        <ControlStyle Height="60px" Width="60px" />
                        <HeaderStyle CssClass="font-monospace" Font-Bold="True" Font-Size="24px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:ImageField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-6 mb-4">
            <h1 class="text-center">Option Table</h1>
            <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="OptionID" PageSize="24">
                <Columns>
                    <asp:BoundField DataField="OptionID" HeaderText="OptionID" InsertVisible="False" ReadOnly="True" SortExpression="OptionID">
                        <HeaderStyle CssClass="font-monospace" Font-Size="20px" HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" SortExpression="QuestionID" >
                        <HeaderStyle CssClass="font-monospace" Font-Size="20px" HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Text" HeaderText="Option" SortExpression="Text">
                        <HeaderStyle CssClass="font-monospace" Font-Bold="True" Font-Size="20px" />
                    </asp:BoundField>
                    <asp:CheckBoxField DataField="is_correct" HeaderText="is_correct" SortExpression="is_correct">
                        <HeaderStyle CssClass="font-monospace" Font-Bold="True" Font-Size="20px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
