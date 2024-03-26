namespace PruneUrl.Backend.Application.Commands;

/// <summary>
/// The immutable response to a <see cref="CreateShortUrlCommand"/>.
/// </summary>
/// <param name="ShortUrl">The short url that was created.</param>
public sealed record CreateShortUrlCommandResponse(string ShortUrl) { }
