using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.Data.Contexts;
using MovieWebApi.Data.Entities;
using MovieWebApi.DTOs;

namespace MovieWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ProjectContext _db;

        public MovieController(ProjectContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            if (ModelState.IsValid)
            {
                List<MovieDto> movies = new List<MovieDto>();
                List<Movie> movieList = _db.Movies.ToList();
                foreach (Movie movie in movieList)
                {
                    List<Genre> genres = _db.Genres.Where(g => g.Movies.Contains(movie)).ToList();
                    List<string> genreNames = genres.Select(g => g.Name).ToList();

                    movies.Add(new MovieDto
                    {
                        MovieId = movie.Id,
                        MovieTitle = movie.Title,
                        MovieReleaseDate = movie.ReleaseDate,
                        MoviePoster = movie.Poster,
                        MovieTrailer = movie.Trailer,
                        Genres = genreNames
                    });
                }
                return Ok(movies);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            if (ModelState.IsValid)
            {
                Movie movie = _db.Movies.Find(id);
                List<Genre> genres = _db.Genres.Where(g => g.Movies.Contains(movie)).ToList();
                List<string> genreNames = genres.Select(g => g.Name).ToList();
                if (movie == null)
                {
                    return NotFound();
                }
                MovieDto movieDto = new MovieDto
                {
                    MovieId = movie.Id,
                    MovieTitle = movie.Title,
                    MovieReleaseDate = movie.ReleaseDate,
                    MoviePoster = movie.Poster,
                    MovieTrailer = movie.Trailer,
                    Genres = genreNames
                };
                return Ok(movieDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                Movie movie = new Movie
                {
                    Title = movieDto.MovieTitle,
                    ReleaseDate = movieDto.MovieReleaseDate,
                    Poster = movieDto.MoviePoster,
                    Trailer = movieDto.MovieTrailer

                };
                _db.Movies.Add(movie);
                _db.SaveChanges();
                return Ok(movie);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                Movie movie = _db.Movies.Find(id);
                if (movie == null)
                {
                    return NotFound();
                }
                movie.Title = movieDto.MovieTitle;
                movie.ReleaseDate = movieDto.MovieReleaseDate;
                movie.Poster = movieDto.MoviePoster;
                movie.Trailer = movieDto.MovieTrailer;
                _db.Movies.Update(movie);
                _db.SaveChanges();
                return Ok(movie);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            if (ModelState.IsValid)
            {
                Movie movie = _db.Movies.Find(id);
                if (movie == null)
                {
                    return NotFound();
                }
                _db.Movies.Remove(movie);
                _db.SaveChanges();
                return Ok("Movie Deleted!");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("{movieId}/genres")]
        public IActionResult AddGenreToMovie(int movieId, [FromBody] GenreDto genreDto)
        {
            if (ModelState.IsValid)
            {
                Movie movie = _db.Movies.Find(movieId);
                if (movie == null)
                {
                    return NotFound();
                }
                Genre genre = new Genre
                {
                    Name = genreDto.GenreName
                };
                movie.Genres.Add(genre);
                _db.SaveChanges();
                return Ok(genre);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
