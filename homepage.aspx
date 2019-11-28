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
        </div>
        <p>
        <asp:Button ID="LoadWeather" runat="server" Text="Current Weather in Seattle" OnClick="Load_Current_Weather" />
        <asp:Button ID="LoadRecipe" runat="server" Text="Find Random Recipe" OnClick="Load_Recipe" />
        </p>
        <asp:Label ID ="WeatherInformation" runat="server"></asp:Label>
        <br/>
        <asp:Label ID ="RecipeInfo" runat="server"></asp:Label>
    </form>
</body>
</html>
