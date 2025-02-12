using FilmRatingAPI.Models;
using FilmRatingAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmRatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly FilmRatingContext _context;

        public FilmsController(FilmRatingContext context)
        {
            _context = context;
        }

        // GET: api/Films (Search films)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms(string search = "")
        {
            var films = await _context.Films
                .Where(f => string.IsNullOrEmpty(search) || f.Name.Contains(search) || f.Director.Contains(search))
                .ToListAsync();
            return Ok(films);
        }

        // GET: api/Films/5 (Get film by ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        // POST: api/Films (Add film - Admin only)
        [HttpPost]
        public async Task<ActionResult<Film>> AddFilm(Film film)
        {
            _context.Films.Add(film);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFilm), new { id = film.Id }, film);
        }

        // PUT: api/Films/5 (Edit film - Admin only)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilm(int id, Film film)
        {
            if (id != film.Id)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Films/5 (Delete film - Admin only)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Films/5/Rate (Rate film - User only)
        [HttpPost("{id}/Rate")]
        public async Task<IActionResult> RateFilm(int id, [FromBody] double rating)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            film.Rating = (film.Rating + rating) / 2; // Average rating
            await _context.SaveChangesAsync();
            return Ok(film);
        }
    }
}