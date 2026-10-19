using SeriA_Football.WebUI.Models.Enums;

namespace SeriA_Football.WebUI.Helpers
{
    public static class MatchStatusExtensions
    {
        public static string ToDisplayName(this MatchStatusUI status) => status switch
        {
            MatchStatusUI.Scheduled => "Not started",
            MatchStatusUI.Live => "Live",
            MatchStatusUI.Completed => "Full time",
            MatchStatusUI.Postponed => "Postponed",
            _ => status.ToString()
        };

     
        public static string ToCss(this MatchStatusUI status) => status switch //css sınıfları için enumdaki isimlerin karşılığı
        {
            MatchStatusUI.Scheduled => "NotStarted",
            MatchStatusUI.Live => "Live",
            MatchStatusUI.Completed => "Finished",
            MatchStatusUI.Postponed => "Postponed",
            _ => "NotStarted"
        };

 
        public static bool HasScore(this MatchStatusUI status) =>
            status is MatchStatusUI.Completed or MatchStatusUI.Live;
    }
}
