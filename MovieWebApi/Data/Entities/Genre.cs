﻿namespace MovieWebApi.Data.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
