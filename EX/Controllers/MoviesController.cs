using Microsoft.AspNetCore.Mvc;
using EX.Models;

namespace EX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        // POST: api/Movies
        [HttpPost]
        public ActionResult<bool> Post([FromBody] Movie m)
        {
            bool success = Movie.Insert(m);
            if (!success)
                return BadRequest("Movie with same Id or Title already exists.");
            return Ok(true);
        }

        // GET: api/Movies
        // GET: api/Movies?title=Hobbit
        [HttpGet]
        public ActionResult<List<Movie>> Get([FromQuery] string? title)
        {
            if (!string.IsNullOrEmpty(title))
                return Ok(Movie.GetByTitle(title));

            return Ok(Movie.Read());
        }

        // GET: api/Movies/from/1995-12-17/until/2021-12-17
        [HttpGet("from/{start}/until/{end}")]
        public ActionResult<List<Movie>> GetByReleaseDate(DateTime start, DateTime end)
        {
            return Ok(Movie.GetByReleaseDate(start, end));
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            bool success = Movie.Delete(id);
            if (!success)
                return NotFound("Movie not found.");
            return Ok(true);
        }

    }
}
