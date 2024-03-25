using MediatR;

namespace PruneUrl.Backend.Application.Queries;

/// <summary>
/// An immutable query for getting a <see cref="Domain.Entities.ShortUrl" /> entity
/// </summary>
/// <param name="ShortUrl"> The actual "short url" value to get the corresponding entity for. </param>
public sealed record GetShortUrlQuery(string ShortUrl) : IRequest<GetShortUrlQueryResponse> { }
