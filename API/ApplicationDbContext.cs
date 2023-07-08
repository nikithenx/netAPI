using API.Configurations;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieHall> MovieHalls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new MovieConfiguration().Configure(modelBuilder.Entity<Movie>());
            new MovieHallConfiguration().Configure(modelBuilder.Entity<MovieHall>());
        }
    }
}