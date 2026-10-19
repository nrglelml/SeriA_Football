using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriA_Football.API.DTOs;
using SeriA_Football.API.Helpers;
using SerieA.API.Context;

namespace SeriA_Football.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SerieAContext _context;

        public AuthController(SerieAContext context)
        {
            _context = context;
        }

        // POST /api/auth/login
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.AdminUsers.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            // Kullanıcı yok ya da şifre yanlış: ikisine de AYNI cevap.
            // Ayrı mesaj verilirse dışarıdan hangi kullanıcı adlarının var olduğu öğrenilebilir.
            if (user == null || !PasswordHelper.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");

            return Ok(new { user.Id, user.Username });
        }
    }
}
