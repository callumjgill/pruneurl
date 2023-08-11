﻿using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.App.Endpoints
{
  /// <summary>
  /// Static class containing the names of the REST routes in the API.
  /// </summary>
  internal static class RouteNames
  {
    #region Public Fields

    /// <summary>
    /// The route for the <see cref="ShortUrl" /> POST REST endpoint.
    /// </summary>
    public static readonly string PostShortUrlRoute = nameof(PostShortUrlRoute);

    /// <summary>
    /// The route for the redirecting of short urls REST endpoint.
    /// </summary>
    public static readonly string RedirectRoute = nameof(RedirectRoute);

    #endregion Public Fields
  }
}