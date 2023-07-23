using MediatR;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Commands.CreateShortUrl
{
  /// <summary>
  /// The handler for the <see cref="CreateShortUrlCommand" />.
  /// </summary>
  public sealed class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand>
  {
    #region Private Fields

    private readonly IDbWriteBatch<ShortUrl> dbWriteBatch;
    private readonly IShortUrlFactory shortUrlFactory;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="CreateShortUrlCommandHandler" /> class.
    /// </summary>
    /// <param name="dbWriteBatch">
    /// The operation for creating a <see cref="ShortUrl" /> in the database.
    /// </param>
    /// <param name="shortUrlFactory"> The factory for creating <see cref="ShortUrl" /> instances. </param>
    public CreateShortUrlCommandHandler(IDbWriteBatch<ShortUrl> dbWriteBatch, IShortUrlFactory shortUrlFactory)
    {
      this.dbWriteBatch = dbWriteBatch;
      this.shortUrlFactory = shortUrlFactory;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IRequestHandler{TRequest}.Handle(TRequest, CancellationToken)" />
    public Task Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
      ShortUrl shortUrl = shortUrlFactory.Create(request.LongUrl, request.SequenceId);
      dbWriteBatch.Create(shortUrl);
      return dbWriteBatch.CommitAsync(cancellationToken);
    }

    #endregion Public Methods
  }
}