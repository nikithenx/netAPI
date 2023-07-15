using Microsoft.EntityFrameworkCore;

namespace MinAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}