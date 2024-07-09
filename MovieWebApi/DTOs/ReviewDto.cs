namespace MovieWebApi.DTOs
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public string ReviewReviewer { get; set; }
        public string ReviewContent { get; set; }
        public int ReviewMovieId { get; set; }
    }
}
