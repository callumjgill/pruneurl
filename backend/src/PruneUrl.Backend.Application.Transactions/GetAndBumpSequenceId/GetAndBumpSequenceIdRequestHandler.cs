using MediatR;
using Microsoft.Extensions.Options;
using PruneUrl.Backend.Application.Configuration.Entities.SequenceId;
using PruneUrl.Backend.Application.Exceptions.Database;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Write;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId
{
  /// <summary>
  /// The handler for the <see cref="GetAndBumpSequenceIdRequest" /> transaction request.
  /// </summary>
  public sealed class GetAndBumpSequenceIdRequestHandler : IRequestHandler<GetAndBumpSequenceIdRequest, GetAndBumpSequenceIdResponse>
  {
    #region Private Fields

    private readonly IDbTransactionProvider dbTransactionProvider;
    private readonly ISequenceIdFactory sequenceIdFactory;
    private readonly SequenceIdOptions sequenceIdOptions;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="GetAndBumpSequenceIdRequestHandler" /> class.
    /// </summary>
    /// <param name="dbTransactionProvider"> The provider for <see cref="IDbTransaction{T}" />'s. </param>
    /// <param name="sequenceIdFactory">
    /// The factory for creating instances of the <see cref="SequenceId" /> type.
    /// </param>
    public GetAndBumpSequenceIdRequestHandler(IDbTransactionProvider dbTransactionProvider,
                                              IOptions<SequenceIdOptions> sequenceIdOptions,
                                              ISequenceIdFactory sequenceIdFactory)
    {
      this.dbTransactionProvider = dbTransactionProvider;
      this.sequenceIdFactory = sequenceIdFactory;
      this.sequenceIdOptions = sequenceIdOptions.Value;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
    public async Task<GetAndBumpSequenceIdResponse> Handle(GetAndBumpSequenceIdRequest request, CancellationToken cancellationToken)
    {
      SequenceId sequenceId = await dbTransactionProvider.RunTransactionAsync<SequenceId>(async dbTransaction =>
      {
        SequenceId sequenceId = await GetSequenceId(dbTransaction, cancellationToken);
        BumpSequenceIdValue(dbTransaction, sequenceId);
        return sequenceId;
      }, cancellationToken);
      return new GetAndBumpSequenceIdResponse(sequenceId);
    }

    #endregion Public Methods

    #region Private Methods

    private void BumpSequenceIdValue(IDbUpdateOperation<SequenceId> dbUpdateOperation, SequenceId previousSequenceId)
    {
      SequenceId nextSequenceId = sequenceIdFactory.Create(previousSequenceId.Id, previousSequenceId.Value + 1);
      dbUpdateOperation.Update(nextSequenceId);
    }

    private async Task<SequenceId> GetSequenceId(IDbGetByIdOperation<SequenceId> dbGetByIdOperation, CancellationToken cancellationToken)
    {
      SequenceId? sequenceId = await dbGetByIdOperation.GetByIdAsync(sequenceIdOptions.Id, cancellationToken);
      if (sequenceId == null)
      {
        throw new EntityNotFoundException(typeof(SequenceId), sequenceIdOptions.Id);
      }

      return sequenceId;
    }

    #endregion Private Methods
  }
}