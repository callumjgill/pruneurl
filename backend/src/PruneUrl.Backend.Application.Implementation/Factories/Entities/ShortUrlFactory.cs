using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Implementation;

/// <summary>
/// A factory for creating <see cref="ShortUrl" /> instances.
/// </summary>
/// <param name="dateTimeProvider"> The provider for <see cref="DateTime" /> values. </param>
/// <param name="shortUrlProvider"> The provider for shortend urls. </param>
public sealed class ShortUrlFactory(
  IDateTimeProvider dateTimeProvider,
  IShortUrlProvider shortUrlProvider
) : IShortUrlFactory
{
  private readonly IDateTimeProvider dateTimeProvider = dateTimeProvider;
  private readonly IShortUrlProvider shortUrlProvider = shortUrlProvider;

  /// <inheritdoc cref="IShortUrlFactory.Create(string, int)" />
  public ShortUrl Create(string longUrl, int sequenceId)
  {
    string shortUrlToUse = shortUrlProvider.GetShortUrl(sequenceId);
    DateTime created = dateTimeProvider.GetNow();
    return new ShortUrl(sequenceId.ToString(), longUrl, shortUrlToUse, created);
  }
}
