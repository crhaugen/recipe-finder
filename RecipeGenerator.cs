using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recipeFinder
{
    public class RecipeGenerator
    {

        private int getCurrentTemp(string zipCode)
        {
            weatherAPICall weather = new weatherAPICall();
            WeatherObject weatherInfo = weather.getWeather(zipCode);
            float kelvinTemp = weatherInfo.main.temp;
            int temp = (int)((kelvinTemp - 273.15) * 1.8 + 32);
            return temp;
        }

        private string getCurrentWeather(string zipCode)
        {
            weatherAPICall weather = new weatherAPICall();
            WeatherObject weatherInfo = weather.getWeather(zipCode);
            return weatherInfo.weather[0].main;
        }

        public string generateRecipeType(string zipCode)
        {
            string typeOfFood;
            int temp = getCurrentTemp(zipCode);
            string weather = getCurrentWeather(zipCode).ToLower();


            if(temp <= 32)
            {
                if(weather == "rain" || weather == "snow" || weather == "clear")
                {
                    typeOfFood = "soup";
                }
                else
                {
                    typeOfFood = "casserole";
                }
            } 
            else if(temp <= 60)
            {
                if (weather == "rain")
                {
                    typeOfFood = "pasta";
                }
                else if(weather == "clear")
                {
                    typeOfFood = "spicy";
                }
                else
                {
                    typeOfFood = "chocolate";
                }
            }
            else if(temp <= 80)
            {
                if (weather == "rain")
                {
                    typeOfFood = "pasta";
                }
                else if (weather == "clear")
                {
                    typeOfFood = "burger";
                }
                else
                {
                    typeOfFood = "corn";
                }
            }
            else
            {
                typeOfFood = "ice";
            }

            return typeOfFood;
        }

    }
}