using Microsoft.AspNetCore.Mvc;

namespace MovieDatasetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieDataSetController : ControllerBase
    {
        const int DEFAULT_COUNT = 10;
        const int DEFAULT_PAGE = 1;


        private IDatabase Database => _database;

        private readonly ILogger<MovieDataSetController> _logger;
        private readonly IDatabase _database;

        public MovieDataSetController(ILogger<MovieDataSetController> logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet(Name = "GetMovies")]
        public IEnumerable<Movie> Get
            ( [FromQuery] int count = DEFAULT_COUNT
            , [FromQuery] int page = DEFAULT_PAGE
            , [FromQuery] MovieOrderBy orderBy = MovieOrderBy.None
            , [FromQuery] string? title = null
            , [FromQuery] string? genre = null)
        {
            _logger.Log(LogLevel.Information,$"Get with {count}, {page}, {orderBy}, {title}, {genre}");

            var movies = Database.Movies;

            movies = orderBy switch
            {
                MovieOrderBy.None => movies,
                MovieOrderBy.AlphabeticalAscending => movies.OrderBy(x => x.Title),
                MovieOrderBy.AlphabeticalDescending => movies.OrderByDescending(x => x.Title),
                MovieOrderBy.ReleaseDataAscending => movies.OrderBy(x => x.ReleaseDate),
                MovieOrderBy.ReleaseDateDescending => movies.OrderByDescending(x => x.ReleaseDate),
                _ => movies,
            };

            var results = movies
                .Where(x => genre == null || genre.Trim() == string.Empty || x.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase))
                .Where(x => title == null || title.Trim() == string.Empty || x.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Skip(count * (page -1))
                .Take(count)
                .ToArray();

            return results;
        }

        [HttpGet("GetByGenre/{genre}")]
        public IEnumerable<Movie> GetByGenre
            ( [FromRoute] string? genre = null
            , [FromQuery] int count = DEFAULT_COUNT
            , [FromQuery] int page = DEFAULT_PAGE
            , [FromQuery] MovieOrderBy orderBy = MovieOrderBy.None
            , [FromQuery] string? title = null)
        {
            _logger.Log(LogLevel.Information, $"Get with {count}, {page}, {orderBy}, {title}, {genre}");

            var genreMovies = Database.Movies.Where(x => genre == null 
                || genre.Trim() == string.Empty 
                || x.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));

            genreMovies = orderBy switch
            {
                MovieOrderBy.None => genreMovies,
                MovieOrderBy.AlphabeticalAscending => genreMovies.OrderBy(x => x.Title),
                MovieOrderBy.AlphabeticalDescending => genreMovies.OrderByDescending(x => x.Title),
                MovieOrderBy.ReleaseDataAscending => genreMovies.OrderBy(x => x.ReleaseDate),
                MovieOrderBy.ReleaseDateDescending => genreMovies.OrderByDescending(x => x.ReleaseDate),
                _ => genreMovies,
            };

            var results = genreMovies   
                .Where(x => title == null || title.Trim() == string.Empty || x.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Skip(count * (page - 1))
                .Take(count)
                .ToArray();

            return results;
        }
    }
}