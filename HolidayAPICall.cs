/*
 * Chyanne Haugen and Kathleen Guinee
 * CSS 436 Program 6
 * Last edited on 12/07/2019
 * 
 * 
 * This file controls API calls for the holiday api (calendarific.com). Information about the holidays are used in
 * the generation of recipe reccomendations, which can be found in RecipeGenerator.cs
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Configuration;
using System.Threading;

namespace recipeFinder
{
    public class HolidayAPICall
    {
//this function returns the name of a holiday for a given date. The holidays are assumed to be in the United States,
//but may also return holidays in the UK.
//------------------getHoliday(DateTime date) -----------------------------------------
        public string getHoliday(DateTime date)
        {
            string year = date.Year.ToString();
            string day = date.Day.ToString();
            string month = date.Month.ToString();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://calendarific.com/api/v2/");
                string holiday_key = ConfigurationManager.AppSettings["HolidayAPI"];

                for(int i = 0; i < 4; i++)
                {
                    HttpResponseMessage response = client.GetAsync("holidays?&api_key=" + holiday_key + "&country=US&year=" + year + "&day=" + day + "&month=" + month).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        Debug.WriteLine(result);
                        HolidayObject holiday = JsonConvert.DeserializeObject<HolidayObject>(result);
                        if (holiday.response.holidays.Length == 0)
                        {
                            return "no holiday today";
                        }
                        string holidayInfo = holiday.response.holidays[0].name;
                        return holidayInfo;
                    }
                    else
                    {
                        Debug.WriteLine("Unsuccsessful request. Status code: " + response.StatusCode);
                        Thread.Sleep(2000 * i);
                    }
                }
            }
            return null;
        }
    }//end getHoliday()

//The following class and data is created from the special paste options for creating classes out of JSON. Holiday information is 
//stored in these classes. 
    public class HolidayObject
    {
        public Meta meta { get; set; }
        public Response response { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
    }

    public class Response
    {
        public Holiday[] holidays { get; set; }
    }

    public class Holiday
    {
        public string name { get; set; }
        public string description { get; set; }
        public Date date { get; set; }
        public string[] type { get; set; }
        public string locations { get; set; }
        public object states { get; set; }
    }

    public class Date
    {
        public string iso { get; set; }
        public Datetime datetime { get; set; }
    }

    public class Datetime
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

}