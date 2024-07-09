using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.Data.Contexts;
using MovieWebApi.Data.Entities;
using MovieWebApi.DTOs;

namespace MovieWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ProjectContext _db;

        public ReviewController(ProjectContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            if (ModelState.IsValid)
            {
                List<ReviewDto> reviews = new List<ReviewDto>();
                List<Review> reviewList = _db.Reviews.ToList();
                foreach (Review review in reviewList)
                {
                    reviews.Add(new ReviewDto
                    {
                        ReviewId = review.Id,
                        ReviewReviewer = review.Reviewer,
                        ReviewContent = review.Content,
                        ReviewMovieId = review.MovieId
                    });
                }
                return Ok(reviews);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpGet("ReviewMovieId")]
        public IActionResult GetReviewsById(int ReviewMovieId)
        {
            if (ModelState.IsValid)
            {
                List<ReviewDto> reviews = new List<ReviewDto>();
                List<Review> reviewList = _db.Reviews.Where(x => x.MovieId == ReviewMovieId).ToList();
                foreach (Review review in reviewList)
                {
                    reviews.Add(new ReviewDto
                    {
                        ReviewId = review.Id,
                        ReviewReviewer = review.Reviewer,
                        ReviewContent = review.Content,
                        ReviewMovieId = review.MovieId
                    });
                }
                return Ok(reviews);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPost]
        public IActionResult AddReview(ReviewDto reviewDto)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review
                {
                    Reviewer = reviewDto.ReviewReviewer,
                    Content = reviewDto.ReviewContent,
                    MovieId = reviewDto.ReviewMovieId
                };
                _db.Reviews.Add(review);
                _db.SaveChanges();
                return Ok(review);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("ReviewId")]
        public IActionResult UpdateReview(int ReviewId, [FromBody] ReviewDto reviewDto)
        {
            if (ModelState.IsValid)
            {
                Review review = _db.Reviews.FirstOrDefault(x => x.Id == ReviewId);
                if (review == null)
                {
                    return NotFound();
                }
                review.Reviewer = reviewDto.ReviewReviewer;
                review.Content = reviewDto.ReviewContent;
                review.MovieId = reviewDto.ReviewMovieId;
                _db.SaveChanges();
                return Ok(review);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("ReviewId")]
        public IActionResult DeleteReview(int ReviewId)
        {
            if (ModelState.IsValid)
            {
                Review review = _db.Reviews.FirstOrDefault(x => x.Id == ReviewId);
                if (review == null)
                {
                    return NotFound();
                }
                _db.Reviews.Remove(review);
                _db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
