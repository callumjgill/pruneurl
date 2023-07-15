using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Factories.Entities
{
  /// <summary>
  /// Defines a factory for creating <see cref="ShortUrl" /> instances.
  /// </summary>
  public interface IShortUrlFactory
  {
    #region Public Methods

    /// <summary>
    /// Creates a new <see cref="ShortUrl" /> instance given a <paramref name="longUrl" /> and
    /// optional <paramref name="shortUrl" />.
    /// </summary>
    /// <param name="longUrl"> The long url the <see cref="ShortUrl" /> instance will encapsulate. </param>
    /// <param name="shortUrl"> Optional short url the end user wishes to use. </param>
    /// <returns>
    /// A new <see cref="ShortUrl" /> instance. This will have a new unique ID and not reference an
    /// existing "short url".
    /// </returns>
    ShortUrl Create(string longUrl, string? shortUrl = null);

    #endregion Public Methods
  }
}