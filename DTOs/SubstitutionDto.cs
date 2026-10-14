namespace SeriA_Football.API.DTOs
{
    public class SubstitutionDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string PlayerIn { get; set; } = null!;
        public string PlayerOut { get; set; } = null!;
        public int Minute { get; set; }
    }
}
