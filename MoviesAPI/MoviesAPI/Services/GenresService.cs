using MoviesApi.Dtos;

namespace MoviesApi.Services
{
    public class GenresService : IGenresService
    {
        private readonly AppDbContext _context;
        public GenresService(AppDbContext context) => this._context = context; 

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();            
        }

        public async Task<Genre> GetById(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return genre;
        }
        public async Task<Genre> Update(Genre genre)
        {
            _context.Update(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task<Genre> Delete(Genre genre)
        {
            _context.Remove(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public Task<bool> IsValidGenre(byte id)
        {
            return _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
