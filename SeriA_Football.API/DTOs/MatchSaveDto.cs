using SeriA_Football.API.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace SeriA_Football.API.DTOs
{
    public class MatchSaveDto
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

        [EnumDataType(typeof(MatchStatus))]
        public MatchStatus Status { get; set; }
    }
}
