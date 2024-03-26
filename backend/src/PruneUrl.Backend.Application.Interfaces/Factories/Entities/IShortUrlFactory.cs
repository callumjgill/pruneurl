using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces;

/// <summary>
/// Defines a factory for creating <see cref="ShortUrl" /> instances.
/// </summary>
public interface IShortUrlFactory
{
  /// <summary>
  /// Creates a new <see cref="ShortUrl" /> instance given a <paramref name="longUrl" /> and a
  /// <paramref name="sequenceId" />.
  /// </summary>
  /// <param name="longUrl"> The long url the <see cref="ShortUrl" /> instance will encapsulate. </param>
  /// <param name="sequenceId">
  /// The sequence id used to create the short url the end user wishes to use.
  /// </param>
  /// <returns>
  /// A new <see cref="ShortUrl" /> instance. This will have a new unique ID and not reference an
  /// existing "short url".
  /// </returns>
  ShortUrl Create(string longUrl, int sequenceId);

  /// <summary>
  /// Creates a new <see cref="ShortUrl"/> instance which has default values set for its properties. This is useful
  /// if a placeholder <see cref="ShortUrl"/> is needed to be created, e.g. in the database.
  /// </summary>
  /// <returns>
  /// A new <see cref="ShortUrl" /> instance.
  /// </returns>
  ShortUrl Create();
}
