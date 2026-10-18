using SeriA_Football.WebUI.Models.Enums;

namespace SeriA_Football.WebUI.Models.PageViewModels;

public class MatchDetailPageViewModel
{
    public MatchDetailViewModel Match { get; set; } = null!;

    //birleşik olaylar
    public List<MatchEventViewModel> Events
    {
        get
        {
            var list = new List<MatchEventViewModel>();

            int home = 0, away = 0;
            foreach (var g in Match.Goals.OrderBy(g => g.Minute)) //dakika başı olaylar için
            {
                bool isHome = g.TeamId == Match.HomeTeamId;
                if (isHome) home++; else away++;
                // gol olduğunda 
                list.Add(new MatchEventViewModel
                {
                    Type = MatchEventType.Goal,
                    Minute = g.Minute,
                    TeamId = g.TeamId,
                    IsHome = isHome,
                    PlayerName = g.PlayerName,
                    ScoreAfter = $"{home}-{away}"
                });
            }

            // kartlar
            foreach (var c in Match.Cards)
            {
                list.Add(new MatchEventViewModel
                {
                    Type = c.CardType switch
                    {
                        //card type enumu ile match event enumu farklı olduğu için dönüştürme yapıyoruz
                        CardTypeUI.SecondYellow => MatchEventType.SecondYellow, //ikinci sarı = kırmızı
                        CardTypeUI.Red => MatchEventType.RedCard,
                        _ => MatchEventType.YellowCard
                    },
                    Minute = c.Minute,
                    TeamId = c.TeamId,
                    IsHome = c.TeamId == Match.HomeTeamId,
                    PlayerName = c.PlayerName
                });
            }

            //oyuncu değişiklikleri
            foreach (var s in Match.Substitutions)
            {
                list.Add(new MatchEventViewModel
                {
                    Type = MatchEventType.Substitution,
                    Minute = s.Minute,
                    TeamId = s.TeamId,
                    IsHome = s.TeamId == Match.HomeTeamId,
                    PlayerName = s.PlayerIn,
                    PlayerOut = s.PlayerOut
                });
            }

            return list.OrderBy(e => e.Minute).ThenBy(e => e.Type).ToList(); //önce dakikaya sonra tipe göre sıralama
        }
    }
    //ilk yarı ve ikinci yarı goller
    public int HalfTimeHome => Match.Goals.Count(g => g.Minute <= 45 && g.TeamId == Match.HomeTeamId);
    public int HalfTimeAway => Match.Goals.Count(g => g.Minute <= 45 && g.TeamId == Match.AwayTeamId);
    public int SecondHalfHome => (Match.HomeScore ?? 0) - HalfTimeHome;
    public int SecondHalfAway => (Match.AwayScore ?? 0) - HalfTimeAway;

    //gol atan oyuncular
    public List<GoalViewModel> HomeScorers => Match.Goals.Where(g => g.TeamId == Match.HomeTeamId).OrderBy(g => g.Minute).ToList();
    public List<GoalViewModel> AwayScorers => Match.Goals.Where(g => g.TeamId == Match.AwayTeamId).OrderBy(g => g.Minute).ToList();
    //kart gören oyuncular
    public int YellowCards(int teamId) => Match.Cards.Count(c => c.TeamId == teamId && c.CardType == CardTypeUI.Yellow);
    public int RedCards(int teamId) => Match.Cards.Count(c => c.TeamId == teamId && c.CardType != CardTypeUI.Yellow);
    //oyuncu değişiklikleri
    public int Subs(int teamId) => Match.Substitutions.Count(s => s.TeamId == teamId);
    // maçta olay var mı goller, kartlar veya oyuncu değişiklikleri
    public bool HasEvents => Match.Goals.Count + Match.Cards.Count + Match.Substitutions.Count > 0;
}