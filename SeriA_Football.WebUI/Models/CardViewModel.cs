using SeriA_Football.WebUI.Models.Enums;

namespace SeriA_Football.WebUI.Models
{
    public class CardViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string PlayerName { get; set; } = null!;
        public int Minute { get; set; }
        public CardTypeUI CardType { get; set; }

    }
}
