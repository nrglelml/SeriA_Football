using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeriA_Football.WebUI.Models;
using SeriA_Football.WebUI.Models.PageViewModels;
using System.Net;

namespace SeriA_Football.WebUI.Controllers
{
    [AllowAnonymous]
    public class MatchController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MatchController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient("SerieApi");
            var response = await client.GetAsync($"api/matches/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return View("NotFound");

            response.EnsureSuccessStatusCode();
            var match = await response.Content.ReadFromJsonAsync<MatchDetailViewModel>();

            return View(new MatchDetailPageViewModel { Match = match! });
        }
    }
}
