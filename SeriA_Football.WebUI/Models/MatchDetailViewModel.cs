using SeriA_Football.WebUI.Models.Enums;

namespace SeriA_Football.WebUI.Models
{
    public class MatchDetailViewModel
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; } = null!;
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; } = null!;
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public DateTime MatchDate { get; set; }
        public MatchStatusUI Status { get; set; }
        public string Stadium { get; set; } = null!;
        public List<GoalViewModel> Goals { get; set; } = new();
        public List<CardViewModel> Cards { get; set; } = new();
        public List<SubstituionViewModel> Substitutions { get; set; } = new();
    }
}
