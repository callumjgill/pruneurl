using MediatR;
using Microsoft.EntityFrameworkCore;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Commands;

/// <summary>
/// The handler for the <see cref="CreateShortUrlCommand" />.
/// </summary>
/// <param name="dbContext">
/// The <see cref="IDbContext"/> to allow interaction with the database.
/// </param>
/// <param name="shortUrlFactory"> The factory for creating <see cref="ShortUrl" /> instances. </param>
public sealed class CreateShortUrlCommandHandler(
  IDbContext dbContext,
  IShortUrlFactory shortUrlFactory
) : IRequestHandler<CreateShortUrlCommand, CreateShortUrlCommandResponse>
{
  private readonly IDbContext dbContext = dbContext;
  private readonly IShortUrlFactory shortUrlFactory = shortUrlFactory;

  /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
  public async Task<CreateShortUrlCommandResponse> Handle(
    CreateShortUrlCommand request,
    CancellationToken cancellationToken
  )
  {
    // Need to create a new entry so that we have a guaranteed unique sequence id
    ShortUrl placeholderShortUrl = shortUrlFactory.Create();
    dbContext.ShortUrls.Add(placeholderShortUrl);
    await dbContext.SaveChangesAsync(cancellationToken);

    dbContext.Attach(placeholderShortUrl).State = EntityState.Detached;

    ShortUrl shortUrl = shortUrlFactory.Create(request.LongUrl, placeholderShortUrl.Id);
    dbContext.ShortUrls.Update(shortUrl);
    await dbContext.SaveChangesAsync(cancellationToken);
    return new CreateShortUrlCommandResponse(shortUrl.Url);
  }
}
