using SeriA_Football.API.Entities;

namespace SeriA_Football.API.DTOs
{
    public class GoalDto // “Barella 81. dakikada gol attı” gibi kayıt
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string PlayerName { get; set; } = null!;
        public int Minute { get; set; }
    }
}
