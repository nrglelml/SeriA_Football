using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriA_Football.API.DTOs;
using SeriA_Football.API.Entities;
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
                .Where(m => m.Status == MatchStatus.Completed)
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
                var form = teamMatches
  .OrderByDescending(m => m.MatchDate)
  .Take(5)
  .Select(m =>
  {
      bool isHome = m.HomeTeamId == team.Id;
      int s = isHome ? m.HomeScore ?? 0 : m.AwayScore ?? 0;
      int c = isHome ? m.AwayScore ?? 0 : m.HomeScore ?? 0;
      return s > c ? "G" : s == c ? "B" : "M";
  })
  .Reverse()
  .ToList();
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
                    Points = won * 3 + drawn,
                    Form= form,
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
