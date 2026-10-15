namespace SeriA_Football.API.DTOs
{
    public class StandingDto
    {
        public int Rank { get; set; }            // sıra
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string? TeamLogo { get; set; }
        public int Played { get; set; }          // O
        public int Won { get; set; }             // Galibiyet
        public int Drawn { get; set; }           // Beraberlik
        public int Lost { get; set; }            // Mağlubiyet
        public int GoalsFor { get; set; }        // AG atılan gol
        public int GoalsAgainst { get; set; }    // YG yenilen gol
        public int GoalDifference { get; set; }  // Averaj
        public int Points { get; set; }          // P
        public List<string> Form { get; set; } = new();   // "G", "B", "M" son 5 maç 
    }
}
