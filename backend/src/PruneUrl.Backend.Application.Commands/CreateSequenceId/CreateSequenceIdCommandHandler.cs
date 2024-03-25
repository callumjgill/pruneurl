using MediatR;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Commands.CreateSequenceId;

/// <summary>
/// The handler for the <see cref="CreateSequenceIdCommand" /> request.
/// </summary>
/// <param name="dbWriteBatch">
/// The batch of operations for the <see cref="SequenceId" /> entity which will be committed to
/// the database.
/// </param>
/// <param name="sequenceIdFactory">
/// The factory for creating instances of the <see cref="SequenceId" /> type.
/// </param>
public sealed class CreateSequenceIdCommandHandler(
  IDbWriteBatch<SequenceId> dbWriteBatch,
  ISequenceIdFactory sequenceIdFactory
) : IRequestHandler<CreateSequenceIdCommand>
{
  private readonly IDbWriteBatch<SequenceId> dbWriteBatch = dbWriteBatch;
  private readonly ISequenceIdFactory sequenceIdFactory = sequenceIdFactory;

  /// <inheritdoc cref="IRequestHandler{TRequest}.Handle(TRequest, CancellationToken)" />
  public Task Handle(CreateSequenceIdCommand request, CancellationToken cancellationToken)
  {
    SequenceId newSequenceId = sequenceIdFactory.Create(request.Id, 0);
    dbWriteBatch.Create(newSequenceId);
    return dbWriteBatch.CommitAsync(cancellationToken);
  }
}
