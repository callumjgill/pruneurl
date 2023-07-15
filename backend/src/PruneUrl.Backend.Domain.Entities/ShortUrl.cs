namespace PruneUrl.Backend.Domain.Entities
{
  /// <summary>
  /// An <see cref="IEntity" /> encapsulating a "short url". This is an entity which represents the
  /// main domain of the app, which is the final generated "short" url the end user can use. Each
  /// "short" url has an equivalent "long" url that will be used when redirecting. The entity also
  /// knows when it was created as this is used to determine whether the url is valid, i.e. not expired.
  /// </summary>
  /// <param name="Id"> The unique identifier for the short url. </param>
  /// <param name="LongUrl">
  /// The "long" url, which is the original url the shorter url will allow redirection to.
  /// </param>
  /// <param name="Url"> The generated "short" url that is used by the end user. </param>
  /// <param name="Created"> The date (and time) when this short url was created. </param>
  public sealed record ShortUrl(string Id, string LongUrl, string Url, DateTime Created) : IEntity
  {
  }
}