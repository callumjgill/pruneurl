using MediatR;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Commands.CreateSequenceId
{
  /// <summary>
  /// The handler for the <see cref="CreateSequenceIdCommand" /> request.
  /// </summary>
  public sealed class CreateSequenceIdCommandHandler : IRequestHandler<CreateSequenceIdCommand>
  {
    #region Private Fields

    private readonly IDbWriteBatch<SequenceId> dbWriteBatch;
    private readonly ISequenceIdFactory sequenceIdFactory;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="CreateSequenceIdCommandHandler" /> class.
    /// </summary>
    /// <param name="dbWriteBatch">
    /// The batch of operations for the <see cref="SequenceId" /> entity which will be committed to
    /// the database.
    /// </param>
    /// <param name="sequenceIdFactory">
    /// The factory for creating instances of the <see cref="SequenceId" /> type.
    /// </param>
    public CreateSequenceIdCommandHandler(IDbWriteBatch<SequenceId> dbWriteBatch,
                                          ISequenceIdFactory sequenceIdFactory)
    {
      this.dbWriteBatch = dbWriteBatch;
      this.sequenceIdFactory = sequenceIdFactory;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IRequestHandler{TRequest}.Handle(TRequest, CancellationToken)" />
    public Task Handle(CreateSequenceIdCommand request, CancellationToken cancellationToken)
    {
      SequenceId newSequenceId = sequenceIdFactory.Create(request.Id, 0);
      dbWriteBatch.Create(newSequenceId);
      return dbWriteBatch.CommitAsync(cancellationToken);
    }

    #endregion Public Methods
  }
}