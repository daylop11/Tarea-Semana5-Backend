using Microsoft.AspNetCore.Mvc;
using AWS5.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AWS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == newUser.Username);

            if (existingUser != null)
                return BadRequest("El usuario ya existe");

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado correctamente");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
                return Unauthorized("Credenciales incorrectas");

            if (user.Password != request.Password)
                return Unauthorized("La contraseña es incorrecta");

            return Ok(new
            {
                token = "token_simple_123",
                username = user.Username
            });
        }

        [HttpGet("validate")]
        public IActionResult Validate()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (authHeader == "Bearer token_simple_123")
                return Ok(new { valid = true });

            return Unauthorized(new { valid = false });
        }
    }
}