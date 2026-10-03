namespace SeriA_Football.API.Entities
{
    public class Substitution
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public string PlayerIn { get; set; } = null!;   
        public string PlayerOut { get; set; } = null!;  
        public int Minute { get; set; }

        public Match Match { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
