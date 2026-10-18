using SeriA_Football.WebUI.Models.Enums;
using System.Globalization;

namespace SeriA_Football.WebUI.Models.PageViewModels
{
    public class FixturePageViewModel
    {
        public const int TotalWeeks = 38;
        private static readonly CultureInfo En = CultureInfo.GetCultureInfo("en-GB");

        public int Week { get; set; }
        public List<MatchListViewModel> Matches { get; set; } = new();

        //hafta
        public int PreviousWeek => Week - 1;
        public int NextWeek => Week + 1;
        public bool HasPrevious => Week > 1;
        public bool HasNext => Week < TotalWeeks;
        public IEnumerable<int> VisibleWeeks =>
    new[] { Week - 1, Week, Week + 1 }.Where(w => w >= 1 && w <= TotalWeeks);

        //tarih
        public DateTime? StartDate => Matches.Count == 0 ? null : Matches.Min(m => m.MatchDate);
        public DateTime? EndDate => Matches.Count == 0 ? null : Matches.Max(m => m.MatchDate);

        // 22 Aug – 24 Aug 2026
        public string DateRangeText
        {
            get
            {
                if (StartDate == null) return "No matches scheduled";
                var s = StartDate.Value; var e = EndDate!.Value;
                return s.Date == e.Date
                    ? s.ToString("d MMM yyyy", En)
                    : $"{s.ToString("d MMM", En)} – {e.ToString("d MMM yyyy", En)}";
            }
        }

        //günlere göre gruplanmış maçlar
        public List<IGrouping<DateTime, MatchListViewModel>> MatchesByDay => Matches
            .OrderBy(m => m.MatchDate)
            .GroupBy(m => m.MatchDate.Date)
            .ToList();

        //summary
        public int TotalMatches => Matches.Count;
        public int CompletedMatches => Matches.Count(m => m.Status == MatchStatusUI.Completed);
        public int LiveMatches => Matches.Count(m => m.Status == MatchStatusUI.Live);

        // skoru olan maçlar
        private IEnumerable<MatchListViewModel> Scored =>
            Matches.Where(m => m.HomeScore != null && m.AwayScore != null);
        //toplam gol
        public int GoalCount => Scored.Sum(m => m.HomeScore!.Value + m.AwayScore!.Value);
        //maç başına gol ort
        public double GoalsPerMatch => CompletedMatches == 0
            ? 0
            : (double)Matches.Where(m => m.Status == MatchStatusUI.Completed)
                             .Sum(m => m.HomeScore!.Value + m.AwayScore!.Value) / CompletedMatches;
        //biten maçlar
        private IEnumerable<MatchListViewModel> Finished =>
            Matches.Where(m => m.Status == MatchStatusUI.Completed);
        public int DrawCount => Finished.Count(m => m.HomeScore == m.AwayScore);//beraberlik
        public int HomeWinCount => Finished.Count(m => m.HomeScore > m.AwayScore);//ev sahibi galibiyeti
        public int AwayWinCount => Finished.Count(m => m.HomeScore < m.AwayScore);//deplasman galibiyeti
    }
}
