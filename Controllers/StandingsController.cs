using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriA_Football.API.DTOs;
using SeriA_Football.API.Entities.Enums;
using SerieA.API.Context;

namespace SeriA_Football.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly SerieAContext _context;
        public StandingsController(SerieAContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StandingDto>>> GetStandings()
        {
            var teams = await _context.Teams.ToListAsync();

            var matches = await _context.Matches
                .Where(m => m.Status==MatchStatus.Completed)
                .ToListAsync();

            var standings = teams.Select(team =>
            {
                var teamMatches = matches.Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id).ToList();
                int won = 0, drawn = 0, lost = 0, goalsFor = 0, goalsAgainst = 0;
                foreach (var match in teamMatches)
                {
                    bool isHome = match.HomeTeamId == team.Id;
                    int scored = isHome ? match.HomeScore ?? 0 : match.AwayScore ?? 0;
                    int conceded = isHome ? match.AwayScore ?? 0 : match.HomeScore ?? 0;
                    goalsFor += scored;
                    goalsAgainst += conceded;

                    if (scored > conceded) won++;
                    else if (scored == conceded) drawn++;
                    else lost++;
                }
                return new StandingDto
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    TeamLogo = team.LogoUrl,
                    Played = teamMatches.Count,
                    Won = won,
                    Drawn = drawn,
                    Lost = lost,
                    GoalsFor = goalsFor,
                    GoalsAgainst = goalsAgainst,
                    GoalDifference = goalsFor - goalsAgainst,
                    Points = won * 3 + drawn
                };
                    }).OrderByDescending(s => s.Points).ThenByDescending(s => s.GoalDifference).ThenByDescending(s => s.GoalsFor).ThenBy(s => s.TeamName).ToList();
            for (int i = 0; i < standings.Count; i++)
            {
                standings[i].Rank = i + 1;
            }
            return Ok(standings);
        }
    }
}
/*
 public class StandingDto
    {
        public int Rank { get; set; }            // sıra
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string? TeamLogo { get; set; }
        public int Played { get; set; }          // O
        public int Won { get; set; }             // G
        public int Drawn { get; set; }           // B
        public int Lost { get; set; }            // M
        public int GoalsFor { get; set; }        // AG
        public int GoalsAgainst { get; set; }    // YG
        public int GoalDifference { get; set; }  // Averaj
        public int Points { get; set; }          // P
    }
 
 
 
 */