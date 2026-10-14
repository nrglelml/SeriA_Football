namespace SeriA_Football.API.Entities
{
    public class MatchGoal
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }          // golü ATAN takım
        public string PlayerName { get; set; } = null!;
        public int Minute { get; set; }

        public Match Match { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
