namespace SeriA_Football.WebUI.Models
{
    public class StandingViewModel
    {
        public int Rank { get; set; }            // sıra
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string? TeamLogo { get; set; }
        public int Played { get; set; }          // O
        public int Won { get; set; }             // G
        public int Drawn { get; set; }           // B
        public int Lost { get; set; }            // M
        public int GoalsFor { get; set; }        // AG
        public int GoalsAgainst { get; set; }    // YG
        public int GoalDifference { get; set; }  // Averaj
        public int Points { get; set; }          // P
        public List<string> Form { get; set; } = new();
        public int FormPoints => Form.Sum(f => f == "W" ? 3 : f == "D" ? 1 : 0);
        public int FormMaxPoints => Form.Count * 3;
    }
}
