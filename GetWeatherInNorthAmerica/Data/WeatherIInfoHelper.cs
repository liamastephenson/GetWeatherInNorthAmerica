using Microsoft.AspNetCore.DataProtection.KeyManagement;
using RestSharp;
using System.Drawing;
using System;
using Newtonsoft.Json.Linq;

namespace GetWeatherInNorthAmerica.Data
{
    public class WeatherIInfoHelper
    {
        public class Coord
        {
            public string lon { get; set; }
            public string lat { get; set; }
        }
        static string GetWeatherCity(string responseX)
        {
            JArray coordsArray = JArray.Parse(responseX);
            IList<Coord> coords = coordsArray.Select(p => new Coord
            {
                lat = (string)p["lat"],
                lon = (string)p["lon"]
            }).ToList();
            var client = new RestClient("http://api.openweathermap.org");
            var request = new RestRequest
       (
         "https://api.openweathermap.org/data/2.5/weather"
       );

            request.AddParameter("lat", coords[0].lat);
            request.AddParameter("lon", coords[0].lon); // Set the limit parameter
            request.AddParameter("appid", apiKey);

            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }
    }
}
