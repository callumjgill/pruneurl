﻿using MediatR;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Commands;

/// <summary>
/// An immutable command for creating a <see cref="ShortUrl" />
/// </summary>
/// <param name="LongUrl">
/// The "long" url, which is the original url the shorter url will allow redirection to.
/// </param>
public sealed record CreateShortUrlCommand(string LongUrl)
  : IRequest<CreateShortUrlCommandResponse> { }
