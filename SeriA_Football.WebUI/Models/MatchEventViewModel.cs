using SeriA_Football.WebUI.Models.Enums;

namespace SeriA_Football.WebUI.Models
{
    public class MatchEventViewModel
    {
        public MatchEventType Type { get; set; }
        public int Minute { get; set; }
        public int TeamId { get; set; }
        public bool IsHome { get; set; }          
        public string PlayerName { get; set; } = null!;  
        public string? PlayerOut { get; set; }    
        public string? ScoreAfter { get; set; }   


        public string Category => Type switch
        {
            MatchEventType.Goal => "goal",
            MatchEventType.Substitution => "sub",
            _ => "card"
        };

     
        public string Label => Type switch
        {
            MatchEventType.Goal => "Goal",
            MatchEventType.YellowCard => "Yellow card",
            MatchEventType.SecondYellow => "Second yellow, sent off",
            MatchEventType.RedCard => "Red card",
            MatchEventType.Substitution => "Substitution",
            _ => ""
        };

        public bool IsFirstHalf => Minute <= 45;
        public string Key { get; set; } = "";
        public string TypeCss => Type switch
        {
            MatchEventType.Goal => "goal",
            MatchEventType.YellowCard => "yellow",
            MatchEventType.SecondYellow => "second-yellow",
            MatchEventType.RedCard => "red",
            MatchEventType.Substitution => "sub",
            _ => ""
        };

    }
}
