using System.Security.Cryptography.X509Certificates;

namespace MovieWebApi.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string Poster { get; set; }
        public string Trailer { get; set; }


        //Movie has one movie detail
        public MovieDetail MovieDetail { get; set; }
        //Movie has many reviews
        public ICollection<Review> Reviews { get; set; }
        //Movie has many genres
        public ICollection<Genre> Genres { get; set; }
    }
}
