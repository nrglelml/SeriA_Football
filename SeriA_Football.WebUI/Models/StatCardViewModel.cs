namespace SeriA_Football.WebUI.Models
{
    public class StatCardViewModel
    {
        public string Variant { get; set; } = "leader";   // leader, attack, defence, danger (CSS sınıfı)
        public string Label { get; set; } = null!;
        public string? TeamName { get; set; }
        public string? Note { get; set; }
        public string Value { get; set; } = "-";
        public string Unit { get; set; } = "";
    }
}
