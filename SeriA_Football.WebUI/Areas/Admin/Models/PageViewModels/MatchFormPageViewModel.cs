using Microsoft.AspNetCore.Mvc.Rendering;

namespace SeriA_Football.WebUI.Areas.Admin.Models.PageViewModels
{
    public class MatchFormPageViewModel
    {
        public int? Id { get; set; }   //create için ? 
        public bool IsEdit => Id.HasValue;
        public MatchSaveViewModel Match { get; set; } = new();

        public List<SelectListItem> Teams { get; set; } = new();
        public List<SelectListItem> Statuses { get; set; } = new();
    }
}
