using SeriA_Football.WebUI.Models;

public class StandingPageViewModel
{
    public List<StandingViewModel> Standings { get; set; } = new();

    public int CurrentWeek => Standings.Count == 0 ? 0 : Standings.Max(s => s.Played);

    // Lider kartı
    public StandingViewModel? Leader => Standings.FirstOrDefault();
    public StandingViewModel? RunnerUp => Standings.Skip(1).FirstOrDefault();
    public int LeaderGap => Leader != null && RunnerUp != null ? Leader.Points - RunnerUp.Points : 0;

    // Gol kartları
    public StandingViewModel? BestAttack => Standings.MaxBy(s => s.GoalsFor);
    public StandingViewModel? BestDefense => Standings.MinBy(s => s.GoalsAgainst);

    // Düşme hattı kartı: 18. sıra ve kurtuluşa (17. sıra) kaç puan uzak
    public StandingViewModel? RelegationTeam => Standings.FirstOrDefault(s => s.Rank == 18);
    public StandingViewModel? SafeTeam => Standings.FirstOrDefault(s => s.Rank == 17);
    public int PointsToSafety => RelegationTeam != null && SafeTeam != null ? SafeTeam.Points - RelegationTeam.Points : 0;

    // Yan panel: son 5 maçta en çok puan toplayan 3 takım
    public List<StandingViewModel> InForm => Standings
        .Where(s => s.Form.Count > 0)
        .OrderByDescending(s => s.FormPoints)
        .ThenByDescending(s => s.GoalDifference)
        .Take(3)
        .ToList();

    // Puan dağılımı ekseninin üst sınırı (en yüksek puanı 5'in katına yuvarlar)
    public int LadderMax => Standings.Count == 0 ? 5 : Math.Max(5, (int)Math.Ceiling(Standings.Max(s => s.Points) / 5.0) * 5);
}