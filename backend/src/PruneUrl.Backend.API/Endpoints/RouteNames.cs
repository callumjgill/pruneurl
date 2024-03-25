using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.API;

/// <summary>
/// Static class containing the names of the REST routes in the API.
/// </summary>
internal static class RouteNames
{
  /// <summary>
  /// The route for the <see cref="ShortUrl" /> POST REST endpoint.
  /// </summary>
  public static readonly string PostShortUrlRoute = nameof(PostShortUrlRoute);

  /// <summary>
  /// The route for the redirecting of short urls REST endpoint.
  /// </summary>
  public static readonly string RedirectRoute = nameof(RedirectRoute);
}
