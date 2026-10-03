using SeriA_Football.API.Entities.Enums;

namespace SeriA_Football.API.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int Week { get; set; }
        public DateTime MatchDate { get; set; }   // tarih + saat birlikte
        public int? HomeScore { get; set; }       // oynanmamış maçta null
        public int? AwayScore { get; set; }
        public MatchStatus Status { get; set; }
        public string Stadium { get; set; } = null!;

        public Team HomeTeam { get; set; } = null!;
        public Team AwayTeam { get; set; } = null!;
        public ICollection<MatchGoal> Goals { get; set; } = new List<MatchGoal>();
        public ICollection<MatchCard> Cards { get; set; } = new List<MatchCard>();
        public ICollection<Substitution> Substitutions { get; set; } = new List<Substitution>();
    }
}
