using MediatR;
using PruneUrl.Backend.Application.Exceptions.Database;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Queries.GetShortUrl;

/// <summary>
/// The handler for the <see cref="GetShortUrlQuery" /> which will return a <see
/// cref="GetShortUrlQueryResponse" />.
/// </summary>
/// <param name="dbGetByIdOperation">
/// The operation for retriving a <see cref="ShortUrl" /> from the database using its id.
/// </param>
/// <param name="sequenceIdProvider">
/// The provider of a "sequence id" given the equivalent "short url".
/// </param>
public sealed class GetShortUrlQueryHandler(
  IDbGetByIdOperation<ShortUrl> dbGetByIdOperation,
  ISequenceIdProvider sequenceIdProvider
) : IRequestHandler<GetShortUrlQuery, GetShortUrlQueryResponse>
{
  private readonly IDbGetByIdOperation<ShortUrl> dbGetByIdOperation = dbGetByIdOperation;
  private readonly ISequenceIdProvider sequenceIdProvider = sequenceIdProvider;

  /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
  public async Task<GetShortUrlQueryResponse> Handle(
    GetShortUrlQuery request,
    CancellationToken cancellationToken
  )
  {
    int sequenceId = sequenceIdProvider.GetSequenceId(request.ShortUrl);
    ShortUrl shortUrl = await GetShortUrl(sequenceId, cancellationToken);
    return new GetShortUrlQueryResponse(shortUrl);
  }

  private async Task<ShortUrl> GetShortUrl(int sequenceId, CancellationToken cancellationToken)
  {
    string id = sequenceId.ToString();
    ShortUrl? shortUrl = await dbGetByIdOperation.GetByIdAsync(id, cancellationToken);
    if (shortUrl == null)
    {
      throw new EntityNotFoundException(typeof(ShortUrl), id);
    }

    return shortUrl;
  }
}
