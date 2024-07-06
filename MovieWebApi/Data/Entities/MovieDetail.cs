namespace MovieWebApi.Data.Entities
{
    public class MovieDetail
    {
        public int Id { get; set; }
        public string description { get; set; }
        public string director { get; set; }
        public float rating { get; set; }
        public int movieId { get; set; }

        //MovieDetail has one movie
        public Movie Movie { get; set; }
    }
}
