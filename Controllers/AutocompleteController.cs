using easyflight_mvc_dotnet_razor.Libraries;
using Microsoft.AspNetCore.Mvc;

namespace easyflight_mvc_dotnet_razor.Controllers
{
    [Route("api/[controller]")]
    public class AutocompleteController : Controller
    {

        [HttpGet("{query}")]
        async public Task<AmadeusApiSdkLocationsResponseModel> Get(string query)
        {

            DotNetEnv.Env.Load();
            string apiKey = DotNetEnv.Env.GetString("API_KEY");
            string apiSecret = DotNetEnv.Env.GetString("API_SECRET");

            AmadeusApiSdk amadeusApiSdk = new AmadeusApiSdk(apiKey, apiSecret);
            return await amadeusApiSdk.AirportAndCitySearch(query);
        }

    }
}

