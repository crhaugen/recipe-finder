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

        }

        protected void Load_Recipe_Random(object sender, EventArgs e)
        {
            RecipeAPICall recipeCall = new RecipeAPICall();
            RecipeInfo.Text = recipeCall.getRandomRecipe();

        }
        protected void Is_Holiday(object sender, EventArgs e)
        {
            HolidayAPICall holiday = new HolidayAPICall();
            DateTime today = DateTime.Today;
            HolidayInfo.Text = holiday.getHoliday(today);
        }
    }
}