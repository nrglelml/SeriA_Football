namespace SeriA_Football.WebUI.Models
{
    public class SubstituionViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string PlayerIn { get; set; } = null!;
        public string PlayerOut { get; set; } = null!;
        public int Minute { get; set; }
    }
}
