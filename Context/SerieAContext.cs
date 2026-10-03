using Microsoft.EntityFrameworkCore;
using SeriA_Football.API.Entities;
using SerieA.API.Data;


namespace SerieA.API.Context;

public class SerieAContext : DbContext
{
    public SerieAContext(DbContextOptions<SerieAContext> options) : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchGoal> MatchGoals => Set<MatchGoal>();
    public DbSet<MatchCard> MatchCards => Set<MatchCard>();
    public DbSet<Substitution> Substitutions => Set<Substitution>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        // Match -> Team iki kez bağlı. SQL Server "multiple cascade paths"
        // hatası vermesin diye Restrict kullanılıyor.
        mb.Entity<Match>()
            .HasOne(m => m.HomeTeam).WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HomeTeamId).OnDelete(DeleteBehavior.Restrict);

        mb.Entity<Match>()
            .HasOne(m => m.AwayTeam).WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.AwayTeamId).OnDelete(DeleteBehavior.Restrict);

        // Ev sahibi ve deplasman aynı olamaz (DB seviyesinde ek güvence)
        mb.Entity<Match>().ToTable(t =>
            t.HasCheckConstraint("CK_Match_DifferentTeams", "[HomeTeamId] <> [AwayTeamId]"));

        // Maç silinince olayları da silinsin; takım tarafı Restrict
        mb.Entity<MatchGoal>().HasOne(x => x.Match).WithMany(m => m.Goals)
            .HasForeignKey(x => x.MatchId).OnDelete(DeleteBehavior.Cascade);
        mb.Entity<MatchGoal>().HasOne(x => x.Team).WithMany()
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Restrict);

        mb.Entity<MatchCard>().HasOne(x => x.Match).WithMany(m => m.Cards)
            .HasForeignKey(x => x.MatchId).OnDelete(DeleteBehavior.Cascade);
        mb.Entity<MatchCard>().HasOne(x => x.Team).WithMany()
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Restrict);

        mb.Entity<Substitution>().HasOne(x => x.Match).WithMany(m => m.Substitutions)
            .HasForeignKey(x => x.MatchId).OnDelete(DeleteBehavior.Cascade);
        mb.Entity<Substitution>().HasOne(x => x.Team).WithMany()
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Restrict);

        // Seed (örnek veri)
        mb.Entity<Team>().HasData(SeedData.Teams);
        mb.Entity<Match>().HasData(SeedData.Matches);
        mb.Entity<MatchGoal>().HasData(SeedData.Goals);
        mb.Entity<MatchCard>().HasData(SeedData.Cards);
        mb.Entity<Substitution>().HasData(SeedData.Substitutions);
    }
}