<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="recipeFinder.homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Welcome!!!!!!!!!!!!!!!!!
            <br />
            enter zip code:<br />
            <asp:TextBox ID="Zipcode" runat="server"></asp:TextBox>
        </div>
        <p>
        <asp:Button ID="LoadRecipeGenerator" runat="server" Text="Load Recipe from Generator!" OnClick="Load_Recipe_Generator" />
        <asp:Button ID="LoadRecipe" runat="server" Text="Find Random Recipe" OnClick="Load_Recipe_Random" style="margin-top: 0px" />
        </p>
        <asp:Label ID ="WeatherInformation" runat="server"></asp:Label>
        <br/>
        <asp:Label ID ="RecipeInfo" runat="server"></asp:Label>
    </form>
</body>
</html>