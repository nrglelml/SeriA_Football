using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeriA_Football.WebUI.Areas.Admin.Models;
using System.Net;
using System.Security.Claims;

namespace SeriA_Football.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            // Zaten giriş yapmışsa formu tekrar gösterme
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Matches", new { area = "Admin" });

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            HttpResponseMessage response;
            try
            {
                var client = _httpClientFactory.CreateClient("SerieApi");
                response = await client.PostAsJsonAsync("api/auth/login",
                    new { model.Username, model.Password });
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Sunucuya ulaşılamadı. API'nin çalıştığından emin olun.");
                return View(model);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                model.Password = "";
                return View(model);
            }

            response.EnsureSuccessStatusCode();

            // Oturum çerezi: içinde kullanıcı adı ve rolü taşınır
            var claims = new List<Claim>
        {
            new(ClaimTypes.Name, model.Username),
            new(ClaimTypes.Role, "Admin")
        };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = model.RememberMe });

            // returnUrl dışarıdan gelir; sadece kendi sitemizin adresiyse kullan (open redirect koruması)
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Matches", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
