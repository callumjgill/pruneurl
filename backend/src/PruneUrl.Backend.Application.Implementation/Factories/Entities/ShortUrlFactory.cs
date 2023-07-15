using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Implementation.Factories.Entities
{
  /// <summary>
  /// A factory for creating <see cref="ShortUrl" /> instances.
  /// </summary>
  public sealed class ShortUrlFactory : IShortUrlFactory
  {
    #region Private Fields

    private readonly IDateTimeProvider dateTimeProvider;
    private readonly IEntityIdProvider entityIdProvider;
    private readonly IShortUrlProvider shortUrlProvider;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="ShortUrlFactory" /> class.
    /// </summary>
    /// <param name="dateTimeProvider"> The provider for <see cref="DateTime" /> values. </param>
    /// <param name="entityIdProvider">
    /// The provider for unique identifiers for <see cref="ShortUrl" />'s.
    /// </param>
    /// <param name="shortUrlProvider"> The provider for shortend urls. </param>
    public ShortUrlFactory(IDateTimeProvider dateTimeProvider,
                           IEntityIdProvider entityIdProvider,
                           IShortUrlProvider shortUrlProvider)
    {
      this.dateTimeProvider = dateTimeProvider;
      this.entityIdProvider = entityIdProvider;
      this.shortUrlProvider = shortUrlProvider;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IShortUrlFactory.Create(string, string?)" />
    public ShortUrl Create(string longUrl, string? shortUrl = null)
    {
      DateTime created = dateTimeProvider.GetNow();
      string id = entityIdProvider.NewId();
      string shortUrlToUse = shortUrl ?? shortUrlProvider.GetShortUrl(longUrl);
      return new ShortUrl(id, longUrl, shortUrlToUse, created);
    }

    #endregion Public Methods
  }
}