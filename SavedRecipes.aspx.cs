/*
 * Chyanne Haugen and Kathleen Guinee
 * CSS 436 Program 5
 * Last edited on 12/07/2019
 * 
 * 
 * This is the c# file that controls the buttons and functionality of the Saved Recipes page. 

The buttons are available on this page:

See more recipes -- this button returns the next 9 recipes stored in the azure recipe table where the rowkey matches the 
                    user's username. If it is the last page, and the user clicks this button, they are returned to the first 9 recipes.

Assumptions for the use of this webpage are as follows: 

Items stored in the table are assumed to have valid syntax. Sometimes the title of recipes will have characters that are
not allowed in Azure table/blob storage. In this case, nothing will be displayed.

*/
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

//if the user is not logged in, this will redirect them to the log in page. Otherwise, it loads up the stored recipes
//and displays them as an accordion.
//---------------------Page_Load(object sender, EventArgs e)----------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            initializeLabelsArray();
            load_recipes(pageNumber);
            ViewFraction.Text = "Currently seeing page " + pageNumber + "/ " + Math.Ceiling(totalNumOfRecipes / 9.0);
        }//end Page_Load()

//displays the next 9 recipes stored in the azure table query associated with this user.
//---------------------------Next_Recipes_Click(object sender, EventArgs e)
        protected void Next_Recipes_Click(object sender, EventArgs e)
        {
            if (pageNumber < Math.Ceiling(totalNumOfRecipes / 9.0))
            {
                pageNumber = pageNumber+1;
            }
            else
            {
                pageNumber = 1;
            }
            load_recipes(pageNumber);
            ViewFraction.Text = "Currently seeing page " + pageNumber + "/ " + Math.Ceiling(totalNumOfRecipes / 9.0) + " Recipes";
        }//end Next_Recipes_Click()

//private helper function that creates an array of all of the labels in the html page.
//--------------------initializLabelsArray() ---------------------------------
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
        }//end initializeLabelsArray()

//private helper function that loads all of the information from the table query (using the username as the partitionkey)
//and displays them through the labels.
//---------------------load_recipes(int pageNumber) ----------------------
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
        }//end load_recipes()

    }
}