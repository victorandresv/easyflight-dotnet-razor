using System.Net.Http.Headers;
using amadeus;
using Microsoft.AspNetCore.Mvc;

namespace easyflight_mvc_dotnet_razor.Controllers
{
    public class SearchOriginDestinyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConfirmOrigin(String inputOrigin)
        {
            Amadeus amadeus = Amadeus
                    .builder("UL3RwPkrJWESRaL6ZwWciQeEUZ772G8X", "I5rRCeudc9BzRUOM")
                    .build();
            Response results = amadeus.get("/v1/reference-data/locations", Params.with("subType", "AIRPORT").and("keyword", inputOrigin));
            ViewBag.data = results.data;
            return View();
        }

        public IActionResult Destiny()
        {
            return View();
        }
    }
}

