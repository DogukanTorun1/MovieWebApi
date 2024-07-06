namespace MovieWebApi.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Content { get; set; }
        public int MovieId { get; set; }

        //Review has one movie
        public Movie Movie { get; set; }


    }
}
