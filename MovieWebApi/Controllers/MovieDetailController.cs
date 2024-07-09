using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.Data.Contexts;
using MovieWebApi.Data.Entities;
using MovieWebApi.DTOs;

namespace MovieWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieDetailController : ControllerBase
    {
        private readonly ProjectContext _db;

        public MovieDetailController(ProjectContext db)
        {
            _db = db;
        }

        [HttpGet("movieId")]
        public IActionResult GetMovieDetail(int movieId)
        {
            if(ModelState.IsValid)
            {
                MovieDetail movieDetail = _db.MovieDetails.FirstOrDefault(x => x.movieId == movieId);
                if (movieDetail == null)
                {
                    return NotFound();
                }
                MovieDetailDto movieDetailDto = new MovieDetailDto
                {
                    DetailId = movieDetail.Id,
                    DetailDescription = movieDetail.description,
                    DetailDirector = movieDetail.director,
                    DetailRating = movieDetail.rating,
                    DetailMovieId = movieDetail.movieId

                };
                return Ok(movieDetailDto);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public IActionResult AddMovieDetail(MovieDetailDto movieDetailDto)
        {
            if (ModelState.IsValid)
            {
                MovieDetail movieDetail = new MovieDetail
                {
                    description = movieDetailDto.DetailDescription,
                    director = movieDetailDto.DetailDirector,
                    rating = movieDetailDto.DetailRating,
                    movieId = movieDetailDto.DetailMovieId
                };
                _db.MovieDetails.Add(movieDetail);
                _db.SaveChanges();
                return Ok(movieDetail);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("DetailMovieId")]
        public IActionResult UpdateMovieDetail(int DetailMovieId, [FromBody] MovieDetailDto movieDetailDto)
        {
            if (ModelState.IsValid)
            {
                MovieDetail movieDetail = _db.MovieDetails.FirstOrDefault(x => x.movieId == DetailMovieId);
                if (movieDetail == null)
                {
                    return NotFound();
                }
                movieDetail.description = movieDetailDto.DetailDescription;
                movieDetail.director = movieDetailDto.DetailDirector;
                movieDetail.rating = movieDetailDto.DetailRating;
                movieDetail.movieId = movieDetailDto.DetailMovieId;
                _db.MovieDetails.Update(movieDetail);
                _db.SaveChanges();
                return Ok(movieDetailDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("DetailMovieId")]
        public IActionResult DeleteMovieDetail(int DetailMovieId)
        {
            if (ModelState.IsValid)
            {
                MovieDetail movieDetail = _db.MovieDetails.FirstOrDefault(x => x.movieId == DetailMovieId);
                if (movieDetail == null)
                {
                    return NotFound();
                }
                _db.MovieDetails.Remove(movieDetail);
                _db.SaveChanges();
                return Ok("Movie Detail Deleted");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
