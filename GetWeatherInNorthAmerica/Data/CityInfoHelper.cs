using Microsoft.AspNetCore.DataProtection.KeyManagement;
using RestSharp;

namespace GetWeatherInNorthAmerica.Data
{
    public class CityInfoHelper
    {

        static string GetCityInfo(string city)
        {
            var client = new RestClient("http://api.openweathermap.org");
            var request = new RestRequest
            (
              "http://api.openweathermap.org/geo/1.0/direct"
            );

            request.AddParameter("q", city);
            request.AddParameter("limit", "5"); // Set the limit parameter
            request.AddParameter("appid", apiKey);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                // Parse and display the weather information
                Console.WriteLine($"Geo information for {city}:");
                Console.WriteLine(response.Content);
                return response.Content;
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
                return response.ErrorMessage;
            }


        }
    }
}
