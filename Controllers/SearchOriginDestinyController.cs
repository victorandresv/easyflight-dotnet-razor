using System.Net.Http.Headers;
using easyflight_mvc_dotnet_razor.Libraries;
using Microsoft.AspNetCore.Mvc;

namespace easyflight_mvc_dotnet_razor.Controllers
{
    public class SearchOriginDestinyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ConfirmOrigin(String inputOrigin)
        {
            AmadeusApiSdk amadeusApiSdk = new AmadeusApiSdk("UL3RwPkrJWESRaL6ZwWciQeEUZ772G8X", "I5rRCeudc9BzRUOM");
            string data = await amadeusApiSdk.AirportAndCitySearch();
            ViewBag.data = data;
            return View();
        }

        public IActionResult Destiny()
        {
            return View();
        }
    }
}

