using Microsoft.AspNetCore.Mvc;
using AWS5.Models;
using System.Collections.Generic;
using System.Linq;

namespace AWS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static List<User> users = new List<User>();

        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (users.Any(u => u.Username == newUser.Username))
                return BadRequest("El usuario ya existe");

            users.Add(newUser);
            return Ok("Usuario registrado correctamente");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = users.FirstOrDefault(u =>
                u.Username == request.Username &&
                u.Password == request.Password);

            if (user == null)
                return Unauthorized("Credenciales incorrectas");

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