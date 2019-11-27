<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="recipeFinder._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        &nbsp;</p>
    <p>
        New User:</p>
    <p>
        <asp:TextBox ID="newUserName" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:TextBox ID="newPassword" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:TextBox ID="reNewPassword" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="CreateUser" runat="server" Text="Button" OnClick="CreateUser_Click" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        Returning User:</p>
    <p>
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
   
    
   
    </p>
    <asp:TextBox ID="Password" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Login" runat="server" OnClick="Login_Click" Text="Button" />

    <br />
    <br />
    <asp:TextBox ID="Output" runat="server"></asp:TextBox>

</asp:Content>
