using Microsoft.AspNetCore.Mvc;

namespace MovieDatasetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieDataSetController : ControllerBase
    {
        const int DEFAULT_COUNT = 10;
        const int DEFAULT_PAGE = 1;

        private readonly ILogger<MovieDataSetController> _logger;

        public MovieDataSetController(ILogger<MovieDataSetController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMovies")]
        public IEnumerable<Movie> Get
            ( [FromQuery] int count = DEFAULT_COUNT
            , [FromQuery] int page = DEFAULT_PAGE)
        {


            var results = Database.Movies
                .Skip(count * page -1)
                .Take(count)
                .ToArray();

            return results;
        }

        [HttpGet("GetByGenre/{genre}")]
        public IEnumerable<Movie> GetByGenre([FromRoute]string genre, [FromQuery] int count, [FromQuery] int page, [FromQuery] int perPage)
        {
            var results = Database.Movies.Take(55).ToArray();

            return results;
        }
    }
}