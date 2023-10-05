namespace MoviesApi.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById (byte id);
        Task<Genre> Add(Genre genre);
        Task<Genre> Update(Genre genre);
        Task<Genre> Delete(Genre genre);
        Task<bool> IsValidGenre(byte id);
    }
}
