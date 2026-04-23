using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVibeAPI.Data;
using CineVibeAPI.Models;

namespace CineVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            try
            {
                var movies = await _context.Movies.ToListAsync();
                return Ok(movies); // Ensure HTTP 200 is returned
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/movie/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                {
                    return NotFound(new { message = "Movie not found" }); // Return a clear message
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/movie
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            try
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMovie), new { id = movie.movieid }, movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/movie/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.movieid)
            {
                return BadRequest(new { message = "Movie ID mismatch" });
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Movies.Any(e => e.movieid == id))
                {
                    return NotFound(new { message = "Movie not found" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/movie/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                {
                    return NotFound(new { message = "Movie not found" });
                }

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
    }
}
