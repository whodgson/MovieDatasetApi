namespace MovieDatasetApi
{
    public class Movie
    {
        public DateTime ReleaseDate { get; set; }
        public required string Title { get; set; }
        public required string Overview { get; set; }
        public float Popularity { get; set; }
        public int VoteCount { get; set; }
        public float VoteAverage { get; set; }
        public required string OriginalLanguage { get; set; }
        public required string Genre { get; set; }
        public required string PosterUrl { get; set; }

    }
}
