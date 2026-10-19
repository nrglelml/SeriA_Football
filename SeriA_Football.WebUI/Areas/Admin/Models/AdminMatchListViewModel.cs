using SeriA_Football.WebUI.Models;
using SeriA_Football.WebUI.Models.Enums;
using SeriA_Football.WebUI.Models.PageViewModels;

namespace SeriA_Football.WebUI.Areas.Admin.Models
{
    public class AdminMatchListViewModel
    {
        public int Week { get; set; }
        public string? Team { get; set; }
        public MatchStatusUI? Status { get; set; }
        public List<TeamViewModel> Teams { get; set; } = new();
        public List<MatchListViewModel> WeekMatches { get; set; } = new();   // ui tarafındaki model
        public int TotalWeeks => FixturePageViewModel.TotalWeeks;
        public List<MatchListViewModel> Matches => WeekMatches //filtreleme işlemleri
       .Where(m => string.IsNullOrWhiteSpace(Team)
                || m.HomeTeamName.Contains(Team, StringComparison.OrdinalIgnoreCase)
                || m.AwayTeamName.Contains(Team, StringComparison.OrdinalIgnoreCase))
       .Where(m => Status == null || m.Status == Status)
       .ToList();

        public bool IsFiltered => !string.IsNullOrWhiteSpace(Team) || Status != null;


    }
}
