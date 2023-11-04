namespace MovieDatasetApi
{
    public interface IDatabase
    {
        IEnumerable<Movie> Movies { get; }
    }
}
