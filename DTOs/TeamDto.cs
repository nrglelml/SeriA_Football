namespace SeriA_Football.API.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public string City { get; set; } = null!;
        public string Stadium { get; set; } = null!;
    }
}

