using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriA_Football.API.DTOs;
using SerieA.API.Context;
using System.Threading.Tasks;

namespace SeriA_Football.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly SerieAContext _context;
        public MatchesController(SerieAContext context)
        {
            _context = context;
        }
        [HttpGet("week/{week}")]
        public async Task<ActionResult<IEnumerable<MatchDetailDto>>> GetMatches(int week)
        {
            var matches = await _context.Matches
        .Where(m => m.Week == week)
        .OrderBy(m => m.MatchDate)
        .Select(m => new MatchListDto
        {
            Id = m.Id,
            Week = m.Week,
            HomeTeamId = m.HomeTeamId,
            HomeTeamName = m.HomeTeam.Name,
            HomeTeamLogo = m.HomeTeam.LogoUrl,
            AwayTeamId = m.AwayTeamId,
            AwayTeamName = m.AwayTeam.Name,
            AwayTeamLogo = m.AwayTeam.LogoUrl,
            HomeScore = m.HomeScore,
            AwayScore = m.AwayScore,
            MatchDate = m.MatchDate,
            Status = m.Status,
            Stadium = m.Stadium
        })
        .ToListAsync();

            return Ok(matches);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDetailDto>> GetMatchDetail(int id)
        {
           var match = await _context.Matches.Where(m=> m.Id == id).Select(m=> new MatchDetailDto
           {
               Id = m.Id,
               Week = m.Week,
               HomeTeamId = m.HomeTeamId,
               HomeTeamName = m.HomeTeam.Name,
               HomeTeamLogo = m.HomeTeam.LogoUrl,
               AwayTeamId = m.AwayTeamId,
               AwayTeamName = m.AwayTeam.Name,
               AwayTeamLogo = m.AwayTeam.LogoUrl,
               HomeScore = m.HomeScore,
               AwayScore = m.AwayScore,
               MatchDate = m.MatchDate,
               Status = m.Status,
               Stadium = m.Stadium,
               Goals = m.Goals.Select(g => new GoalDto
               {
                   TeamId = g.TeamId,
                   TeamName = g.Team.Name,
                   PlayerName = g.PlayerName,
                   Minute = g.Minute
               }).ToList(),
               Cards = m.Cards.Select(c => new CardDto
                {
                     TeamId = c.TeamId,
                     TeamName = c.Team.Name,
                     PlayerName = c.PlayerName,
                     Minute = c.Minute,
                     CardType = c.CardType
                }).ToList(),
                Substitutions = m.Substitutions.Select(s => new SubstitutionDto
                 {
                        TeamId = s.TeamId,
                        TeamName = s.Team.Name,
                        PlayerOut = s.PlayerOut,
                        PlayerIn = s.PlayerIn,
                        Minute = s.Minute
                 }).ToList()
           }).FirstOrDefaultAsync();
            if (match == null)
                return NotFound($"{id} numaralı maç bulunamadı.");
            return Ok(match);
        }
    }
}
