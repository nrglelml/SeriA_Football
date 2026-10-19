using SeriA_Football.WebUI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SeriA_Football.WebUI.Areas.Admin.Models
{
    public class MatchSaveViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Ev sahibi takım seçilmelidir.")]
        public int HomeTeamId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Deplasman takımı seçilmelidir.")]
        public int AwayTeamId { get; set; }

        [Range(1, 38)]
        public int Week { get; set; }

        [Required(ErrorMessage = "Maç tarihi girilmelidir.")]
        public DateTime? MatchDate { get; set; }

        [Range(0, 30)]
        public int? HomeScore { get; set; }

        [Range(0, 30)]
        public int? AwayScore { get; set; }

        [EnumDataType(typeof(MatchStatusUI))]
        public MatchStatusUI Status { get; set; }
    }
}
