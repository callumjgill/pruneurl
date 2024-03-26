using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Implementation;

/// <summary>
/// A factory for creating <see cref="ShortUrl" /> instances.
/// </summary>
/// <param name="shortUrlProvider"> The provider for shortend urls. </param>
public sealed class ShortUrlFactory(IShortUrlProvider shortUrlProvider) : IShortUrlFactory
{
  private readonly IShortUrlProvider shortUrlProvider = shortUrlProvider;

  /// <inheritdoc cref="IShortUrlFactory.Create(string, int)" />
  public ShortUrl Create(string longUrl, int sequenceId)
  {
    string url = shortUrlProvider.GetShortUrl(sequenceId);
    return new ShortUrl
    {
      Id = sequenceId,
      LongUrl = longUrl,
      Url = url
    };
  }

  /// <inheritdoc cref="IShortUrlFactory.Create" />
  public ShortUrl Create()
  {
    return new ShortUrl
    {
      Id = 0,
      LongUrl = string.Empty,
      Url = string.Empty
    };
  }
}
