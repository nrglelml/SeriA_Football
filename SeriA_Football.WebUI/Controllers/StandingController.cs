using Microsoft.AspNetCore.Mvc;
using SeriA_Football.WebUI.Models;


namespace SeriA_Football.WebUI.Controllers
{
    public class StandingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StandingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var model = new StandingPageViewModel();

            try
            {
                var client = _httpClientFactory.CreateClient("SerieApi");

                var rows = await client.GetFromJsonAsync<List<StandingViewModel>>("api/standings");

                model.Standings = rows ?? new List<StandingViewModel>();
            }
            catch (HttpRequestException)
            {
                ViewBag.Error = "Standings could not be loaded. Make sure the API is running.";
            }
            return View(model);

        }
    }
}
