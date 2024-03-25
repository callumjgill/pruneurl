using MediatR;
using Microsoft.Extensions.Options;
using PruneUrl.Backend.Application.Configuration;
using PruneUrl.Backend.Application.Exceptions;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Transactions;

/// <summary>
/// The handler for the <see cref="GetAndBumpSequenceIdRequest" /> transaction request.
/// </summary>
/// <param name="dbTransactionProvider"> The provider for <see cref="IDbTransaction{T}" />'s. </param>
/// <param name="sequenceIdFactory">
/// The factory for creating instances of the <see cref="SequenceId" /> type.
/// </param>
public sealed class GetAndBumpSequenceIdRequestHandler(
  IDbTransactionProvider dbTransactionProvider,
  IOptions<SequenceIdOptions> sequenceIdOptions,
  ISequenceIdFactory sequenceIdFactory
) : IRequestHandler<GetAndBumpSequenceIdRequest, GetAndBumpSequenceIdResponse>
{
  private readonly IDbTransactionProvider dbTransactionProvider = dbTransactionProvider;
  private readonly ISequenceIdFactory sequenceIdFactory = sequenceIdFactory;
  private readonly SequenceIdOptions sequenceIdOptions = sequenceIdOptions.Value;

  /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
  public async Task<GetAndBumpSequenceIdResponse> Handle(
    GetAndBumpSequenceIdRequest request,
    CancellationToken cancellationToken
  )
  {
    SequenceId sequenceId = await dbTransactionProvider.RunTransactionAsync<SequenceId>(
      async dbTransaction =>
      {
        SequenceId sequenceId = await GetSequenceId(dbTransaction, cancellationToken);
        BumpSequenceIdValue(dbTransaction, sequenceId);
        return sequenceId;
      },
      cancellationToken
    );
    return new GetAndBumpSequenceIdResponse(sequenceId);
  }

  private void BumpSequenceIdValue(
    IDbUpdateOperation<SequenceId> dbUpdateOperation,
    SequenceId previousSequenceId
  )
  {
    SequenceId nextSequenceId = sequenceIdFactory.Create(
      previousSequenceId.Id,
      previousSequenceId.Value + 1
    );
    dbUpdateOperation.Update(nextSequenceId);
  }

  private async Task<SequenceId> GetSequenceId(
    IDbGetByIdOperation<SequenceId> dbGetByIdOperation,
    CancellationToken cancellationToken
  )
  {
    SequenceId? sequenceId = await dbGetByIdOperation.GetByIdAsync(
      sequenceIdOptions.Id,
      cancellationToken
    );
    if (sequenceId == null)
    {
      throw new EntityNotFoundException(typeof(SequenceId), sequenceIdOptions.Id);
    }

    return sequenceId;
  }
}
