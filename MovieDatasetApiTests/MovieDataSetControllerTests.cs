using Microsoft.Extensions.Logging;
using Moq;
using MovieDatasetApi;
using MovieDatasetApi.Controllers;

namespace MovieDatasetApiTests
{
    public class MovieDataSetControllerTests
    {
        private List<Movie> DummyMovies = new List<Movie>
        {
            {
                new Movie
                {
                    ReleaseDate = new DateTime(1993,7,16),
                    Title = "Birthday Surprise",
                    Overview = "A thing happens.",
                    Popularity = 9.5F,
                    VoteCount = 1051,
                    VoteAverage = 9.2F,
                    OriginalLanguage = "en",
                    Genre = "Action",
                    PosterUrl = "www.google.co.uk",
                }
            },
            {
                new Movie
                {
                    ReleaseDate = new DateTime(2001,2,2),
                    Title = "Whoops!",
                    Overview = "A thing happens, badly.",
                    Popularity = 1.5F,
                    VoteCount = 2551,
                    VoteAverage = 1.2F,
                    OriginalLanguage = "en",
                    Genre = "Comedy",
                    PosterUrl = "www.google.co.uk",
                }
            },
        };

        [Fact]
        public void GetMovies_WithCount_CorrectNumberShouldBeReturned()
        {
            // Arrrange.
            var mockLogger = new Mock<ILogger<MovieDataSetController>>();
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(x => x.Movies).Returns(DummyMovies);

            var movieDataSetController = new MovieDataSetController(mockLogger.Object, mockDatabase.Object);

            // Act.
            var results = movieDataSetController.Get(1, 1, MovieOrderBy.None, null, null);

            // Assert.
            Assert.True(results.Count() == 1);
        }
    }
}