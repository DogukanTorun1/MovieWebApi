using Microsoft.EntityFrameworkCore;
using MovieWebApi.Data.Entities;

namespace MovieWebApi.Data.Contexts
{
    public class ProjectContext : DbContext
    {
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieDetail> MovieDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MovieDetail)
                .WithOne(md => md.Movie)
                .HasForeignKey<MovieDetail>(md => md.movieId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany(g => g.Movies);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=MovieApiDb;Trusted_Connection=True;TrustServerCertificate=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
