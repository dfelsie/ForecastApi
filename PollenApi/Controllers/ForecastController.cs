using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Collections.Generic;
using PollenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace Forecast.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class LatLonPair{
        public string lat { set; get; }
        public string lon { set; get; }


    }
    public class ForecastController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpPost("api/getforecast")]
        [EnableCors("Policy1")]

        public async Task<System.Text.Json.JsonElement> GetForecast()
        {
            Response.Clear();
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            LatLonPair llp = JsonConvert.DeserializeObject<LatLonPair>(json);


            string apiKey =Environment.GetEnvironmentVariable("API_KEY");
            string url = $"http://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={llp.lat},{llp.lon}&days=5&aqi=no&alerts=no";
            

            return await ProcessRepositories(url);
            //var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            //var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);


        }

        public class Repository
        {
            public string name { get; set; }
        }

        private static async Task<System.Text.Json.JsonElement> ProcessRepositories(string url)
        {
            var hClient = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            var streamTask = client.GetStreamAsync(url);
            System.Text.Json.JsonElement repositories = await System.Text.Json.JsonSerializer.DeserializeAsync<dynamic>(await streamTask);
            return repositories;


            /*var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            foreach (var repo in repositories)
                Console.WriteLine(repo.name);*/
        }

    }

    


}
