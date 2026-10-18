using Microsoft.AspNetCore.Mvc;
using SeriA_Football.WebUI.Models;
using SeriA_Football.WebUI.Models.PageViewModels;

namespace SeriA_Football.WebUI.Controllers
{
    public class FixtureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FixtureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int week=1)
        {
            week = Math.Clamp(week, 1, FixturePageViewModel.TotalWeeks);

            var model = new FixturePageViewModel { Week = week };

            try
            {
                var client = _httpClientFactory.CreateClient("SerieApi");

                var rows = await client.GetFromJsonAsync<List<MatchListViewModel>>($"api/matches/week/{week}");

                model.Matches = rows ?? new List<MatchListViewModel>();
            }
            catch (HttpRequestException)
            {
                ViewBag.Error = "Fixtures could not be loaded. Make sure the API is running.";
            }
            return View(model);
        }
    }
}
