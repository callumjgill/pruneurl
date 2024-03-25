using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.TestHelpers;

/// <summary>
/// A library of helper functions to do with <see cref="IEntity" />'s.
/// </summary>
public static class EntityTestHelper
{
  /// <summary>
  /// Creates a new <see cref="SequenceId" /> instance for testing.
  /// </summary>
  /// <param name="id"> Optional id for the sequence id. </param>
  /// <param name="value"> Optional value for the sequence id. </param>
  /// <returns> A new <see cref="SequenceId" /> instance. </returns>
  public static SequenceId CreateSequenceId(string? id = null, int? value = null)
  {
    string idToUse = id ?? string.Empty;
    int valueToUse = value ?? default;
    return new SequenceId(idToUse, valueToUse);
  }

  /// <summary>
  /// Creates a new <see cref="ShortUrl" /> instance for testing.
  /// </summary>
  /// <param name="id"> Optional id for the short url. </param>
  /// <param name="longUrl"> Optional long url for the short url. </param>
  /// <param name="url"> Optional url for the short url. </param>
  /// <param name="created"> Optional creation date for the short url. </param>
  /// <returns> A new <see cref="ShortUrl" /> instance. </returns>
  public static ShortUrl CreateShortUrl(
    string? id = null,
    string? longUrl = null,
    string? url = null,
    DateTime? created = null
  )
  {
    string idToUse = id ?? string.Empty;
    string longUrlToUse = longUrl ?? string.Empty;
    string urlToUse = url ?? string.Empty;
    DateTime createdToUse = created ?? default;
    return new ShortUrl(idToUse, longUrlToUse, urlToUse, createdToUse);
  }
}
