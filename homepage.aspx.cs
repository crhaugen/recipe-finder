/*
 * Chyanne Haugen and Kathleen Guinee
 * CSS 436 Program 6
 * Last edited on 12/07/2019
 * 
 * 
 * This is the c# file that controls the buttons and functionality of the recipe generation and finder page.

The buttons are available on this page:

Load Recipe from Generator -- uses the recipe generator class to load a reccomended recipe based on weather information 
                                at a specific location which is provided by the Zip Code.

Find random recipe -- uses the RecipeCallAPI class to return a random recipe.

Save Recipe -- Uses the SaveManager class to save the recipe information (title, ingredients, and instructions) to a blob.

Assumptions for the use of this webpage are as follows: 

ZipCodes are assumed to be entered as valid zip code numbers. 

*/
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
        static string IMG_URL = "";

//Loads the page. If the user tries to access this page without being logged in, 
//they will be redirected to the log in page.
 //--------------Page_Load(object sender, EventArgs e) ----------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if(IMG_URL.Length == 0)
            {
                RecipeImage.Visible = false;
            }
            else
            {
                RecipeImage.Visible = true;
            }   
        }//end Page_Load()

//This function uses the Receipe Generator class to come up with a a reccomendation based on weather information
//for a given zipcode. 
//-------------------Load_Recipe_Generator(object sender, EventArgs e) -----------------------------------------------
        protected void Load_Recipe_Generator(object sender, EventArgs e)
        {
            Info.Text = "";
            if(Zipcode.Text.Length == 0)
            {
                RecipeInfo.Text = "Sorry, we need your zipcode to use the recommendation generator. Please enter a zipcode and try again";
                return;
            }
            RecipeGenerator recipeGenerator = new RecipeGenerator();
            string foodType = recipeGenerator.generateRecipeType(Zipcode.Text);
            RecipeAPICall recipeCall = new RecipeAPICall();
            string type = recipeCall.getRecipeBytype(foodType);
            string name = recipeCall.getRecipeName();
            string imgURL = recipeCall.getImgURL();

            if (type == null || name == null)
            {
                RecipeInfo.Text = "Sorry. Could not load recipe at the moment, please try again later.";
              
            }
            else
            {
                RecipeInfo.Text = type;
                RecipeName.Text = name;
                RecipeImage.Visible = true;
                RecipeImage.ImageUrl = imgURL;
                IMG_URL = imgURL;
            }
          
        }//end Load_Recipe_Generator()

//add comments here
//--------------------Load_Recipe_Random(object sender, EventArgs e)---------------
        protected void Load_Recipe_Random(object sender, EventArgs e)
        {
            Info.Text = "";
            RecipeAPICall recipeCall = new RecipeAPICall();

            string intstructions = recipeCall.getRandomRecipe();
            string name = recipeCall.getRecipeName();
            string imgURL = recipeCall.getImgURL();

            if (intstructions == null || name == null)
            {
                RecipeInfo.Text = "Sorry. Could not load recipe at the moment, please try again later.";

            }
            else
            {
                RecipeInfo.Text = intstructions;
                RecipeName.Text = name;
                
                if(imgURL!= null)
                {
                    RecipeImage.Visible = true;
                    RecipeImage.ImageUrl = imgURL;
                }
                else
                {
                    RecipeImage.Visible = false;
                }
                IMG_URL = imgURL;
            }

        }//end Load_Recipe_Random()

//helper class for testing purposes
//--------------------Is_Holiday(object sender, EventArgs e)
        protected void Is_Holiday(object sender, EventArgs e)
        {
            HolidayAPICall holiday = new HolidayAPICall();
            DateTime today = DateTime.Today;
            //HolidayInfo.Text = holiday.getHoliday(today);
        }//end Is_Holiday()

//add comments here
//--------------------Save_Recipe(object sender, EventArgs e)--------------------------
        protected void Save_Recipe(object sender, EventArgs e)
        {
            if (RecipeInfo.Text.Length > 0)
            {
                SaveManager saved = new SaveManager();
                string username = Session["User"].ToString();
                saved.addRecipe(RecipeName.Text, RecipeInfo.Text, username);
                Info.Text = "Recipe Saved!";
                
            }
            else
            {
                Info.Text = "No recipe to save. Please generate or load a recipe";
            }
        }//end Save_Recipe(object sender, EventArgs e)
    }
}