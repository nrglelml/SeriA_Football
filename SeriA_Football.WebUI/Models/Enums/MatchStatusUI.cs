namespace SeriA_Football.WebUI.Models.Enums
{
    public enum MatchStatusUI
    {
        Scheduled = 0,   // Planlandı (henüz oynanmadı)
        Live = 1,        // Oynanıyor
        Completed = 2,   // Tamamlandı  -> SADECE bunlar puan durumuna dahil
        Postponed = 3    // Ertelendi
    }
}
