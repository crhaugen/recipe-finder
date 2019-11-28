<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="recipeFinder._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        &nbsp;</p>
    <p>
        New User:</p>
    <p>
        <asp:TextBox ID="newUserName" runat="server" placeholder ="Username"></asp:TextBox>
    </p>
    <p>
        <asp:TextBox ID="newPassword" input type ="password" runat="server" placeholder ="Password"></asp:TextBox>
    </p>
    <p>
        <asp:TextBox ID="reNewPassword" input type="password" runat="server" placeholder ="Repeat Password"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="CreateUser" runat="server" Text="Create Account" OnClick="CreateUser_Click" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        Returning User:</p>
    <p>
        <asp:TextBox ID="UserName" runat="server" placeholder ="Username"></asp:TextBox>
   
    
   
    </p>
    <asp:TextBox ID="Password" input type="password" runat="server" placeholder ="Password"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Login" runat="server" OnClick="Login_Click" Text="Login" />

    <br />
    <br />
    <asp:Label ID ="Output" runat="server"></asp:Label>

</asp:Content>
