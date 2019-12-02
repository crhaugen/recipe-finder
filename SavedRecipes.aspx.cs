using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace recipeFinder
{
    public partial class SavedRecipes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            load_recipes();
        }

        private void load_recipes()
        {
            try
            {
                SaveManager sm = new SaveManager();
                string username = Session["User"].ToString();
                Dictionary<string, Uri> recipeNameAndURL = sm.getSavedRecipes(username);
                foreach (var recipe in recipeNameAndURL)
                {
                    RecipeList.Text = RecipeList.Text + recipe.Key + "<br>";
                }
            }
            catch(Exception e)
            {

            }
        }
       
    }
}