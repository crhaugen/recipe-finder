<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="recipeFinder.homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - My ASP.NET Application</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>

    <form id="form1" runat="server">
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/">Application name</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li><a runat="server" href="~/">Home</a></li>
                        <li><a runat="server" href="~/About">About</a></li>
                        <li><a runat="server" href="~/Contact">Contact</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div>
            Welcome!!!!!!!!!!!!!!!!!
            <br />
            enter zip code:<br />
            <asp:TextBox ID="Zipcode" runat="server"></asp:TextBox>
        </div>
        <p>
            <asp:Button ID="LoadRecipeGenerator" runat="server" Text="Load Recipe from Generator!" OnClick="Load_Recipe_Generator" />
            <asp:Button ID="LoadRecipe" runat="server" Text="Find Random Recipe" OnClick="Load_Recipe_Random" Style="margin-top: 0px" />
        </p>
        <asp:Label ID="WeatherInformation" runat="server"></asp:Label>
        <br />
        <asp:Label ID="RecipeInfo" runat="server"></asp:Label>
    </form>
</body>
</html>
