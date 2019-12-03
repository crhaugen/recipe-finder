﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="recipeFinder._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%--<div class="row">
        <div class="col-md-4">
            <p>
                New User: 
       
                <br />
                <br />
                <asp:TextBox ID="newUserName" runat="server" placeholder="Username"></asp:TextBox><br />
                <br />
                <asp:TextBox ID="newPassword" input type="password" runat="server" placeholder="Password"></asp:TextBox><br />
                <br />
                <asp:TextBox ID="reNewPassword" input type="password" runat="server" placeholder="Repeat Password"></asp:TextBox><br />
                <br />
                <asp:Button ID="CreateUser" runat="server" Text="Create Account" OnClick="CreateUser_Click" /><br />
                &nbsp;
       
            </p>
        </div>
        <div class="col-md-4">
            <p>
                Returning User:
                <br /><br />
                <asp:TextBox ID="UserName" runat="server" placeholder="Username"></asp:TextBox>
                <br /><br />
                <asp:TextBox ID="Password" input type="password" runat="server" placeholder="Password"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="Login" runat="server" OnClick="Login_Click" Text="Login" />
                <br />
                <br />
                <p>
                    <asp:Label ID="Output" runat="server"></asp:Label>
        </div>
    </div>--%>

    <div class="login-page">
        <div class="form">
            <p style ="font-size: large;">
                Welcome to the RecipeFinder site! The best way to find something to cook!.
                <br />
                If you have trouble figuring out what to cook, this site is for you.
                <br />
                <br />
                Login or Signup to start.
            </p>
        </div>
        <div class="form">
                <asp:Label ID="Output" runat="server"></asp:Label>
                <asp:TextBox ID="UserName" runat="server" placeholder="Username"></asp:TextBox>
                <asp:TextBox ID="Password" input type="password" runat="server" placeholder="Password"></asp:TextBox>
                <asp:Button ID="Login" runat="server" OnClick="Login_Click" Text="Login" />
                <br />
                <br />
                <asp:TextBox ID="newUserName" runat="server" placeholder="Username"></asp:TextBox><br />
                <asp:TextBox ID="newPassword" input type="password" runat="server" placeholder="Password"></asp:TextBox><br />
                <asp:TextBox ID="reNewPassword" input type="password" runat="server" placeholder="Repeat Password"></asp:TextBox><br />
                <asp:Button ID="CreateUser" runat="server" Text="Create Account" OnClick="CreateUser_Click" /><br />     
               
        </div>
    </div>
</asp:Content>