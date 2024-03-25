using Google.Cloud.Firestore;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs
{
  /// <summary>
  /// An <see cref="FirestoreEntityDTO" /> encapsulating a "short url". This is an entity which
  /// represents the main domain of the app, which is the final generated "short" url the end user
  /// can use. Each "short" url has an equivalent "long" url that will be used when redirecting. The
  /// entity also knows when it was created as this is used to determine whether the url is valid,
  /// i.e. not expired.
  /// </summary>
  [FirestoreData]
  internal sealed class ShortUrlDTO : FirestoreEntityDTO
  {
    /// <summary>
    /// The date (and time) when this short url was created.
    /// </summary>
    [FirestoreDocumentCreateTimestamp]
    public DateTime? Created { get; set; }

    /// <summary>
    /// The "long" url, which is the original url the shorter url will allow redirection to.
    /// </summary>
    [FirestoreProperty]
    public string? LongUrl { get; set; }

    /// <summary>
    /// The generated "short" url that is used by the end user.
    /// </summary>
    [FirestoreProperty]
    public string? Url { get; set; }
  }
}
