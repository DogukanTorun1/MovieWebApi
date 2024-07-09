using MovieWebApi.Data.Entities;

namespace MovieWebApi.DTOs
{
    public class MovieDto
    {

        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieReleaseDate { get; set; }
        public string MoviePoster { get; set; }
        public string MovieTrailer { get; set; }
        public List<string> Genres { get; set; }

    }
}
