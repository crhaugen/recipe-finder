<%@ Page Title="About" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="recipeFinder.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br /><br />
    <div class="form" style="text-align:left">
    <h2><%: Title %>.</h2>
    <br />
    <p>The purpose of our website is to help users discover the perfect recipe at any given time, and most especially for those moments when someone has no idea to what to make for dinner. The website uses weather data and holiday information to recommend recipes. We hope in the future to implement machine learning to help improve the recommendations. As of right now, the recommendations are based on our intuition and experience. For example, if the temperature is below 32 degrees fahrenheit and snowing, our program will recommend a soup dish, and if it is below 32 degrees fahrenheit and clear, the recipe finder will recommend a casserole. There are a variety of conditions that will generate different recommendations in this way.

The main functionalities of our website are as follows:
<ul>
    <li><b>Create an account</b> -- Users can create a new account, which will allow them to view saved recipes 
and, in the future, take advantage of the machine learning food recommendations that learns from their preferences.</li>
    <li><b>Login to an existing account</b>-- If a user has previously created an account, it will be stored in our database.  The 
user will be able to log into their stored account.</li>
    <li><b>Find a random recipe</b> -- The user can load any random recipe, if they are feeling lucky.</li>
    <li><b>Generate a recipe recommendation</b> -- The user can generate a recommendation based on information about the weather or any current holidays. In the future, we will be implementing machine learning to help hone the recommendations.</li>
    <li><b>Save recipe </b>-- If the user finds a recipe that they want to save for later, they are able to save it and later retrieve it from the “Saved Recipes” page.</li>
</ul>


The services used for this project are as follows:
<ul>
    <li><b>Azure App Service</b>  -- Microsoft’s app service allows for the easy hosting, scaling, and development of websites, especially using the .Net framework. We use the app service to host and monitor our web application.</li>
    <li><b>Azure tables</b> -- Azure tables is a NoSQL database hosted in Microsoft’s Azure cloud. We use tables for storing user information such as usernames, passwords, and the Blob URLs to saved recipes.</li>
    <li><b>Azure Blob Storage</b> -- Azure blob storage is a flexible type of storage, hosted in the cloud, that we used to save the contents (ingredients and instructions) of recipes.</li>
    <li><b>Weather API</b> -- We are using the OpenWeatherMap API to get weather and temperature data based on the user’s location (https://openweathermap.org/api). We use temperature and weather conditions to generate recipe recommendations.</li>
    <li><b>Holiday API</b> -- We are using the calendarific API to get data about any current holidays  (https://calendarific.com/). Specific holidays will generate specific types of food. Right now, the holidays are limited to those celebrated in the United States.
    <li><b>Recipe API</b> --We are using the Spoonacular API to retrieve recipes for users    (https://spoonacular.com/food-api). We specifically used the random query in our project.
    </li>
</ul>
</p>
        </div>
</asp:Content>
