using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Queries.GetShortUrl;

/// <summary>
/// The immutable response to a <see cref="GetShortUrlQuery" />.
/// </summary>
/// <param name="ShortUrl"> The <see cref="Domain.Entities.ShortUrl" /> entity. </param>
public sealed record GetShortUrlQueryResponse(ShortUrl ShortUrl) { }
