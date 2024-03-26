using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.TestHelpers;

/// <summary>
/// A library of helper functions to do with <see cref="Entity" />'s.
/// </summary>
public static class EntityTestHelper
{
  /// <summary>
  /// Creates a new <see cref="ShortUrl" /> instance for testing.
  /// </summary>
  /// <param name="id"> Optional id for the short url. </param>
  /// <param name="longUrl"> Optional long url for the short url. </param>
  /// <param name="url"> Optional url for the short url. </param>
  /// <returns> A new <see cref="ShortUrl" /> instance. </returns>
  public static ShortUrl CreateShortUrl(int? id = null, string? longUrl = null, string? url = null)
  {
    int idToUse = id ?? 0;
    string longUrlToUse = longUrl ?? string.Empty;
    string urlToUse = url ?? string.Empty;
    return new ShortUrl
    {
      Id = idToUse,
      LongUrl = longUrlToUse,
      Url = urlToUse
    };
  }
}
