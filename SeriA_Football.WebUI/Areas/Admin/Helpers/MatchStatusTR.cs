using SeriA_Football.WebUI.Models.Enums;

namespace SeriA_Football.WebUI.Areas.Admin.Helpers
{
    public static class MatchStatusTR
    {
        public static string ToTurkish(this MatchStatusUI status) => status switch
        {
            MatchStatusUI.Scheduled => "Planlandı",
            MatchStatusUI.Live => "Oynanıyor",
            MatchStatusUI.Completed => "Tamamlandı",
            MatchStatusUI.Postponed => "Ertelendi",
            _ => status.ToString()
        };

        public static string ToAdminCss(this MatchStatusUI status) => status switch //css sınıfları için enumdaki isimlerin karşılığı
        {
            MatchStatusUI.Scheduled => "NotStarted",
            MatchStatusUI.Live => "Live",
            MatchStatusUI.Completed => "Finished",
            MatchStatusUI.Postponed => "Postponed",
            _ => "Başlamadı"
        };


        public static bool HasScore(this MatchStatusUI status) =>
            status is MatchStatusUI.Completed or MatchStatusUI.Live;
    }
}

