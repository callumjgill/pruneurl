namespace PruneUrl.Backend.Application.Interfaces.Providers;

/// <summary>
/// Defines a provider for "sequence id"'s from a short url, intended as a mechanism to convert
/// back to a sequence id from a short url. This is a "unique" positive integer value.
/// </summary>
public interface ISequenceIdProvider
{
  /// <summary>
  /// Returns a "sequence id" given a "short" url.
  /// </summary>
  /// <param name="shortUrl"> The short url to get the sequence id for. </param>
  /// <returns> A sequence id. </returns>
  int GetSequenceId(string shortUrl);
}
