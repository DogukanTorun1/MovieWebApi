using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.Data.Contexts;
using MovieWebApi.Data.Entities;
using MovieWebApi.DTOs;

namespace MovieWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ProjectContext _db;

        public GenreController(ProjectContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            if (ModelState.IsValid)
            {
                List<GenreDto> genres = new List<GenreDto>();
                List<Genre> genreList = _db.Genres.ToList();
                foreach (Genre genre in genreList)
                {
                    genres.Add(new GenreDto
                    {
                        GenreId = genre.Id,
                        GenreName = genre.Name
                    });
                }
                return Ok(genres);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetGenre(int id)
        {
            if (ModelState.IsValid)
            {
                Genre genre = _db.Genres.Find(id);
                if (genre == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new GenreDto
                    {
                        GenreId = genre.Id,
                        GenreName = genre.Name
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] GenreDto genreDto)
        {
            if (ModelState.IsValid)
            {
                Genre genre = new Genre
                {
                    Name = genreDto.GenreName
                };
                _db.Genres.Add(genre);
                _db.SaveChanges();
                return Ok(genre);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            if (ModelState.IsValid)
            {
                Genre genre = _db.Genres.Find(id);
                if (genre == null)
                {
                    return NotFound();
                }
                _db.Genres.Remove(genre);
                _db.SaveChanges();
                return Ok("Genre Deleted!");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
