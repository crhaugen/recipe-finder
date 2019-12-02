using System;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
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
            if (Session["User"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Load_Recipe_Generator(object sender, EventArgs e)
        {
            RecipeGenerator recipeGenerator = new RecipeGenerator();
            string foodType = recipeGenerator.generateRecipeType(Zipcode.Text);
            RecipeAPICall recipeCall = new RecipeAPICall();
            RecipeInfo.Text = recipeCall.getRecipeBytype(foodType);
            RecipeName.Text = recipeCall.getRecipeName();
        }

        protected void Load_Recipe_Random(object sender, EventArgs e)
        {
            RecipeAPICall recipeCall = new RecipeAPICall();
            RecipeInfo.Text = recipeCall.getRandomRecipe();
            RecipeName.Text = recipeCall.getRecipeName();

        }
        protected void Is_Holiday(object sender, EventArgs e)
        {
            HolidayAPICall holiday = new HolidayAPICall();
            DateTime today = DateTime.Today;
            HolidayInfo.Text = holiday.getHoliday(today);
        }

        protected void Save_Recipe(object sender, EventArgs e)
        {
            if (RecipeInfo.Text.Length > 0)
            {
                SaveManager saved = new SaveManager();
                string username = Session["User"].ToString();
                saved.addRecipe(RecipeName.Text, RecipeInfo.Text, username);
                HolidayInfo.Text = "Recipe Saved!";
            }
            else
            {
                HolidayInfo.Text = "No recipe to save. Please generate or load a recipe";
            }
        }
    }
}