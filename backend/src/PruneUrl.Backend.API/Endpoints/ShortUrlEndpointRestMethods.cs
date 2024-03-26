using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruneUrl.Backend.Application.Commands;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.API;

/// <summary>
/// Static class containing the methods for the <see cref="ShortUrl" /> entity resource REST endpoint.
/// </summary>
internal static class ShortUrlEndpointRestMethods
{
  /// <summary>
  /// The POST REST Endpoint for creating a new <see cref="ShortUrl" /> entity.
  /// </summary>
  /// <param name="requestBody">
  /// The request body containing data that will be used to create a <see cref="ShortUrl" />
  /// entity from.
  /// </param>
  /// <param name="mediator">
  /// The <see cref="IMediator" /> interface used to send requests to the underlying database.
  /// </param>
  /// <returns>
  /// A task representing the asynchronous operation of creating a new <see cref="ShortUrl" /> entity.
  /// </returns>
  public static Task<IResult> PostShortUrl(
    [FromBody] ShortUrlPostRequest requestBody,
    [FromServices] IMediator mediator
  )
  {
    return EndpointRestMethodsUtilities.HandleErrors(async () =>
    {
      CreateShortUrlCommand command = new(requestBody.LongUrl);
      CreateShortUrlCommandResponse response = await mediator.Send(command);
      return Results.CreatedAtRoute(RouteNames.RedirectRoute, new { response.ShortUrl });
    });
  }
}
