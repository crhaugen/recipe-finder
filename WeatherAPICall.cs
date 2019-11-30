﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace recipeFinder
{
    public class weatherAPICall
    {
        public WeatherObject getWeather(string zip)
        {
            zip = "98122";

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
                HttpResponseMessage response = client.GetAsync("weather?zip=" + zip + "&APPID=68a17d1cd823a7b3837948dc44884dac").Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(result);
                    WeatherObject weatherInfo = JsonConvert.DeserializeObject<WeatherObject>(result);
                    return weatherInfo;
                }
                else
                {
                    Console.WriteLine("Unsuccsessful request. Please make sure the city name is spelled correctly");
                }
            }
            return null;
        }
    }
    public class WeatherObject
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public int deg { get; set; }
        public float gust { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

}