using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.App.Endpoints.Models;

/// <summary>
/// A DTO describing the request body for the POST REST endpoint of the <see cref="ShortUrl" /> resource.
/// </summary>
/// <param name="LongUrl">
/// The long url which will have a corresponding <see cref="ShortUrl" /> resource created for it.
/// </param>
public sealed record ShortUrlPostRequest(string LongUrl) { }
