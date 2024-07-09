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
    }
}
