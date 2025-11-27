<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="JPJ_Theory_Hub.RegisterPage" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>JPJ Theory Hub - Group 31</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />

    <%--if 'account setting' got bug, use this--%>    
    <%--<link href="~/Content/Site.css" rel="stylesheet" runat="server" />--%>  
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>

<body class="kxiBackground">
    <form id="form1" runat="server">
        <div class="row min-vh-100 justify-content-center align-items-center kxi-text-unhighlightable">
            <div class="col-lg liquid-glass black" style="max-width: 1400px; height: 720px">
                <div class="row">
                    <div class="col-12 text-center">
                        <h1 class="text-center" style="font-size:48px; margin-bottom: 60px"">Registration</h1>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-md-5 pe-5">
                        <div class="mb-5">
                            <h6 class="text-start" style="font-size:36px">&nbsp;User Name</h6>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control liquid-glass kxiTextbox size2" />
                        </div>
                        <div class="mb-5">
                            <h6 class="text-start" style="font-size:36px">&nbsp;Email</h6>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control liquid-glass kxiTextbox size2" TextMode="Email" />
                        </div>
                        <div class="mb-5">
                            <h6 class="text-start" style="font-size:36px">&nbsp;Phone Number</h6>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control liquid-glass kxiTextbox size2" TextMode="Phone" />
                        </div>
                    </div>
                    <div class="col-md-5 ps-5">
                        <div class="mb-5">
                            <h6 class="text-start" style="font-size:36px">&nbsp;Password</h6>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control liquid-glass kxiTextbox size2" />
                        </div>                           
                        <div class="mb-5">
                            <h6 class="text-start" style="font-size:36px">&nbsp;Confirm Password</h6>
                            <asp:TextBox ID="txtCPassword" runat="server" TextMode="Password" CssClass="form-control liquid-glass kxiTextbox size2" />
                        </div>
                        <div class="text-end" style="margin-top: 100px; margin-right: 50px">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn btn-outline-light liquid-glass kxiButton size6 " />
                        </div>
                    </div>
                </div> 
            </div> 
        </div>
        <p> 
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseForAssignmentConnectionString1 %>" 
                    SelectCommand="SELECT * FROM [UsersTable]" 
                    InsertCommand="INSERT INTO [UsersTable] ([UserName], [Email], [Password], [PhoneNumber]) VALUES (@UserName, @Email, @Password, @PhoneNumber); SET @NewUserID = SCOPE_IDENTITY();"
                    OnInserted="SqlDataSource1_Inserted" >
                <InsertParameters>
                    <asp:ControlParameter ControlID="txtUserName" Name="UserName" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txtEmail" Name="Email" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txtPassword" Name="Password" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txtPhone" Name="PhoneNumber" PropertyName="Text" Type="Int32" />
                    <asp:Parameter Name="NewUserID" Type="Int32" Direction="Output" />
                </InsertParameters>
            </asp:SqlDataSource>
        </p>
    </form>
</body>
</html>