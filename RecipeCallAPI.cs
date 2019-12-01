using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Configuration;

namespace recipeFinder
{
    public class RecipeAPICall
    {
        public string getRandomRecipe()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.spoonacular.com/recipes/");
                string recipe_key = ConfigurationManager.AppSettings["RecipeAPI"];
                HttpResponseMessage response = client.GetAsync("random?number=1&apiKey=" + recipe_key).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    //Debug.WriteLine(result);
                    RecipeObject recipe = JsonConvert.DeserializeObject<RecipeObject>(result);

                    return getRecipeInformation(recipe);
                }
                else
                {
                    Debug.WriteLine("Unsuccsessful request.");

                }
            }
            return null;
        }

        public string getRecipeBytype(string typeOfFood)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.spoonacular.com/recipes/");
                string recipe_key = ConfigurationManager.AppSettings["RecipeAPI"];
                HttpResponseMessage response = client.GetAsync("search?query=" + typeOfFood + "&number=1&apiKey=" + recipe_key).Result;
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("typeOfFood " + typeOfFood);
                    string result = response.Content.ReadAsStringAsync().Result;
                    Rootobject recipe = JsonConvert.DeserializeObject<Rootobject>(result);

                    string id = recipe.results[0].id.ToString();
                    HttpResponseMessage infoResponse = client.GetAsync(id + "/information?&apiKey=" + recipe_key).Result;

                    if (infoResponse.IsSuccessStatusCode)
                    {
                        string infoResult = infoResponse.Content.ReadAsStringAsync().Result;
                        Recipe recipeInfo = JsonConvert.DeserializeObject<Recipe>(infoResult);

                        RecipeObject newRecipe = new RecipeObject();
                        newRecipe.recipes = new Recipe[1];
                        newRecipe.recipes[0] = recipeInfo;
                        return getRecipeInformation(newRecipe);
                    }
                    else
                    {
                        Debug.WriteLine("Unsuccsessful request. ");
                        return null;
                    }
                }
                else
                {
                    Debug.WriteLine("Unsuccsessful request. ");
                    return null;
                }
            }
        }

        private string getRecipeInformation(RecipeObject recipe)
        {
            string ingredients = "Ingredients: <br><br>";
            for (int i = 0; i < recipe.recipes[0].extendedIngredients.Length; i++)
            {
                var currentIngredient = recipe.recipes[0].extendedIngredients[i];
                ingredients = ingredients + currentIngredient.measures.us.amount + " " + currentIngredient.measures.us.unitShort + " " + currentIngredient.name + "<br>";
            }

            string instructions = "<br> Instructions: <br><br>";
            for (int j = 0; j < recipe.recipes[0].analyzedInstructions.Length; j++)
            {
                var currentInstruction = recipe.recipes[0].analyzedInstructions[j];
                string steps = "";
                for (int k = 0; k < recipe.recipes[0].analyzedInstructions[j].steps.Length; k++)
                {
                    var currentStep = currentInstruction.steps[k];
                    steps = steps + currentStep.number.ToString() + ":   " + currentStep.step + "<br>";
                }
                instructions = instructions + currentInstruction.name + steps;
            }

            return recipe.recipes[0].title + "<br><br>" + ingredients + instructions;
        }
    }

    public class RecipeObject
    {
        public Recipe[] recipes { get; set; }
    }

    public class Recipe
    {
        public bool vegetarian { get; set; }
        public bool vegan { get; set; }
        public bool glutenFree { get; set; }
        public bool dairyFree { get; set; }
        public bool veryHealthy { get; set; }
        public bool cheap { get; set; }
        public bool veryPopular { get; set; }
        public bool sustainable { get; set; }
        public int weightWatcherSmartPoints { get; set; }
        public string gaps { get; set; }
        public bool lowFodmap { get; set; }
        public bool ketogenic { get; set; }
        public bool whole30 { get; set; }
        public int preparationMinutes { get; set; }
        public int cookingMinutes { get; set; }
        public string sourceUrl { get; set; }
        public string spoonacularSourceUrl { get; set; }
        public int aggregateLikes { get; set; }
        public float spoonacularScore { get; set; }
        public float healthScore { get; set; }
        public string creditsText { get; set; }
        public string sourceName { get; set; }
        public float pricePerServing { get; set; }
        public Extendedingredient[] extendedIngredients { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public int readyInMinutes { get; set; }
        public int servings { get; set; }
        public string image { get; set; }
        public string imageType { get; set; }
        public string[] cuisines { get; set; }
        public string[] dishTypes { get; set; }
        public string[] diets { get; set; }
        public object[] occasions { get; set; }
        public Winepairing winePairing { get; set; }
        public string instructions { get; set; }
        public Analyzedinstruction[] analyzedInstructions { get; set; }
    }

    public class Winepairing
    {
    }

    public class Extendedingredient
    {
        public int id { get; set; }
        public string aisle { get; set; }
        public string image { get; set; }
        public string consitency { get; set; }
        public string name { get; set; }
        public string original { get; set; }
        public string originalString { get; set; }
        public string originalName { get; set; }
        public float amount { get; set; }
        public string unit { get; set; }
        public string[] meta { get; set; }
        public string[] metaInformation { get; set; }
        public Measures measures { get; set; }
    }

    public class Measures
    {
        public Us us { get; set; }
        public Metric metric { get; set; }
    }

    public class Us
    {
        public float amount { get; set; }
        public string unitShort { get; set; }
        public string unitLong { get; set; }
    }

    public class Metric
    {
        public float amount { get; set; }
        public string unitShort { get; set; }
        public string unitLong { get; set; }
    }

    public class Analyzedinstruction
    {
        public string name { get; set; }
        public Step[] steps { get; set; }
    }

    public class Step
    {
        public int number { get; set; }
        public string step { get; set; }
        public Ingredient[] ingredients { get; set; }
        public Equipment[] equipment { get; set; }
        public Length length { get; set; }
    }

    public class Length
    {
        public int number { get; set; }
        public string unit { get; set; }
    }

    public class Ingredient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }

    public class Equipment
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }


    public class Rootobject
    {
        public Result[] results { get; set; }
        public string baseUri { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
        public int processingTimeMs { get; set; }
        public long expires { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public string title { get; set; }
        public int readyInMinutes { get; set; }
        public int servings { get; set; }
        public string image { get; set; }
        public string[] imageUrls { get; set; }
    }
}