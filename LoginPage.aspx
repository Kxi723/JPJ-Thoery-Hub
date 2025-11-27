<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="JPJ_Theory_Hub.LoginPage" %>

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
            <div class="col-lg liquid-glass black" style="max-width: 1200px">
                <div class="row align-items-center">
                    <div class="col-md-7 text-center">
                        <h1 class="fw-bold font-monospace" style="font-size:70px">JPJ Theory Hub</h1>
                    </div>

                    <div class="col-md-5">
                        <div class="p-5">
                            <div class="mb-3">
                                <h6 class="text-start" style="font-size:30px">User Name</h6>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control liquid-glass kxiTextbox size1" />
                                <div class="text-end mt-2">
                                    <medium>No account, 
                                        <asp:HyperLink ID="hlSignUp" runat="server" NavigateUrl="~/RegisterPage.aspx" CssClass="kxiLinkButton set1">sign up</asp:HyperLink> here
                                    </medium>
                                </div>
                            </div>
                            <div class="mb-3">
                                <h6 class="text-start" style="font-size:30px">Password</h6>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control liquid-glass kxiTextbox size1"/>
                            </div>

                            <div class=" mt-5">
                                <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" CssClass="btn btn-outline-light liquid-glass kxiButton size6" />
                                <div class="mt-2">
                                    <medium>&nbsp;&nbsp;&nbsp;log in as 
                                        <asp:LinkButton ID="lbGuestLogin" runat="server" OnClick="lbGuestLogin_Click" CssClass="kxiLinkButton set3">guest</asp:LinkButton>
                                    </medium>
                                </div>
                            </div>
                        </div>
                    </div>
                </div> 
            </div> 
        </div>
        <p> 
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseForAssignmentConnectionString1 %>" 
                DeleteCommand="DELETE FROM [UsersTable] WHERE [UserID] = @UserID" 
                InsertCommand="INSERT INTO [UsersTable] ([UserName], [Email], [Password], [PhoneNumber]) VALUES (@UserName, @Email, @Password, @PhoneNumber)" 
                SelectCommand="SELECT * FROM [UsersTable]" 
                UpdateCommand="UPDATE [UsersTable] SET [UserName] = @UserName, [Email] = @Email, [Password] = @Password, [PhoneNumber] = @PhoneNumber WHERE [UserID] = @UserID">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="PhoneNumber" Type="Decimal" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="PhoneNumber" Type="Decimal" />
                    <asp:Parameter Name="UserID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </p>
    </form>
</body>
</html>