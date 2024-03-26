namespace PruneUrl.Backend.Domain.Entities;

/// <summary>
/// An <see cref="Entity" /> encapsulating a "short url". This is an entity which represents the
/// main domain of the app, which is the final generated "short" url the end user can use. Each
/// "short" url has an equivalent "long" url that will be used when redirecting.
/// </summary>
public sealed class ShortUrl : Entity
{
  /// <summary>
  /// The "long" url, which is the original url the shorter url will allow redirection to.
  /// </summary>
  public required string LongUrl { get; set; }

  /// <summary>
  /// The generated "short" url that is used by the end user.
  /// </summary>
  public required string Url { get; set; }
}
