using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using RestSharp;
using static GetWeatherInNorthAmerica.Data.WeatherInfoHelper;

namespace GetWeatherInNorthAmerica.Data
{
    public class Cities
    {
        public string name {  get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
    }

    public class CityInfoHelper
    {

       public static IList<Cities> GetCityInfo(string city)
        {
            var client = new RestClient("http://api.openweathermap.org");
            var request = new RestRequest
            (
              "http://api.openweathermap.org/geo/1.0/direct"
            );
            Passwords passwords = new Passwords();
            request.AddParameter("q", city);
            request.AddParameter("limit", "44"); // Set the limit parameter (44 is the highest recorded number of cities with the same name
            request.AddParameter("appid", passwords.getApiKey());

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {

                JArray citiesArray = JArray.Parse(response.Content);
                IList<Cities> cities = citiesArray.Select(p => new Cities
                {
                    name = (string)p["name"],
                    country = (string)p["country"],
                    state = (string)p["state"],
                    lat = (string)p["lat"],
                    lon = (string)p["lon"]
                }).ToList();


                // Parse and display the weather information
                Console.WriteLine($"Geo information for {city}:");
                Console.WriteLine(response.Content);
                return cities;
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
                return null;
            }


        }
    }
}
