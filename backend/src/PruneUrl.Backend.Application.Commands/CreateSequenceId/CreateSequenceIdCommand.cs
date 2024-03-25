using MediatR;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Commands.CreateSequenceId;

/// <summary>
/// The immutable command for creating a new <see cref="SequenceId" /> entity.
/// </summary>
/// <param name="Id"> The unique identifier for the <see cref="SequenceId" /> entity. </param>
public sealed record CreateSequenceIdCommand(string Id) : IRequest { }
