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

        // GET: api/peliculas
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(peliculas);
        }

        // GET: api/peliculas/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pelicula = peliculas.FirstOrDefault(p => p.Id == id);

            if (pelicula == null)
                return NotFound("Película no encontrada");

            return Ok(pelicula);
        }

        // POST: api/peliculas
        [HttpPost]
        public IActionResult Post([FromBody] Pelicula nuevaPelicula)
        {
            nuevaPelicula.Id = peliculas.Max(p => p.Id) + 1;
            peliculas.Add(nuevaPelicula);

            return CreatedAtAction(nameof(GetById), new { id = nuevaPelicula.Id }, nuevaPelicula);
        }

        // PUT: api/peliculas/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pelicula peliculaActualizada)
        {
            var pelicula = peliculas.FirstOrDefault(p => p.Id == id);

            if (pelicula == null)
                return NotFound("Película no encontrada");

            pelicula.Titulo = peliculaActualizada.Titulo;
            pelicula.Director = peliculaActualizada.Director;
            pelicula.Anio = peliculaActualizada.Anio;
            pelicula.Genero = peliculaActualizada.Genero;
            pelicula.Calificacion = peliculaActualizada.Calificacion;

            return Ok(pelicula);
        }

        // DELETE: api/peliculas/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pelicula = peliculas.FirstOrDefault(p => p.Id == id);

            if (pelicula == null)
                return NotFound("Película no encontrada");

            peliculas.Remove(pelicula);
            return NoContent();
        }
    }
}
