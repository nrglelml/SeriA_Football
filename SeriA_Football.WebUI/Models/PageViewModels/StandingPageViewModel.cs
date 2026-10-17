using SeriA_Football.WebUI.Models;
using System.Globalization;

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
    public List<StatCardViewModel> StatCards
    {
        get
        {
            var cards = new List<StatCardViewModel>();
            var inv = CultureInfo.InvariantCulture;

            if (Leader != null)
            {
                string note = RunnerUp == null ? ""
                    : LeaderGap > 0 ? $"{LeaderGap} pts clear of {RunnerUp.TeamName}"
                    : $"Level on points with {RunnerUp.TeamName}";

                cards.Add(new StatCardViewModel
                {
                    Variant = "leader",
                    Label = "Leader",
                    TeamName = Leader.TeamName,
                    Note = note,
                    Value = Leader.Points.ToString(),
                    Unit = "pts"
                });
            }

            if (BestAttack != null)
                cards.Add(new StatCardViewModel
                {
                    Variant = "attack",
                    Label = "Best attack",
                    TeamName = BestAttack.TeamName,
                    Value = BestAttack.GoalsFor.ToString(),
                    Unit = "goals",
                    Note = BestAttack.Played > 0 ? $"{(double)BestAttack.GoalsFor / BestAttack.Played:0.00} per match".ToString(inv) : null
                });

            if (BestDefense != null)
                cards.Add(new StatCardViewModel
                {
                    Variant = "defence",
                    Label = "Best defence",
                    TeamName = BestDefense.TeamName,
                    Value = BestDefense.GoalsAgainst.ToString(),
                    Unit = "conceded",
                    Note = BestDefense.Played > 0 ? string.Format(inv, "{0:0.00} per match", (double)BestDefense.GoalsAgainst / BestDefense.Played) : null
                });

            if (RelegationTeam != null)
                cards.Add(new StatCardViewModel
                {
                    Variant = "danger",
                    Label = "Relegation zone",
                    TeamName = RelegationTeam.TeamName,
                    Value = $"{RelegationTeam.Rank}.",
                    Unit = "place",
                    Note = $"{RelegationTeam.Points} pts, {PointsToSafety} from safety"
                });

            return cards;
        }
    }
}