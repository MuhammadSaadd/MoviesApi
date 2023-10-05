namespace MoviesApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Movie> Movies => Set<Movie>();
    }
}
