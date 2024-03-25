using MediatR;
using Microsoft.Extensions.Options;
using PruneUrl.Backend.Application.Commands;
using PruneUrl.Backend.Application.Configuration;
using PruneUrl.Backend.Application.Queries;

namespace PruneUrl.Backend.API;

/// <summary>
/// Extensions for the <see cref="IHost" /> interface is
/// </summary>
public static class HostExtensions
{
  /// <summary>
  /// Ensures that the underlying database is setup properly for use by the app.
  /// </summary>
  /// <param name="host"> This <see cref="IHost" />. </param>
  /// <returns>
  /// A task representing the asynchronous operation of setting up the underlying database.
  /// </returns>
  public static async Task EnsureDbIsSetup(this IHost host)
  {
    IMediator mediator = host.Services.GetRequiredService<IMediator>();
    SequenceIdOptions sequenceIdOptions = host
      .Services.GetRequiredService<IOptions<SequenceIdOptions>>()
      .Value;
    GetSequenceIdQueryResponse getSequenceIdQueryResponse = await mediator.Send(
      new GetSequenceIdQuery(sequenceIdOptions.Id)
    );
    if (getSequenceIdQueryResponse.SequenceId == null)
    {
      await mediator.Send(new CreateSequenceIdCommand(sequenceIdOptions.Id));
    }
  }
}
