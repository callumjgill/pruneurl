namespace PruneUrl.Backend.Application.Interfaces.Providers
{
  /// <summary>
  /// Defines a provider for "short" url's. Implementations will generate and return a short url
  /// based upon the given long url.
  /// </summary>
  public interface IShortUrlProvider
  {
    #region Public Methods

    /// <summary>
    /// Returns a "short" url given a "long" url.
    /// </summary>
    /// <param name="longUrl"> The long url to shorten. </param>
    /// <returns> A shortened url. </returns>
    string GetShortUrl(string longUrl);

    #endregion Public Methods
  }
}