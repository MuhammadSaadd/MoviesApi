using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;
using System.Runtime.InteropServices;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;

        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(IMoviesService moviesService, IGenresService genresService, IMapper mapper)
        {
            this._moviesService = moviesService;
            this._genresService = genresService;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _moviesService.GetAllMovies();
            // TODO: map movies to DTO
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            // primary key
            var movie = await _moviesService.GetMovieById(id);
            
            if (movie is null)
                return NotFound("This movie not fount");

            var dto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetMoviesByGenreIdAsync(byte genreId)
        {
            // TODO: map movies to DTO
            var movies = await _moviesService.GetAllMovies(genreId);
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync([FromForm] MovieDto movieDto)
        {
            if (movieDto.Poster == null)
                return BadRequest("Poster is required!");   

            if (!_allowedExtenstions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed");
            
            if(movieDto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidGenre = await _genresService.IsValidGenre(movieDto.GenreId);
            if(!isValidGenre)
                return BadRequest("Invalid genre ID!");

            using var dataStream = new MemoryStream();
            await movieDto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(movieDto);
            movie.Poster = dataStream.ToArray();

            await _moviesService.AddMovie(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(int id,[FromForm] MovieDto movieDto)
        {
            var movie = await _moviesService.GetMovieById(id);

            if (movie is null)
                return NotFound($"No movie was found with ID: {id}");

            var isValidGenre = await _genresService.IsValidGenre(movieDto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            if(movieDto.Poster is not null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed");

                if (movieDto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();
                await movieDto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }

            movie.Title = movieDto.Title;
            movie.Year = movieDto.Year;
            movie.GenreId = movieDto.GenreId;
            movie.StoreLine = movieDto.StoreLine;
            movie.Rate = movieDto.Rate;

            _moviesService.UpdateMovie(movie); 
            
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _moviesService.GetMovieById(id);

            if (movie is null)
                return NotFound($"No movie was found with ID: {id}");

            _moviesService.DeleteMovie(movie);

            return Ok(movie);
        }
    }
}
