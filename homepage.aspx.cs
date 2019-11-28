using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace recipeFinder
{
    public partial class homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Load_Current_Weather(object sender, EventArgs e)
        {
            weatherAPICall weather = new weatherAPICall();
            WeatherObject weatherInfo = weather.getWeather("98122");
            float kelvinTemp = weatherInfo.main.temp;
            int temp = (int)((kelvinTemp - 273.15) * 1.8 + 32);
            WeatherInformation.Text = weatherInfo.weather[0].main + "   " + temp + " \x00B0F";
        }

        protected void Load_Recipe(object sender, EventArgs e)
        {
            RecipeAPICall recipeCall = new RecipeAPICall();
            RecipeObject recipe = recipeCall.getRandomRecipe();
            string ingredients = "Ingredients: <br><br>";
            for(int i = 0; i < recipe.recipes[0].extendedIngredients.Length; i++)
            {
                var currentIngredient = recipe.recipes[0].extendedIngredients[i];
                ingredients = ingredients + currentIngredient.measures.us.amount + " " + currentIngredient.measures.us.unitShort + " " +  currentIngredient.name + "<br>";
            }

            //string instructions = "<br>Instructions: <br><br>" +recipe.recipes[0].instructions;
            string instructions = "<br> Instructions: <br><br>";
            for(int j = 0; j < recipe.recipes[0].analyzedInstructions.Length; j++)
            {
                var currentInstruction = recipe.recipes[0].analyzedInstructions[j];
                string steps = "";
                for(int k = 0; k < recipe.recipes[0].analyzedInstructions[j].steps.Length; k++)
                {
                    var currentStep = currentInstruction.steps[k];
                    steps = steps + currentStep.number.ToString() + ":   " + currentStep.step + "<br>";
                }
                instructions = instructions + currentInstruction.name + steps;
            }
            RecipeInfo.Text = recipe.recipes[0].title + "<br><br>" + ingredients + instructions;
            
        }

    }
}