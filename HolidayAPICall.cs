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
    public class HolidayAPICall
    {
        public string getHoliday(DateTime date)
        {
            string year = date.Year.ToString();
            string day = date.Day.ToString();
            string month = date.Month.ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://calendarific.com/api/v2/");
                string holiday_key = ConfigurationManager.AppSettings["HolidayAPI"];
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
                    Debug.WriteLine("Unsuccsessful request.");

                }
            }
            return null;
        }
    }

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
        public string states { get; set; }
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