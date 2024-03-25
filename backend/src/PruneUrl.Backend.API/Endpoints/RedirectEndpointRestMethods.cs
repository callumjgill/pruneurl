using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruneUrl.Backend.Application.Exceptions;
using PruneUrl.Backend.Application.Queries;

namespace PruneUrl.Backend.API;

/// <summary>
/// Static class containing the methods for redirecting REST endpoint.
/// </summary>
internal static class RedirectEndpointRestMethods
{
  /// <summary>
  /// The GET REST Endpoint which redirects to the corresponding long url.
  /// </summary>
  /// <param name="shortUrl"> The short url relative path. </param>
  /// <param name="mediator">
  /// The <see cref="IMediator" /> interface used to send requests to the underlying database.
  /// </param>
  /// <returns>
  /// A task representing the asynchronous operation of retriving the long url for redirection
  /// from the given short url.
  /// </returns>
  public static Task<IResult> GetShortUrl(
    [FromRoute] string shortUrl,
    [FromServices] IMediator mediator
  )
  {
    return EndpointRestMethodsUtilities.HandleErrors(async () =>
    {
      try
      {
        var query = new GetShortUrlQuery(shortUrl);
        GetShortUrlQueryResponse response = await mediator.Send(query);
        return Results.Redirect(response.ShortUrl.LongUrl);
      }
      catch (EntityNotFoundException)
      {
        return Results.NotFound();
      }
    });
  }
}
