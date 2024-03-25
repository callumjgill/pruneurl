using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruneUrl.Backend.App.Endpoints.Models;
using PruneUrl.Backend.Application.Commands.CreateShortUrl;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.App.Endpoints
{
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
      [FromServices] IMediator mediator,
      [FromServices] IShortUrlProvider shortUrlProvider
    )
    {
      return EndpointRestMethodsUtilities.HandleErrors(async () =>
      {
        var sequenceIdRequest = new GetAndBumpSequenceIdRequest();
        GetAndBumpSequenceIdResponse sequenceIdResponse = await mediator.Send(sequenceIdRequest);
        int sequenceId = sequenceIdResponse.SequenceId.Value;
        var command = new CreateShortUrlCommand(requestBody.LongUrl, sequenceId);
        await mediator.Send(command);
        string shortUrl = shortUrlProvider.GetShortUrl(sequenceId);
        return Results.CreatedAtRoute(RouteNames.RedirectRoute, new { shortUrl });
      });
    }
  }
}
