using MediatR;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Queries.GetSequenceId
{
  /// <summary>
  /// The handler for the <see cref="GetSequenceIdQuery" /> request.
  /// </summary>
  public sealed class GetSequenceIdQueryHandler : IRequestHandler<GetSequenceIdQuery, GetSequenceIdQueryResponse>
  {
    #region Private Fields

    private readonly IDbGetByIdOperation<SequenceId> dbGetByIdOperation;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="GetSequenceIdQueryHandler" /> class.
    /// </summary>
    /// <param name="dbGetByIdOperation">
    /// The operation for retrieving a <see cref="SequenceId" /> by its id.
    /// </param>
    public GetSequenceIdQueryHandler(IDbGetByIdOperation<SequenceId> dbGetByIdOperation)
    {
      this.dbGetByIdOperation = dbGetByIdOperation;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
    public async Task<GetSequenceIdQueryResponse> Handle(GetSequenceIdQuery request, CancellationToken cancellationToken)
    {
      SequenceId? sequenceId = await dbGetByIdOperation.GetByIdAsync(request.Id, cancellationToken);
      return new GetSequenceIdQueryResponse(sequenceId);
    }

    #endregion Public Methods
  }
}