using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Migrations;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresServices;
        // constructor
        public GenresController(IGenresService genresServices) => this._genresServices = genresServices;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresServices.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GenreDto genreDto)
        {
            var genre = new Genre { Name = genreDto.Name };
            await _genresServices.Add(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenresAsync(byte id, [FromBody] GenreDto genreDto)
        {
            var genre = await _genresServices.GetById(id);
            if (genre is null)
                return NotFound($"No genre was found with ID: {id}");
            genre.Name = genreDto.Name;

            await _genresServices.Update(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreAsync(byte id)
        {
            var genre = await _genresServices.GetById(id);
            if (genre is null)
                return NotFound($"No genre was found with ID: {id}");

            _genresServices?.Delete(genre);

            return Ok(genre);
        }

    }
}
