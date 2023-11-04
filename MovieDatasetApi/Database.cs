using System.Text.RegularExpressions;

namespace MovieDatasetApi
{
    public class Database : IDatabase
    {
        private IEnumerable<Movie> movies;
        public IEnumerable<Movie> Movies
        {
            get
            {
                if (movies == null)
                    movies = LoadMoviesFromCsv();

                return movies;
            }
        }

        private IEnumerable<Movie> LoadMoviesFromCsv()
        {
            var rawValues = File.ReadAllLines(@"mymoviedb.csv")
                .Skip(1)
                .ToList();

            var importedMovies = new List<Movie>();

            foreach(var rawValue in rawValues)
            {
                // Split the column read from the CSV, it's important
                // to only split commas that lie outside of double-quote
                // blocks. If one of the records is malformed, it will be skipped.

                string[] columnValues = Regex.Split(rawValue, "[,]{1}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                try
                {
                    var movie = new Movie
                    {
                        ReleaseDate = Convert.ToDateTime(columnValues[0]),
                        Title = columnValues[1].ToString(),
                        Overview = columnValues[2].ToString(),
                        Popularity = Convert.ToSingle(columnValues[3]),
                        VoteCount = Convert.ToInt32(columnValues[4]),
                        VoteAverage = Convert.ToSingle(columnValues[5]),
                        OriginalLanguage = columnValues[6].ToString(),
                        Genre = columnValues[7].ToString(),
                        PosterUrl = columnValues[8].ToString(),
                    };

                    importedMovies.Add( movie );
                    Console.WriteLine($"The movie '{movie.Title}' was imported from the CSV. ");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"There was an issue importing a movie record from the CSV, skipping.\n{ex.Message}");
                    continue;
                }
                

            }

            return importedMovies;
        }
    }
}
