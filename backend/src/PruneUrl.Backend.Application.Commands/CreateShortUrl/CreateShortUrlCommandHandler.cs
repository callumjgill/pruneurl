using MediatR;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Commands;

/// <summary>
/// The handler for the <see cref="CreateShortUrlCommand" />.
/// </summary>
/// <param name="dbWriteBatch">
/// The operation for creating a <see cref="ShortUrl" /> in the database.
/// </param>
/// <param name="shortUrlFactory"> The factory for creating <see cref="ShortUrl" /> instances. </param>
public sealed class CreateShortUrlCommandHandler(
  IDbWriteBatch<ShortUrl> dbWriteBatch,
  IShortUrlFactory shortUrlFactory
) : IRequestHandler<CreateShortUrlCommand>
{
  private readonly IDbWriteBatch<ShortUrl> dbWriteBatch = dbWriteBatch;
  private readonly IShortUrlFactory shortUrlFactory = shortUrlFactory;

  /// <inheritdoc cref="IRequestHandler{TRequest}.Handle(TRequest, CancellationToken)" />
  public Task Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
  {
    ShortUrl shortUrl = shortUrlFactory.Create(request.LongUrl, request.SequenceId);
    dbWriteBatch.Create(shortUrl);
    return dbWriteBatch.CommitAsync(cancellationToken);
  }
}
