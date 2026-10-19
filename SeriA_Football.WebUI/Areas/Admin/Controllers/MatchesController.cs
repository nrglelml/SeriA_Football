using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeriA_Football.WebUI.Areas.Admin.Models;
using SeriA_Football.WebUI.Models;
using SeriA_Football.WebUI.Models.Enums;
using SeriA_Football.WebUI.Models.PageViewModels;
using System.Net;

namespace SeriA_Football.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MatchesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MatchesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int week=1, string? team = null, MatchStatusUI? status = null)
        {
            week = Math.Clamp(week, 1, FixturePageViewModel.TotalWeeks);
            var model = new AdminMatchListViewModel { Week = week, Team = team?.Trim(), Status = status };
            try
            {
                var client = _httpClientFactory.CreateClient("SerieApi");
                model.WeekMatches = await client.GetFromJsonAsync<List<MatchListViewModel>>($"api/matches/week/{week}")
                                ?? new List<MatchListViewModel>();
                model.Teams = await client.GetFromJsonAsync<List<TeamViewModel>>("api/teams") ?? new();
            }
            catch (HttpRequestException)
            {
                TempData["Error"] = "Maçlar yüklenemedi. API'nin çalıştığından emin olun.";
            }

            return View(model);
        }
    

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[FromBody] bu parametreyi isteğin gövdesindeki (body) JSON’dan oku
        public async Task<IActionResult> Create([FromBody] MatchSaveViewModel model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("SerieApi");
                var response = await client.PostAsJsonAsync("api/matches", model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Maç eklendi.";
                    return Json(new { ok = true });
                }

                var message = await ReadApiErrorAsync(response);
                return BadRequest(new { ok = false, message });
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, new { ok = false, message = "API'ye ulaşılamadı. API'nin çalıştığından emin olun." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromBody] MatchSaveViewModel model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("SerieApi");
                var response = await client.PutAsJsonAsync($"api/matches/{id}", model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Maç güncellendi.";
                    return Json(new { ok = true });
                }

                var message = response.StatusCode == HttpStatusCode.NotFound ?
                    "Maç bulunamadı. Silinmiş olabilir.":await ReadApiErrorAsync(response);
                return BadRequest(new { ok = false, message });
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, new { ok = false, message = "API'ye ulaşılamadı. API'nin çalıştığından emin olun." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int week)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("SerieApi");
                var response = await client.DeleteAsync($"api/matches/{id}");

                if (response.IsSuccessStatusCode)
                    TempData["Success"] = "Maç silindi.";
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    TempData["Error"] = "Maç bulunamadı. Daha önce silinmiş olabilir.";
                else
                    TempData["Error"] = "Maç silinemedi.";
            }
            catch (HttpRequestException)
            {
                TempData["Error"] = "API'ye ulaşılamadı. API'nin çalıştığından emin olun.";
            }

            return RedirectToAction(nameof(Index), new { week });
        }

        //hata mesajı
        private static async Task<string> ReadApiErrorAsync(HttpResponseMessage response)
        {
            var body = (await response.Content.ReadAsStringAsync()).Trim();
            if (body.Length == 0 || body.StartsWith("{"))
                return "Gönderilen bilgiler geçersiz. Lütfen alanları kontrol edin.";
            return body.Trim('"');
        }

    }
}
