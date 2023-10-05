namespace MoviesApi.Dtos
{
    public class MovieDto
    {
        [MaxLength(255)]
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoreLine { get; set; } = null!;
        public IFormFile? Poster { get; set; }
        public byte GenreId { get; set; }
    }
}
