using SeriA_Football.API.Entities.Enums;

namespace SeriA_Football.API.DTOs
{
    public class CardDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string PlayerName { get; set; } = null!;
        public int Minute { get; set; }
        public CardType CardType { get; set; }

    }
}
