using SeriA_Football.API.Entities.Enums;

namespace SeriA_Football.API.DTOs
{
    public class MatchListDto
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; } = null!;
        public string? HomeTeamLogo { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; } = null!;
        public string? AwayTeamLogo { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public DateTime MatchDate { get; set; }
        public MatchStatus Status { get; set; }
        public string Stadium { get; set; } = null!;
    }
}
