using Microsoft.AspNetCore.Mvc;

namespace SeriA_Football.WebUI.ViewComponents._StandingComponents
{
    public class _StandingRankComponent:ViewComponent
    {
        public IViewComponentResult Invoke(StandingPageViewModel model)
        {
            return View(model);
        }
    }
}
