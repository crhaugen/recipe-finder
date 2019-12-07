<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LoggedInMasterPage.Master" CodeBehind="homepage.aspx.cs" Inherits="recipeFinder.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%--    <body>
        <div>
            
            <br />
            enter zip code:<br />
            <asp:TextBox ID="Zipcode" runat="server"></asp:TextBox>
        </div>
        <p>
        <asp:Button ID="LoadRecipeGenerator" runat="server" Text="Load Recipe from Generator!" OnClick="Load_Recipe_Generator" />
        <asp:Button ID="IsHoliday" runat="server" Text="Is it a holiday today?" OnClick="Is_Holiday" />
        <asp:Button ID="LoadRecipe" runat="server" Text="Find Random Recipe" OnClick="Load_Recipe_Random" style="margin-top: 0px" />
        </p>
        <asp:Label ID ="WeatherInformation" runat="server"></asp:Label>
        <br/>
        <asp:Label ID ="RecipeName" runat="server"></asp:Label>
        <br />
        <asp:Label ID ="RecipeInfo" runat="server"></asp:Label>
        <asp:Label ID ="HolidayInfo" runat="server"></asp:Label>
        <asp:Button ID="SaveButton" runat="server" Text="Save Recipe!" OnClick="Save_Recipe" />
    </body>--%>


    <div class="login-page">
        <div class="form">
            <p style="font-size: large;">
                Enter Zip code.
                <br />
                <asp:TextBox ID="Zipcode" runat="server"></asp:TextBox>
                <asp:Button ID="LoadRecipeGenerator" runat="server" Text="Load Recipe from Generator!" OnClick="Load_Recipe_Generator" />
                <asp:Button ID="LoadRecipe" runat="server" Text="Find Random Recipe" OnClick="Load_Recipe_Random" Style="margin-top: 0px" />
                <br />
                <asp:Button ID="SaveButton" runat="server" Text="Save Recipe!" OnClick="Save_Recipe" />
                <br />
                <asp:Label ID ="Info" runat="server"></asp:Label>
            </p>
        </div>
        <div class="form">
            <asp:Image ID="RecipeImage" runat="server" ImageUrl="~/images/" /> 
            <br /><br />
            <asp:Label ID="RecipeName" runat="server"></asp:Label>
            <br />
            <asp:Label ID="RecipeInfo" runat="server"></asp:Label>

        </div>
    </div>

</asp:Content>