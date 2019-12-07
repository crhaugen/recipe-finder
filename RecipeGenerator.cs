/*
 * Chyanne Haugen and Kathleen Guinee
 * CSS 436 Program 6
 * Last edited on 12/07/2019
 * 
 * 
 * This file determines which tags should be used by the RecipeAPICall. Tags are determined based on the weather information 
 *returned by the WeatherAPICall as well as any holiday information returned from the HolidayAPICall. Right now the tag is determined
 * by a series of if/then statements, but we hope to implement machine learning in the future to improve reccomendations.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace recipeFinder
{
    public class RecipeGenerator
    {
//returns the current temperature at a given location (determined by the zipcode) in degrees farenheit
//-------------------------------getCurrentTemp(string zipCode)--------------------------
        private int getCurrentTemp(string zipCode)
        {
            weatherAPICall weather = new weatherAPICall();

            if (weather == null)
            {
                return 50;
            }

            WeatherObject weatherInfo = weather.getWeather(zipCode);

            if (weatherInfo == null)
            {
                return 50;
            }

            float kelvinTemp = weatherInfo.main.temp;
            int temp = (int)((kelvinTemp - 273.15) * 1.8 + 32);
            return temp;
        }//end getCurrentTemp

//returns the current weather conditions from a given zipcode. For example, it could return clear, clouds, or snow, as well
//as other conditions
//--------------------------getCurrentWeather(string zipCode)--------------------------
        private string getCurrentWeather(string zipCode)
        {
            weatherAPICall weather = new weatherAPICall();

            if (weather == null)
            {
                return "clear";
            }

            WeatherObject weatherInfo = weather.getWeather(zipCode);

            if (weatherInfo == null)
            {
                return "clear";
            }

            return weatherInfo.weather[0].main;
        }//end getCurrentWeather

//if it is a holiday today, then this function returns the name of that holiday, if it is not a holiday
//it returns "notaholiday". This is only applicable to the US and UK.
//-------------------------------getHoliday(DateTime date)-------------------------------------------------------
        private string getHoliday(DateTime date)
        {
            HolidayAPICall holiday = new HolidayAPICall();

            if (holiday == null)
            {
                return "notHoliday";
            }

            string holidayResults = holiday.getHoliday(date);

            if (holidayResults == null)
            {
                return "notHoliday";
            }
            return holiday.getHoliday(date);
        }//end getHoliday()

//generates a tag to be used by the RecipeCallAPI to find a random recipe. 
//-----------------------------generateRecipeType(string zipCode)-----------------------------------------
        public string generateRecipeType(string zipCode)
        {
            string typeOfFood;
            int temp = getCurrentTemp(zipCode);
            string weather = getCurrentWeather(zipCode).ToLower();

            DateTime today = DateTime.Today;
            if (getHoliday(today).ToLower().Contains("christmas"))
            {
                typeOfFood = "christmas";

            }
            else if (getHoliday(today).ToLower().Contains("thanksgiving"))
            {
                typeOfFood = "thanksgiving";
            }

            else if (getHoliday(today).ToLower().Contains("independence day"))
            {
                typeOfFood = "hotdog";

            }

            else if (getHoliday(today).ToLower().Contains("easter"))
            {
                typeOfFood = "ham";
            }
            //halloween, for some reason the API returned "world cities' day" first 
            else if (getHoliday(today).ToLower().Contains("world cities"))
            {
                typeOfFood = "candy";
            }

            else if (temp <= 32)
            {
                if (weather == "rain" || weather == "snow" || weather == "clear")
                {
                    typeOfFood = "soup";
                }
                else
                {
                    typeOfFood = "casserole";
                }
            }
            else if (temp <= 60)
            {
                if (weather == "rain")
                {
                    typeOfFood = "pasta";
                }
                else if (weather == "clear")
                {
                    typeOfFood = "spicy";
                }
                else
                {
                    typeOfFood = "chocolate";
                }
            }
            else if (temp <= 80)
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

    }//end generateRecipeType
}