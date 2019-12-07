using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace recipeFinder
{
    public class RecipeGenerator
    {

        private int getCurrentTemp(string zipCode)
        {
            weatherAPICall weather = new weatherAPICall();

            if(weather == null)
            {
                return 50;
            }

            WeatherObject weatherInfo = weather.getWeather(zipCode);
            float kelvinTemp = weatherInfo.main.temp;
            int temp = (int)((kelvinTemp - 273.15) * 1.8 + 32);
            return temp;
        }

        private string getCurrentWeather(string zipCode)
        {
            weatherAPICall weather = new weatherAPICall();

            if (weather == null)
            {
                return "clear";
            }

            WeatherObject weatherInfo = weather.getWeather(zipCode);
            return weatherInfo.weather[0].main;
        }

        private string getHoliday(DateTime date)
        {
            HolidayAPICall holiday = new HolidayAPICall();
            //DateTime today = DateTime.Today;
            

            string holidayResults = holiday.getHoliday(date);
            Debug.WriteLine(holidayResults);
            return holiday.getHoliday(date);
        }

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
            else if(getHoliday(today).ToLower().Contains("thanksgiving"))
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

    }
}