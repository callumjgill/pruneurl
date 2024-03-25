namespace PruneUrl.Backend.Application.Interfaces.Providers
{
  /// <summary>
  /// Defines a provider for "short" url's. Implementations will generate and return a short url
  /// based upon the given sequence id. This is a "unique" positive integer value which is returned
  /// from the database.
  /// </summary>
  public interface IShortUrlProvider
  {
    /// <summary>
    /// Returns a "short" url given the sequence id.
    /// </summary>
    /// <param name="sequenceId"> The sequence id for the short url. </param>
    /// <returns> A shortened url. </returns>
    string GetShortUrl(int sequenceId);
  }
}
