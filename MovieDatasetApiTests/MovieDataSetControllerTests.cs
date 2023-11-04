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
            {
                new Movie
                {
                    ReleaseDate = new DateTime(2001,2,2),
                    Title = "Finally Funny",
                    Overview = "After 8000 years, comedy has been invented.",
                    Popularity = 8.5F,
                    VoteCount = 41626,
                    VoteAverage = 7.7F,
                    OriginalLanguage = "en",
                    Genre = "Comedy",
                    PosterUrl = "www.google.co.uk",
                }
            },
        };

        [Fact]
        public void GetMovies_WithCount_CorrectNumber()
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

        [Fact]
        public void GetMovies_WithTitleQuery_CorrectNumber()
        {
            // Arrrange.
            var mockLogger = new Mock<ILogger<MovieDataSetController>>();
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(x => x.Movies).Returns(DummyMovies);

            var movieDataSetController = new MovieDataSetController(mockLogger.Object, mockDatabase.Object);

            // Act.
            var results = movieDataSetController.Get(10, 1, MovieOrderBy.None, "Birthday", null);

            // Assert.
            Assert.True(results.Count() == 1);
        }

        [Fact]
        public void GetMovies_WithInvalidTitleQuery_NoRecords()
        {
            // Arrrange.
            var mockLogger = new Mock<ILogger<MovieDataSetController>>();
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(x => x.Movies).Returns(DummyMovies);

            var movieDataSetController = new MovieDataSetController(mockLogger.Object, mockDatabase.Object);

            // Act.
            var results = movieDataSetController.Get(10, 1, MovieOrderBy.None, "Birth-day", null);

            // Assert.
            Assert.True(results.Count() == 0);
        }

        [Fact]
        public void GetMovies_SecondPageWithCount_CorrectNumber()
        {
            // Arrrange.
            var mockLogger = new Mock<ILogger<MovieDataSetController>>();
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(x => x.Movies).Returns(DummyMovies);

            var movieDataSetController = new MovieDataSetController(mockLogger.Object, mockDatabase.Object);

            // Act.
            var results = movieDataSetController.Get(2, 2, MovieOrderBy.None, null, null);

            // Assert.
            Assert.True(results.Count() == 1);
        }
    }
}