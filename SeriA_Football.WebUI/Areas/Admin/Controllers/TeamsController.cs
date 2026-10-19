using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeriA_Football.WebUI.Areas.Admin.Models;

namespace SeriA_Football.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TeamsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var teams = new List<TeamViewModel>();
            try
            {
                var client = _httpClientFactory.CreateClient("SerieApi");
                teams = await client.GetFromJsonAsync<List<TeamViewModel>>("api/teams") ?? new();
            }
            catch (HttpRequestException)
            {
                TempData["Error"] = "Takımlar yüklenemedi. API'nin çalıştığından emin olun.";
            }
            return View(teams);
        }
    }
}
