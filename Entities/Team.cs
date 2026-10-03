namespace SeriA_Football.API.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public string City { get; set; } = null!;
        public string Stadium { get; set; } = null!;

        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    }
}
