namespace MoviesApi.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) => this._context = context; 

        public async Task<IEnumerable<Movie>> GetAllMovies(byte genreId = 0)
        {
            return await _context.Movies
                .Where(m => m.GenreId == genreId || genreId == 0)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .SingleOrDefaultAsync(m => m.Id == id);
            
            return movie;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public Movie UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();

            return movie;
        }
        
        public Movie DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();

            return movie;
        }
    }
}
