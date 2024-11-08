using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vejrudsigten
{
    public class WeatherForecastRepository : IWeatherRepository
    {
        public async Task<Root> GetCurrentAsync()
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api.weatherapi.com/v1/current.json?key=6941771b74f94582ba0114956240711&q=Copenhagen&aqi=no");

     
            // Sender anmodningen og venter på svaret
            var response = client.Send(request); 

            // Tjekker om svaret var succesfuldt
            if (response.IsSuccessStatusCode)
            {
                // Læs indholdet som en string synkront
                var json = await response.Content.ReadAsStringAsync(); // Blokering indtil indholdet er tilgængeligt

                // Deserialiserer JSON til en CatFact objekt
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Indstil til at håndtere camelCase
                };

                var root = JsonSerializer.Deserialize<Root>(json, options);

                return root;
            }
            throw new Exception("Somethin went wrong");
        }
    }
}
