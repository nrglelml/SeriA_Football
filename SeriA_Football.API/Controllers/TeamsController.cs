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
    public class TeamsController : ControllerBase
    {
        private readonly SerieAContext _context;
        public TeamsController(SerieAContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams()
        {
            var teams = await _context.Teams.AsNoTracking().OrderBy(t=>t.Name).Select(t => new TeamDto
            {
                Id = t.Id,
                Name = t.Name,
                LogoUrl = t.LogoUrl,
                City = t.City,
                Stadium = t.Stadium
            }).ToListAsync();
            return Ok(teams);
        }
       
    }
}
