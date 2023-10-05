namespace MoviesApi.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; } = null!;
        public byte[] Poster { get; set; } = null!;
        public byte GenreId { get; set; }
        public string GenreName { get; set; } = null!;
    }
}
