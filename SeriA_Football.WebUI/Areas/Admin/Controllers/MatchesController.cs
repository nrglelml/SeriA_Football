using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SeriA_Football.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MatchesController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
