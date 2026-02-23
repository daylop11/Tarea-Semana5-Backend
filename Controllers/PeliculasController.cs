using AWS5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AWS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeliculasController(AppDbContext context)
        {
            _context = context;
        }

        private bool ValidarToken()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            return authHeader == "Bearer token_simple_123";
        }

        //  GET: api/peliculas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!ValidarToken())
                return Unauthorized("Sesión inválida");

            var peliculas = await _context.Peliculas.ToListAsync();
            return Ok(peliculas);
        }

        //  GET: api/peliculas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ValidarToken())
                return Unauthorized("Sesión inválida");

            var pelicula = await _context.Peliculas.FindAsync(id);

            if (pelicula == null)
                return NotFound("Película no encontrada");

            return Ok(pelicula);
        }

        //  POST: api/peliculas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pelicula pelicula)
        {
            if (!ValidarToken())
                return Unauthorized("Sesión inválida");

            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pelicula.Id }, pelicula);
        }

        //  PUT: api/peliculas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pelicula peliculaActualizada)
        {
            if (!ValidarToken())
                return Unauthorized("Sesión inválida");

            if (id != peliculaActualizada.Id)
                return BadRequest("El ID no coincide");

            var pelicula = await _context.Peliculas.FindAsync(id);

            if (pelicula == null)
                return NotFound("Película no encontrada");

            pelicula.Titulo = peliculaActualizada.Titulo;
            pelicula.Director = peliculaActualizada.Director;
            pelicula.Anio = peliculaActualizada.Anio;
            pelicula.Genero = peliculaActualizada.Genero;
            pelicula.Calificacion = peliculaActualizada.Calificacion;

            await _context.SaveChangesAsync();

            return Ok(pelicula);
        }

        //  DELETE: api/peliculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ValidarToken())
                return Unauthorized("Sesión inválida");

            var pelicula = await _context.Peliculas.FindAsync(id);

            if (pelicula == null)
                return NotFound("Película no encontrada");

            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}