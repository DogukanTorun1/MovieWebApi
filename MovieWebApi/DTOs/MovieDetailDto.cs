using MovieWebApi.Data.Entities;

namespace MovieWebApi.DTOs
{
    public class MovieDetailDto
    {
        public int DetailId { get; set; }
        public string DetailDescription { get; set; }
        public string DetailDirector { get; set; }
        public float DetailRating { get; set; }
        public int DetailMovieId { get; set; }
    }
}
