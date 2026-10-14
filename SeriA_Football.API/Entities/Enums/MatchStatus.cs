namespace SeriA_Football.API.Entities.Enums
{
    public enum MatchStatus
    {
        Scheduled = 0,   // Planlandı (henüz oynanmadı)
        Live = 1,        // Oynanıyor
        Completed = 2,   // Tamamlandı  -> SADECE bunlar puan durumuna dahil
        Postponed = 3    // Ertelendi
    }
}
