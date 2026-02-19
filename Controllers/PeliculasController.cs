using AWS5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AWS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private static List<Pelicula> peliculas = new List<Pelicula>
        {
            new Pelicula { Id = 1, Titulo = "Inception", Director = "Christopher Nolan", Anio = 2010, Genero = "Ciencia Ficción", Calificacion = 9.0 },
            new Pelicula { Id = 2, Titulo = "Interstellar", Director = "Christopher Nolan", Anio = 2014, Genero = "Ciencia Ficción", Calificacion = 8.6 }
        };

        private bool ValidarToken()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            return authHeader == "Bearer token_simple_123";
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (!ValidarToken())
                return Unauthorized("Sesión inválida");

            return Ok(peliculas);
        }
    }
}
