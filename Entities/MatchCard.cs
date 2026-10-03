using SeriA_Football.API.Entities.Enums;

namespace SeriA_Football.API.Entities
{
    public class MatchCard
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public string PlayerName { get; set; } = null!;
        public int Minute { get; set; }
        public CardType CardType { get; set; }

        public Match Match { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
