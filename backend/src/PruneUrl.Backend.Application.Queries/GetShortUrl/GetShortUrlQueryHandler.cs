using MediatR;
using Microsoft.EntityFrameworkCore;
using PruneUrl.Backend.Application.Exceptions;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Queries;

/// <summary>
/// The handler for the <see cref="GetShortUrlQuery" /> which will return a <see
/// cref="GetShortUrlQueryResponse" />.
/// </summary>
/// <param name="dbContext">
/// The <see cref="IDbContext"/> to allow interaction with the database.
/// </param>
public sealed class GetShortUrlQueryHandler(IDbContext dbContext)
  : IRequestHandler<GetShortUrlQuery, GetShortUrlQueryResponse>
{
  private readonly IDbContext dbContext = dbContext;

  /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
  public async Task<GetShortUrlQueryResponse> Handle(
    GetShortUrlQuery request,
    CancellationToken cancellationToken
  )
  {
    ShortUrl shortUrl =
      await dbContext.ShortUrls.FirstOrDefaultAsync(
        shortUrl => shortUrl.Url == request.ShortUrl,
        cancellationToken
      )
      ?? throw new EntityNotFoundException<ShortUrl>($"{nameof(ShortUrl.Url)}={request.ShortUrl}.");
    return new GetShortUrlQueryResponse(shortUrl);
  }
}
