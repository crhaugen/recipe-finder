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
        static Label[] recipeTitles = new Label[9];
        static Label[] recipeInfo = new Label[9];
        static int pageNumber = 1;
        static int totalNumOfRecipes = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            initializeLabelsArray();
            pageNumber = 1;
            load_recipes(pageNumber);
            ViewFraction.Text = "Currently seeing page " + pageNumber + "/ " + Math.Ceiling(totalNumOfRecipes / 9.0);
        }
        protected void Next_Recipes_Click(object sender, EventArgs e)
        {
            if (pageNumber < totalNumOfRecipes)
            {
                pageNumber++;
            }
            else
            {
                pageNumber = 1;
            }
            load_recipes(pageNumber);
            ViewFraction.Text = "Currently seeing page " + pageNumber + "/ " + Math.Ceiling(totalNumOfRecipes / 9.0) + " Recipes";
        }
        private void initializeLabelsArray()
        {
            recipeTitles[0] = Recipe1;
            recipeTitles[1] = Recipe2;
            recipeTitles[2] = Recipe3;
            recipeTitles[3] = Recipe4;
            recipeTitles[4] = Recipe5;
            recipeTitles[5] = Recipe6;
            recipeTitles[6] = Recipe7;
            recipeTitles[7] = Recipe8;
            recipeTitles[8] = Recipe9;

            recipeInfo[0] = Recipe1Info;
            recipeInfo[1] = Recipe2Info;
            recipeInfo[2] = Recipe3Info;
            recipeInfo[3] = Recipe4Info;
            recipeInfo[4] = Recipe5Info;
            recipeInfo[5] = Recipe6Info;
            recipeInfo[6] = Recipe7Info;
            recipeInfo[7] = Recipe8Info;
            recipeInfo[8] = Recipe9Info;
        }

        private void load_recipes(int pageNumber)
    {
            SaveManager sm = new SaveManager();
            string username = Session["User"].ToString();
            List<string> recipeNames = sm.getSavedRecipes(username);
            totalNumOfRecipes = recipeNames.Count();
            if (recipeNames == null)
            {
                //RecipeList.Text = "Sorry. Recipe could not load, please try again later.";
            }
            else
            {
                int index = (pageNumber - 1) * 9;
                for(int i = 0 ; i < 9; i++)
                {
               
                    if(index < recipeNames.Count())
                    {
                        recipeTitles[i].Text = recipeNames[index];
                        recipeInfo[i].Text = sm.getBlobContents(recipeNames[index]);
                    }
                    else
                    {

                        recipeTitles[i].Text = "";
                        recipeInfo[i].Text = "";

                    }
                    index++;
                }
            }
        }

    }
}