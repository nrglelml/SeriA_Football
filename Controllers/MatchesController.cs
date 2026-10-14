using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriA_Football.API.DTOs;
using SeriA_Football.API.Entities;
using SeriA_Football.API.Entities.Enums;
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
        public async Task<ActionResult<IEnumerable<MatchListDto>>> GetMatches(int week)
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
            var match = await _context.Matches.Where(m => m.Id == id).Select(m => new MatchDetailDto
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
                Goals = m.Goals.OrderBy(g => g.Minute).Select(g => new GoalDto
                {
                    TeamId = g.TeamId,
                    TeamName = g.Team.Name,
                    PlayerName = g.PlayerName,
                    Minute = g.Minute
                }).ToList(),
                Cards = m.Cards.OrderBy(c => c.Minute).Select(c => new CardDto
                {
                    TeamId = c.TeamId,
                    TeamName = c.Team.Name,
                    PlayerName = c.PlayerName,
                    Minute = c.Minute,
                    CardType = c.CardType
                }).ToList(),
                Substitutions = m.Substitutions.OrderBy(s => s.Minute).Select(s => new SubstitutionDto
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
                return NotFound($"{id} numaralı maç bulunamadı.");
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch(MatchSaveDto matchDto)
        {
            var errorMessage = await ValidateMatchAsync(matchDto, 0);
            if (errorMessage != null)
                return BadRequest(errorMessage);
            var homeTeam = await _context.Teams.FindAsync(matchDto.HomeTeamId);
            var match = new Match
            {
                Week = matchDto.Week,
                HomeTeamId = matchDto.HomeTeamId,
                AwayTeamId = matchDto.AwayTeamId,
                HomeScore = matchDto.HomeScore,
                AwayScore = matchDto.AwayScore,
                MatchDate = matchDto.MatchDate ?? DateTime.Now,
                Status = matchDto.Status,
               Stadium = homeTeam!.Stadium
            };
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatchDetail), new { id = match.Id }, new { match.Id });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(int id, MatchSaveDto matchDto)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
                return NotFound($"{id} numaralı maç bulunamadı.");

            var errorMessage = await ValidateMatchAsync(matchDto, id);
            if (errorMessage != null)
                return BadRequest(errorMessage);

            var homeTeam = await _context.Teams.FindAsync(matchDto.HomeTeamId);


            match.Week = matchDto.Week;
            match.HomeTeamId = matchDto.HomeTeamId;
            match.AwayTeamId = matchDto.AwayTeamId;
            match.HomeScore = matchDto.HomeScore;
            match.AwayScore = matchDto.AwayScore;
            match.MatchDate = matchDto.MatchDate ?? DateTime.Now;
            match.Status = matchDto.Status;
            match.Stadium = homeTeam!.Stadium;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private async Task<string?> ValidateMatchAsync(MatchSaveDto dto, int id)
        {
            if (dto.HomeTeamId == dto.AwayTeamId)
                return "Ev sahibi ve deplasman takımları aynı olamaz.";

            var homeTeam = await _context.Teams.FindAsync(dto.HomeTeamId);
            if (homeTeam == null)
                return $"Ev sahibi takım bulunamadı: {dto.HomeTeamId}";

            var awayTeam = await _context.Teams.FindAsync(dto.AwayTeamId);
            if (awayTeam == null)
                return $"Deplasman takımı bulunamadı: {dto.AwayTeamId}";

            bool conflict = await _context.Matches.AnyAsync(m =>
    m.Week == dto.Week &&
    m.Id != id &&
    (m.HomeTeamId == dto.HomeTeamId || m.AwayTeamId == dto.HomeTeamId ||
     m.HomeTeamId == dto.AwayTeamId || m.AwayTeamId == dto.AwayTeamId));

            if (conflict)
                return "Takımlardan biri bu hafta zaten başka bir maçta yer alıyor.";

            if (dto.Status == MatchStatus.Completed && (dto.HomeScore == null || dto.AwayScore == null))
                return "Tamamlanan maçın skoru girilmelidir.";

            if (dto.Status == MatchStatus.Scheduled && (dto.HomeScore != null || dto.AwayScore != null))
                return "Henüz oynanmamış bir maça skor girilemez.";
            return null;

        }
    }



}