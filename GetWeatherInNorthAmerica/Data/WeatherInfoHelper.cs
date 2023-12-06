using Microsoft.AspNetCore.DataProtection.KeyManagement;
using RestSharp;
using System.Drawing;
using System;
using Newtonsoft.Json.Linq;


namespace GetWeatherInNorthAmerica.Data
{
    public class WeatherReport
    {
        public string name { get; set; }   
        public string state {  get; set; }
        public string main { get; set; }
        public string description { get; set; }
        
        public string temp {  get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    

    public class WeatherInfoHelper
    {
        
       public static WeatherReport GetWeatherCity(Cities city)
        {
           
            var client = new RestClient("http://api.openweathermap.org");
            var request = new RestRequest
       (
         "https://api.openweathermap.org/data/2.5/weather"
       );
            Passwords passwords = new Passwords();
            

            request.AddParameter("lat", city.lat);
            request.AddParameter("lon", city.lon); 
            request.AddParameter("appid",passwords.getApiKey());

            var response = client.Execute(request);
            Console.WriteLine(response.Content);

            var weatherObject = JObject.Parse(response.Content);

            WeatherReport cityWeather = new WeatherReport
            {
                name = city.name,
                main = (string)weatherObject["weather"][0]["main"],
                state = city.state,
                description = (string)weatherObject["weather"][0]["description"],
                temp = (string)weatherObject["main"]["temp"]
            };

           
                cityWeather.temp = ""+(double.Parse(cityWeather.temp) - 273.15);
            

            return cityWeather;
        }
    }
}
