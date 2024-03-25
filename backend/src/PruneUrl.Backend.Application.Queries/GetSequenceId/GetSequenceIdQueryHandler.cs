using MediatR;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Queries.GetSequenceId;

/// <summary>
/// The handler for the <see cref="GetSequenceIdQuery" /> request.
/// </summary>
/// <param name="dbGetByIdOperation">
/// The operation for retrieving a <see cref="SequenceId" /> by its id.
/// </param>
public sealed class GetSequenceIdQueryHandler(IDbGetByIdOperation<SequenceId> dbGetByIdOperation)
  : IRequestHandler<GetSequenceIdQuery, GetSequenceIdQueryResponse>
{
  private readonly IDbGetByIdOperation<SequenceId> dbGetByIdOperation = dbGetByIdOperation;

  /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
  public async Task<GetSequenceIdQueryResponse> Handle(
    GetSequenceIdQuery request,
    CancellationToken cancellationToken
  )
  {
    SequenceId? sequenceId = await dbGetByIdOperation.GetByIdAsync(request.Id, cancellationToken);
    return new GetSequenceIdQueryResponse(sequenceId);
  }
}
